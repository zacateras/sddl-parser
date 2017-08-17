using System;
using Xunit;

namespace Sddl.Parser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void Test1()
        {
            var parser = new Sddl.Parser.Parser();

            var sddlInfo = parser.Parse("O:AOG:DAD:(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)");

            Console.WriteLine(sddlInfo);
        }
    }
}
