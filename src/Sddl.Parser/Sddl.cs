using System.Collections.Generic;
using System.Linq;

namespace Sddl.Parser
{
    public class Sddl
    {
        public string Raw { get; }

        public Sid Owner { get; }
        public Sid Group { get; }
        public Acl Dacl { get; }
        public Acl Sacl { get; }

        public Sddl(string sddl, SecurableObjectType type = SecurableObjectType.Unknown)
        {
            Raw = sddl;

            Dictionary<char, string> components = new Dictionary<char, string>();

            int i = 0;
            int idx = 0;
            int len = 0;

            while (i != -1)
            {
                i = sddl.IndexOf(DeliminatorToken, idx + 1);

                if (idx > 0)
                {
                    len = i > 0
                        ? i - idx - 2
                        : sddl.Length - (idx + 1);
                    components.Add(sddl[idx - 1], sddl.Substring(idx + 1, len));
                }

                idx = i;
            }

            if (components.TryGetValue(OwnerToken, out var owner))
            {
                Owner = new Sid(owner);
                components.Remove(OwnerToken);
            }

            if (components.TryGetValue(GroupToken, out var group))
            {
                Group = new Sid(group);
                components.Remove(GroupToken);
            }

            if (components.TryGetValue(DaclToken, out var dacl))
            {
                Dacl = new Acl(dacl, type);
                components.Remove(DaclToken);
            }

            if (components.TryGetValue(SaclToken, out var sacl))
            {
                Sacl = new Acl(sacl, type);
                components.Remove(SaclToken);
            }

            if (components.Any())
            {
                // ERROR Unknown components encountered.
            }
        }

        public const char DeliminatorToken = ':';
        public const char OwnerToken = 'O';
        public const char GroupToken = 'G';
        public const char DaclToken = 'D';
        public const char SaclToken = 'S';
    }
}