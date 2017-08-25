using System.Collections.Generic;

namespace Sddl.Parser
{
    public class Sid
    {
        public string Raw { get; }

        public string Alias { get; }
        
        public Sid(string sid)
        {
            Raw = sid;

            string alias =
                Match.OneByPrefix(sid, KnownAliases, out var _) ??
                Match.OneByPrefix(sid, KnownSids, out var _);

            if (alias == null)
            {
                // ERROR Unknown SID.
                alias = string.Format(Constants.UnknownFormat, sid);
            }
            
            Alias = alias;
        }

        public static readonly Dictionary<string, string> KnownAliases = new Dictionary<string, string>
        {
            { "DA", "DOMAIN_ADMINISTRATORS" },
            { "DG", "DOMAIN_GUESTS" },
            { "DU", "DOMAIN_USERS" },
            { "ED", "ENTERPRISE_DOMAIN_CONTROLLERS" },
            { "DD", "DOMAIN_DOMAIN_CONTROLLERS" },
            { "DC", "DOMAIN_COMPUTERS" },
            { "BA", "BUILTIN_ADMINISTRATORS" },
            { "BG", "BUILTIN_GUESTS" },
            { "BU", "BUILTIN_USERS" },
            { "LA", "LOCAL_ADMIN" },
            { "LG", "LOCAL_GUEST" },
            { "AO", "ACCOUNT_OPERATORS" },
            { "BO", "BACKUP_OPERATORS" },
            { "PO", "PRINTER_OPERATORS" },
            { "SO", "SERVER_OPERATORS" },
            { "AU", "AUTHENTICATED_USERS" },
            { "PS", "PERSONAL_SELF" },
            { "CO", "CREATOR_OWNER" },
            { "CG", "CREATOR_GROUP" },
            { "SY", "LOCAL_SYSTEM" },
            { "PU", "POWER_USERS" },
            { "WD", "EVERYONE" },
            { "RE", "REPLICATOR" },
            { "IU", "INTERACTIVE" },
            { "NU", "NETWORK" },
            { "SU", "SERVICE" },
            { "RC", "RESTRICTED_CODE" },
            { "WR", "WRITE_RESTRICTED_CODE" },
            { "AN", "ANONYMOUS" },
            { "SA", "SCHEMA_ADMINISTRATORS" },
            { "CA", "CERT_SERV_ADMINISTRATORS" },
            { "RS", "RAS_SERVERS" },
            { "EA", "ENTERPRISE_ADMINS" },
            { "PA", "GROUP_POLICY_ADMINS" },
            { "RU", "ALIAS_PREW2KCOMPACC" },
            { "LS", "LOCAL_SERVICE" },
            { "NS", "NETWORK_SERVICE" },
            { "RD", "REMOTE_DESKTOP" },
            { "NO", "NETWORK_CONFIGURATION_OPS" },
            { "MU", "PERFMON_USERS" },
            { "LU", "PERFLOG_USERS" },
            { "IS", "IIS_USERS" },
            { "CY", "CRYPTO_OPERATORS" },
            { "OW", "OWNER_RIGHTS" },
            { "ER", "EVENT_LOG_READERS" },
            { "RO", "ENTERPRISE_RO_DCs" },
            { "CD", "CERTSVC_DCOM_ACCESS" },
            { "AC", "ALL_APP_PACKAGES" },
            { "RA", "RDS_REMOTE_ACCESS_SERVERS" },
            { "ES", "RDS_ENDPOINT_SERVERS" },
            { "MS", "RDS_MANAGEMENT_SERVERS" },
            { "UD", "USER_MODE_DRIVERS" },
            { "HA", "HYPER_V_ADMINS" },
            { "CN", "CLONEABLE_CONTROLLERS" },
            { "AA", "ACCESS_CONTROL_ASSISTANCE_OPS" },
            { "RM", "REMOTE_MANAGEMENT_USERS" },
            { "AS", "AUTHORITY_ASSERTED" },
            { "SS", "SERVICE_ASSERTED" },
            { "AP", "PROTECTED_USERS" },
            { "KA", "KEY_ADMINS" },
            { "EK", "ENTERPRISE_KEY_ADMINS" },
        };

        public static readonly Dictionary<string, string> KnownSids = new Dictionary<string, string>
        {

        };
    }
}