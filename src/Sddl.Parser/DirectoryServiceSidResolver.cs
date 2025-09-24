using System;
using System.Security.Principal;

namespace Sddl.Parser
{
    /// <summary>
    /// Resolves SID strings using SecurityIdentifier.Translate (DirectoryService lookup)
    /// </summary>
    public class DirectoryServiceSidResolver : ISidResolver
    {
        /// <summary>
        /// Attempts to resolve a SID string to a meaningful name using DirectoryService lookup
        /// </summary>
        /// <param name="sidString">The SID string to resolve</param>
        /// <returns>The resolved name, or null if resolution fails</returns>
        public string ResolveSid(string sidString)
        {
            if (string.IsNullOrEmpty(sidString))
                return null;

            try
            {
                // Try to parse as a SecurityIdentifier
                SecurityIdentifier sid;
                try
                {
                    sid = new SecurityIdentifier(sidString);
                }
                catch
                {
                    // Not a valid SID format
                    return null;
                }

                // Attempt to translate to NTAccount using DirectoryService lookup
                try
                {
                    var account = (NTAccount)sid.Translate(typeof(NTAccount));
                    return account.Value;
                }
                catch
                {
                    // Translation failed - SID may not exist or may not be translatable
                    return null;
                }
            }
            catch
            {
                // Any other error during resolution
                return null;
            }
        }
    }
}