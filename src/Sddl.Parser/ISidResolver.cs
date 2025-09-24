namespace Sddl.Parser
{
    /// <summary>
    /// Interface for resolving SID strings to meaningful names
    /// </summary>
    public interface ISidResolver
    {
        /// <summary>
        /// Attempts to resolve a SID string to a meaningful name
        /// </summary>
        /// <param name="sidString">The SID string to resolve</param>
        /// <returns>The resolved name, or null if resolution fails</returns>
        string ResolveSid(string sidString);
    }
}