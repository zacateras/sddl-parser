namespace Sddl.Parser.Console
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            SecurableObjectType type = SecurableObjectType.Unknown;
            string sddlString;

            switch (args.Length)
            {
                case 2:
                    if (Enum.TryParse(typeof(SecurableObjectType), args[1], out var value))
                    {
                        type = (SecurableObjectType)value;
                        goto case 1;
                    }
                    else
                        goto default;
                case 1:
                    sddlString = args[0];
                    break;
                default:
                    Usage();
                    return;
            }

            var sddl = new Sddl(sddlString, type);

            Console.WriteLine(sddl.ToString());
        }

        private static void Usage()
        {
            string securableAlternative = string.Join(" | ", Enum.GetNames(typeof(SecurableObjectType)));
            Console.WriteLine($"Usage: ./Sddl.Parser.Console.exe \"O:BAG:BAD:(A;CI;CCDCRP;;;NS)\" [{securableAlternative}]");
        }
    }
}
