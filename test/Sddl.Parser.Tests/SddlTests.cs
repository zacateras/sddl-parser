using System;
using System.Collections.Generic;
using Xunit;

namespace Sddl.Parser.Tests
{
    public class SddlTests
    {
        [MemberData(nameof(SddlString_Should_Be_Parsed_And_Printed_As_Expected_Data))]
        [Theory]
        public void SddlString_Should_Be_Parsed_And_Printed_As_Expected(string sddlString, string expectedOutput)
        {
            // Arrange

            // Act
            var sddl = new Sddl(sddlString);
            var actualOutput = sddl.ToString();

            // Assert
            actualOutput = actualOutput.Replace("\r\n", "\n");
            expectedOutput = expectedOutput.Replace("\r\n", "\n");

            Assert.Equal(expectedOutput, actualOutput);
        }

        public static IEnumerable<object[]> SddlString_Should_Be_Parsed_And_Printed_As_Expected_Data
        {
            get
            {
                yield return new object[]
                {
"D:(A;;0x000f003f;;;)",

@"Dacl:
  Ace[00]
    AceType: ACCESS_ALLOWED
    Rights:
      WRITE_OWNER
      WRITE_DAC
      READ_CONTROL
      DELETE
      Unknown(0x3F)
"
                };

                yield return new object[]
                {
"O:AOG:DAD:(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)",

@"Owner: ACCOUNT_OPERATORS
Group: DOMAIN_ADMINISTRATORS
Dacl:
  Ace[00]
    Account: Null Authority
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      WRITE_PROPERTY
      CREATE_CHILD
      DELETE_CHILD
      LIST_CHILDREN
      SELF_WRITE
      READ_CONTROL
      WRITE_DAC
      WRITE_OWNER
      GENERIC_ALL
"
                };

                yield return new object[]
                {
"D:(A;;FA;;;SY)(A;;FA;;;BA)",

@"Dacl:
  Ace[00]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    Rights:
      FILE_ALL
  Ace[01]
    Account: BUILTIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    Rights:
      FILE_ALL
"
                };

                yield return new object[]
                {
"D:PAI(D;OICI;FA;;;BG)(A;OICI;FA;;;BA)(A;OICIIO;FA;;;CO)(A;OICI;FA;;;SY)(A;OICI;FA;;;BU)",

@"Dacl:
  Flags: PROTECTED, AUTO_INHERITED
  Ace[00]
    Account: BUILTIN_GUESTS
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    Account: BUILTIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    Account: CREATOR_OWNER
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    Account: BUILTIN_USERS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
"
                };

                yield return new object[]
                {
"O:BAG:SYD:PAI(D;OICI;FA;;;BG)(A;OICI;FA;;;BA)(A;OICIIO;FA;;;CO)(A;OICI;FA;;;SY)(A;OICI;FA;;;BU)S:AI(AU;OICINPFA;RPDTSDWD;;;BU)(AU;OICINPSA;CCSWRPDTLOSD;;;BU)",

@"Owner: BUILTIN_ADMINISTRATORS
Group: LOCAL_SYSTEM
Dacl:
  Flags: PROTECTED, AUTO_INHERITED
  Ace[00]
    Account: BUILTIN_GUESTS
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    Account: BUILTIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    Account: CREATOR_OWNER
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    Account: BUILTIN_USERS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
Sacl:
  Flags: PROTECTED, AUTO_INHERITED
  Ace[00]
    Account: BUILTIN_GUESTS
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    Account: BUILTIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    Account: CREATOR_OWNER
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    Account: BUILTIN_USERS
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
"
                };

                yield return new object[]
                {
"O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",

@"Owner: DOMAIN_ADMINISTRATORS
Group: DOMAIN_ADMINISTRATORS
Dacl:
  Ace[00]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      WRITE_PROPERTY
      CREATE_CHILD
      DELETE_CHILD
      LIST_CHILDREN
      READ_CONTROL
      WRITE_OWNER
      WRITE_DAC
      STANDARD_DELETE
      SELF_WRITE
  Ace[01]
    Account: DOMAIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      WRITE_PROPERTY
      CREATE_CHILD
      DELETE_CHILD
      LIST_CHILDREN
      READ_CONTROL
      WRITE_OWNER
      WRITE_DAC
      STANDARD_DELETE
      SELF_WRITE
  Ace[02]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aba-0de6-11d0-a285-00aa003049e2
  Ace[03]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967a9c-0de6-11d0-a285-00aa003049e2
  Ace[04]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: 6da8a4ff-0e52-11d0-a286-00aa003049e2
  Ace[05]
    Account: PRINTER_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aa8-0de6-11d0-a285-00aa003049e2
  Ace[06]
    Account: AUTHENTICATED_USERS
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      LIST_CHILDREN
      READ_CONTROL
Sacl:
  Ace[00]
    Account: LOCAL_SYSTEM
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      WRITE_PROPERTY
      CREATE_CHILD
      DELETE_CHILD
      LIST_CHILDREN
      READ_CONTROL
      WRITE_OWNER
      WRITE_DAC
      STANDARD_DELETE
      SELF_WRITE
  Ace[01]
    Account: DOMAIN_ADMINISTRATORS
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      WRITE_PROPERTY
      CREATE_CHILD
      DELETE_CHILD
      LIST_CHILDREN
      READ_CONTROL
      WRITE_OWNER
      WRITE_DAC
      STANDARD_DELETE
      SELF_WRITE
  Ace[02]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aba-0de6-11d0-a285-00aa003049e2
  Ace[03]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967a9c-0de6-11d0-a285-00aa003049e2
  Ace[04]
    Account: ACCOUNT_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: 6da8a4ff-0e52-11d0-a286-00aa003049e2
  Ace[05]
    Account: PRINTER_OPERATORS
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aa8-0de6-11d0-a285-00aa003049e2
  Ace[06]
    Account: AUTHENTICATED_USERS
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      LIST_CHILDREN
      READ_CONTROL
"
                };
            }
        }
    }
}
