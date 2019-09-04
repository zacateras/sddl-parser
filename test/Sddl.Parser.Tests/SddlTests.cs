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

@"Owner: Account Operators
Group: Domain Admins
Dacl:
  Ace[00]
    AceSid: Nobody
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
    AceSid: Local System
    AceType: ACCESS_ALLOWED
    Rights:
      FILE_ALL
  Ace[01]
    AceSid: Administrators
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
    AceSid: Guests
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    AceSid: Administrators
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    AceSid: Creator Owner
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    AceSid: Local System
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    AceSid: Users
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
"
                };

                yield return new object[]
                {
"O:BAG:SYD:PAI(D;OICI;FA;;;BG)(A;OICI;FA;;;BA)(A;OICIIO;FA;;;CO)(A;OICI;FA;;;SY)(A;OICI;FA;;;BU)S:AI(AU;OICINPFA;RPDTSDWD;;;BU)(AU;OICINPSA;CCSWRPDTLOSD;;;BU)",

@"Owner: Administrators
Group: Local System
Dacl:
  Flags: PROTECTED, AUTO_INHERITED
  Ace[00]
    AceSid: Guests
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    AceSid: Administrators
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    AceSid: Creator Owner
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    AceSid: Local System
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    AceSid: Users
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
Sacl:
  Flags: PROTECTED, AUTO_INHERITED
  Ace[00]
    AceSid: Guests
    AceType: ACCESS_DENIED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[01]
    AceSid: Administrators
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[02]
    AceSid: Creator Owner
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT, INHERIT_ONLY
    Rights:
      FILE_ALL
  Ace[03]
    AceSid: Local System
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
  Ace[04]
    AceSid: Users
    AceType: ACCESS_ALLOWED
    AceFlags: OBJECT_INHERIT, CONTAINER_INHERIT
    Rights:
      FILE_ALL
"
                };

                yield return new object[]
                {
"O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",

@"Owner: Domain Admins
Group: Domain Admins
Dacl:
  Ace[00]
    AceSid: Local System
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
    AceSid: Domain Admins
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
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aba-0de6-11d0-a285-00aa003049e2
  Ace[03]
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967a9c-0de6-11d0-a285-00aa003049e2
  Ace[04]
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: 6da8a4ff-0e52-11d0-a286-00aa003049e2
  Ace[05]
    AceSid: Print Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aa8-0de6-11d0-a285-00aa003049e2
  Ace[06]
    AceSid: Authenticated Users
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      LIST_CHILDREN
      READ_CONTROL
Sacl:
  Ace[00]
    AceSid: Local System
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
    AceSid: Domain Admins
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
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aba-0de6-11d0-a285-00aa003049e2
  Ace[03]
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967a9c-0de6-11d0-a285-00aa003049e2
  Ace[04]
    AceSid: Account Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: 6da8a4ff-0e52-11d0-a286-00aa003049e2
  Ace[05]
    AceSid: Print Operators
    AceType: OBJECT_ACCESS_ALLOWED
    Rights:
      CREATE_CHILD
      DELETE_CHILD
    ObjectGuid: bf967aa8-0de6-11d0-a285-00aa003049e2
  Ace[06]
    AceSid: Authenticated Users
    AceType: ACCESS_ALLOWED
    Rights:
      READ_PROPERTY
      LIST_CHILDREN
      READ_CONTROL
"
                };
            }
        }

        [MemberData(nameof(Two_Same_SddlObject_Should_Be_Equal_Data))]
        [Theory]
        public void Two_Same_Sddl_Object_Should_Be_Equal(string sddlString0, string sddlString1)
        {
            Sddl sddlObject0 = null;
            Sddl sddlObject1 = null;

            if (!string.IsNullOrEmpty(sddlString0))
            {
                sddlObject0 = new Sddl(sddlString0);
            }

            if (!string.IsNullOrEmpty(sddlString1))
            {
                sddlObject1 = new Sddl(sddlString1);
            }

            Assert.True(sddlObject0 == sddlObject1);
        }

        public static IEnumerable<object[]> Two_Same_SddlObject_Should_Be_Equal_Data
        {
            get
            {
                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCSDSWWOWD;;;SY)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "G:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "G:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "D:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "D:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAS:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAS:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    null,
                    null
                };
            }
        }

        [MemberData(nameof(Two_Different_SddlObject_Should_Not_Be_Equal_Data))]
        [Theory]
        public void Two_Different_Sddl_Object_Should_Not_Be_Equal(string sddlString0, string sddlString1)
        {
            Sddl sddlObject0 = null;
            Sddl sddlObject1 = null;

            if (!string.IsNullOrEmpty(sddlString0))
            {
                sddlObject0 = new Sddl(sddlString0);
            }

            if (!string.IsNullOrEmpty(sddlString1))
            {
                sddlObject1 = new Sddl(sddlString1);
            }

            Assert.True(sddlObject0 != sddlObject1);
        }

        public static IEnumerable<object[]> Two_Different_SddlObject_Should_Not_Be_Equal_Data
        {
            get
            {
                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    null
                };

                yield return new object[]
                {
                    null,
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aba-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;bf967a9c-0de6-11d0-a285-00aa003049e2;;AO)(OA;;CCDC;6da8a4ff-0e52-11d0-a286-00aa003049e2;;AO)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;DA)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "G:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAS:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };

                yield return new object[]
                {
                    "O:DAG:DAD:(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)",
                    "O:DAG:DAD:(A;;RPWPCCDCLCRCWOWDSDSW;;;SY)(OA;;CCDC;bf967aa8-0de6-11d0-a285-00aa003049e2;;PO)(A;;RPLCRC;;;AU)S:(AU;SAFA;WDWOSDWPCCDCSW;;;WD)"
                };
            }
        }
    }
}