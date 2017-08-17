using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Sddl.Parser
{
    public class Parser
    {
        public SddlInfo Parse(string sddl)
        {
            Dictionary<char, string> components = new Dictionary<char, string>();

            int i = 0;
            int idx = 0;
            int len = 0;

            while (i != -1)
            {
                i = sddl.IndexOf(Constants.Deliminator, idx + 1);

                if (idx > 0)
                {
                    len = i > 0
                        ? i - idx - 2
                        : sddl.Length - (idx + 1);
                    components.Add(sddl[idx - 1], sddl.Substring(idx + 1, len));
                }

                idx = i;
            }

            var sddlInfo = new SddlInfo();

            if (components.TryGetValue(Constants.OwnerTag, out var owner))
            {
                sddlInfo.Owner = TranslateSid(owner);
                components.Remove(Constants.OwnerTag);
            }

            if (components.TryGetValue(Constants.GroupTag, out var group))
            {
                sddlInfo.Group = TranslateSid(group);
                components.Remove(Constants.GroupTag);
            }

            if (components.TryGetValue(Constants.DaclTag, out var dacl))
            {
                sddlInfo.Dacl = ParseAcl(dacl);
                components.Remove(Constants.DaclTag);
            }

            if (components.TryGetValue(Constants.SaclTag, out var sacl))
            {
                sddlInfo.Sacl = ParseAcl(sacl);
                components.Remove(Constants.SaclTag);
            }

            if (components.Any())
            {
                // TODO validate
            }

            return sddlInfo;
        }

        private AclInfo ParseAcl(string aclString)
        {
            AclInfo aclInfo = new AclInfo();

            int begin = aclString.IndexOf(Constants.AceBegin);

            // Flags
            string flags = begin == -1 ? aclString : aclString.Substring(0, begin);
            string reminder = MatchMany(flags, Constants.SdControls, out var flagsLabels);

            if (!string.IsNullOrEmpty(reminder))
            {
                throw new InvalidOperationException(
                    "An error occurred while parsing an aclString. Flags part can not be fully parsed.");
            }

            aclInfo.Flags = flagsLabels.ToArray();

            // Aces
            if (begin != -1)
            {
                LinkedList<AceInfo> aceInfos = new LinkedList<AceInfo>();

                // brackets balance: '(' = +1, ')' = -1
                int balance = 0;
                for (int end = begin; end < aclString.Length; end++)
                {
                    if (aclString[end] == Constants.AceBegin)
                    {
                        balance += 1;
                    }
                    else if (aclString[end] == Constants.AceEnd)
                    {
                        balance -= 1;

                        int length = end - begin - 2;
                        if (length < 0)
                        {
                            throw new InvalidOperationException(
                                "An error occurred while parsing an aclString. Ace is empty.");
                        }

                        if (balance == 0)
                            aceInfos.AddLast(ParseAce(aclString.Substring(begin + 1, length)));
                    }
                    else if (balance <= 0)
                    {
                        throw new InvalidOperationException(
                            "An error occurred while parsing an aclString. Acl contains unexpected characters.");
                    }
                }
                
                aclInfo.Aces = aceInfos.ToArray();
            }

            return aclInfo;
        }

        private AceInfo ParseAce(string aceString)
        {
            var parts = aceString.Split(Constants.Separator);

            if (parts.Length < 6)
            {
                throw new InvalidOperationException(
                    "An error occured while parsing an aceString. Ace contains invalid number of parts.");
            }

            var aceInfo = new AceInfo();

            // ace_type
            if (parts[0].Length > 0)
            {
                string reminder = MatchOne(parts[0], Constants.AceTypes, out var type);
                // TODO validate reminder
                aceInfo.AceType = type;
            }

            // ace_flags
            if (parts[1].Length > 0)
            {
                string reminder = MatchMany(parts[1], Constants.AceFlags, out var flags);
                // TODO validate reminder
                aceInfo.Flags = flags.ToArray();
            }

            // rights
            if (parts[2].Length > 0)
            {
                if (uint.TryParse(parts[2], out uint accessMask))
                {
                    // TODO parse accessMask
                }
                else
                {
                    string reminder = MatchMany(parts[2], Constants.Rights, out var rights);
                    // TODO validate reminder
                    aceInfo.Rights = rights.ToArray();
                }
            }

            // object_guid
            if (parts[3].Length > 0)
            {
                aceInfo.ObjectType = TranslateGuid(parts[3]);
            }

            // inherit_object_guid
            if (parts[4].Length > 0)
            {
                aceInfo.InheritObjectType = TranslateGuid(parts[4]);
            }

            // account_sid
            if (parts[5].Length > 0)
            {
                aceInfo.Account = TranslateSid(parts[5]);
            }

            // resource_attribute
            if (parts.Length > 6)
            {

            }

            return aceInfo;
        }

        private string TranslateSid(string sidString)
        {
#if SYSTEM_DIRECTORYSERVICES_IN_NETCORE
            try
            {
                var sid = new SecurityIdentifier(sidString);
                var ntAccount = sid.Translate(typeof(NTAccount));

                return ntAccount.ToString();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"An error occured while parsing a sidString.", e);
            }
#else
            string reminder = MatchOne(sidString, Constants.UserAliases, out var sidName);
            AssertNoReminder(reminder, Severity.WARN);

            return sidName;
#endif
        }

        private string TranslateGuid(string guidString)
        {
#if SYSTEM_DIRECTORYSERVICES_IN_NETCORE
            var ldapString = $"LDAP://<GUID={guidString}";

            if (DirectoryEntry.Exists(ldapString))
            {
                var directoryEntry = new DirectoryEntry(ldapString);
                return directoryEntry.Name;
            }
            else
                return "GUID ptr is NULL";
#else
            return guidString;
#endif
        }

        private static string MatchMany(string input, Dictionary<string, string> tokensToLabels, out LinkedList<string> labels)
        {
            labels = new LinkedList<string>();

            string reminder = input;
            while (reminder.Length > 0)
            {
                reminder = MatchOne(reminder, tokensToLabels, out string label);

                if (label != null)
                    labels.AddLast(label);
                else
                    break;
            }

            return reminder;
        }

        private static string MatchOne(string input, Dictionary<string, string> tokensToLabels, out string label)
        {
            foreach (var kv in tokensToLabels)
            {
                if (input.StartsWith(kv.Key))
                {
                    label = kv.Value;
                    return input.Substring(kv.Key.Length);
                }
            }

            label = null;
            return input;
        }

        private enum Severity
        {
            ERROR,
            WARN,
            INFO
        }

        private void AssertNoReminder(string reminder, Severity severity)
        {
            if (!string.IsNullOrEmpty(reminder))
            {
                throw new InvalidOperationException(
                    $"[{severity}] Sddl cannot be parsed. The parser encountered unrecognized sequence '{reminder}'");
            }
        }

        public class SddlInfo
        {
            public string Owner { get; set; }

            public string Group { get; set; }

            public AclInfo Dacl { get; set; }

            public AclInfo Sacl { get; set; }

            public RawSecurityDescriptor RawSecurityDescriptor { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.Append($"{nameof(Owner)}: {Owner}{Environment.NewLine}");
                sb.Append($"{nameof(Group)}: {Group}{Environment.NewLine}");
                sb.Append($"{nameof(Dacl)}: {Dacl?.ToString()}{Environment.NewLine}");
                sb.Append($"{nameof(Sacl)}: {Sacl?.ToString()}{Environment.NewLine}");

                return sb.ToString();
            }
        }

        public class AclInfo
        {
            public string[] Flags { get; set; }

            public AceInfo[] Aces { get; set; }
        }

        public class AceInfo
        {
            public string AceType { get; set; }

            public string[] Flags { get; set; }

            public string[] Rights { get; set; }

            public string ObjectType { get; internal set; }

            public string InheritObjectType { get; internal set; }

            public string Account { get; set; }
        }
    }
}
