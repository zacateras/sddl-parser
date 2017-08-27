using System;
using Xunit;

namespace Sddl.Parser.Tests
{
    public class SddlTests
    {
        [Fact]
        public void Test1()
        {
            var sddl = new Sddl("O:AOG:DAD:(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)");

            Console.WriteLine(sddl);
        }
    }
}
