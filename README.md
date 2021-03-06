[![Build status](https://ci.appveyor.com/api/projects/status/tvaoqbbbkchfnp3l?svg=true)](https://ci.appveyor.com/project/zacateras/sddl-parser) [![NuGet](https://img.shields.io/nuget/v/Sddl.Parser)](https://nuget.org/packages/Sddl.Parser)

## SDDL Parser
The console application and underlying nuget library for parsing [SDDL (Security Descriptor Design Language)](https://docs.microsoft.com/en-us/windows/win32/secauthz/security-descriptor-definition-language).

### Console application
```
Usage: ./Sddl.Parser.Console.exe "O:BAG:BAD:(A;CI;CCDCRP;;;NS)" [Unknown | File | Directory | Pipe | Process | Thread | FileMappingObject | AccessToken | WindowsManagementObject | RegistryKey | WindowsService | LocalOrRemotePrinter | NetworkShare | Event | Mutex | Semaphore | Timer | JobObject | DirectoryServiceObject]
```

#### Examples
```
$ ./Sddl.Parser.Console.exe "O:BAG:BAD:(A;CI;CCDCRP;;;NS)"

Owner: Administrators
Group: Administrators
Dacl:
  Ace[00]
    AceSid: Network Service
    AceType: ACCESS_ALLOWED
    AceFlags: CONTAINER_INHERIT
    Rights:
      CREATE_CHILD
      DELETE_CHILD
      READ_PROPERTY


$ ./Sddl.Parser.Console.exe "D:PAI(D;OICI;FA;;;BG)(A;OICI;FA;;;BA)(A;OICIIO;FA;;;CO)(A;OICI;FA;;;SY)(A;OICI;FA;;;BU)"

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
```