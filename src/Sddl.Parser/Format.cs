using System;
using System.Collections.Generic;

namespace Sddl.Parser
{
    internal static class Format
    {
        public static string Unknown(string input)
        {
            const string UnknownString = "Unknown({0})";
            return string.Format(UnknownString, input);
        }

        public static string Indent(string input)
        {
            const string IndentString = "  ";
            return IndentString + input.Replace("\n", $"\n{IndentString}");
        }
    }
}
