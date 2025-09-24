namespace Sddl.Parser
{
    /// <summary>
    /// Configuration options for SID resolution
    /// </summary>
    public class SidResolverOptions
    {
        /// <summary>
        /// Gets or sets whether to enable DirectoryService lookup for unknown SIDs
        /// </summary>
        public bool EnableDirectoryServiceLookup { get; set; } = false;

        /// <summary>
        /// Gets or sets the SID resolver to use for DirectoryService lookups.
        /// If null and EnableDirectoryServiceLookup is true, a default DirectoryServiceSidResolver will be used.
        /// </summary>
        public ISidResolver SidResolver { get; set; }
    }
}