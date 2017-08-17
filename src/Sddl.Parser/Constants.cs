using System.Collections.Generic;

namespace Sddl.Parser
{
    internal static class Constants
    {
        // Replace: #define SDDL_([A-Z1-9_]+)[ ]*TEXT\(\"([A-Z_]+)\"\).*
        // With: { "$2", "$1" },

        public const char OwnerTag = 'O';

        public const char GroupTag = 'G';

        public const char DaclTag = 'D';
        
        public const char SaclTag = 'S';

        public static Dictionary<string, string> SdControls = new Dictionary<string, string>
        {
            { "P", "PROTECTED" },
            { "AR", "AUTO_INHERIT_REQ" },
            { "AI", "AUTO_INHERITED" },
            { "NO_ACCESS_CONTROL", "NULL_ACL" },
        };

        public static Dictionary<string, string> AceTypes = new Dictionary<string, string>
        {
            { "A", "ACCESS_ALLOWED" },
            { "D", "ACCESS_DENIED" },
            { "OA", "OBJECT_ACCESS_ALLOWED" },
            { "OD", "OBJECT_ACCESS_DENIED" },
            { "AU", "AUDIT" },
            { "AL", "ALARM" },
            { "OU", "OBJECT_AUDIT" },
            { "OL", "OBJECT_ALARM" },
            { "ML", "MANDATORY_LABEL" },
            { "TL", "PROCESS_TRUST_LABEL" },
            { "XA", "CALLBACK_ACCESS_ALLOWED" },
            { "XD", "CALLBACK_ACCESS_DENIED" },
            { "RA", "RESOURCE_ATTRIBUTE" },
            { "SP", "SCOPED_POLICY_ID" },
            { "XU", "CALLBACK_AUDIT" },
            { "ZA", "CALLBACK_OBJECT_ACCESS_ALLOWED" },
        };

        public static Dictionary<string, string> AceFlags = new Dictionary<string, string>
        {
            { "CI", "CONTAINER_INHERIT" },
            { "OI", "OBJECT_INHERIT" },
            { "NP", "NO_PROPAGATE" },
            { "IO", "INHERIT_ONLY" },
            { "ID", "INHERITED" },
            { "SA", "AUDIT_SUCCESS" },
            { "FA", "AUDIT_FAILURE" },
        };

        public static Dictionary<string, string> Rights = new Dictionary<string, string>
        {
            { "RP", "READ_PROPERTY" },
            { "WP", "WRITE_PROPERTY" },
            { "CC", "CREATE_CHILD" },
            { "DC", "DELETE_CHILD" },
            { "LC", "LIST_CHILDREN" },
            { "SW", "SELF_WRITE" },
            { "LO", "LIST_OBJECT" },
            { "DT", "DELETE_TREE" },
            { "CR", "CONTROL_ACCESS" },
            { "RC", "READ_CONTROL" },
            { "WD", "WRITE_DAC" },
            { "WO", "WRITE_OWNER" },
            { "SD", "STANDARD_DELETE" },
            { "GA", "GENERIC_ALL" },
            { "GR", "GENERIC_READ" },
            { "GW", "GENERIC_WRITE" },
            { "GX", "GENERIC_EXECUTE" },
            { "FA", "FILE_ALL" },
            { "FR", "FILE_READ" },
            { "FW", "FILE_WRITE" },
            { "FX", "FILE_EXECUTE" },
            { "KA", "KEY_ALL" },
            { "KR", "KEY_READ" },
            { "KW", "KEY_WRITE" },
            { "KX", "KEY_EXECUTE" },
            { "NW", "NO_WRITE_UP" },
            { "NR", "NO_READ_UP" },
            { "NX", "NO_EXECUTE_UP" },
        };
        
        public static Dictionary<string, string> UserAliases = new Dictionary<string, string>
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

        public const char Separator = ';';
        
        public const char Deliminator = ':';

        public const char AceBegin = '(';
        
        public const char AceEnd = ')';
    }
}
