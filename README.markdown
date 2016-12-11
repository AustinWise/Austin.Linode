Austin.Linode, a .NET library for the Linode API
-----------------------------------------------------------------

An easy use way to use the Linode API from C#.  While every API call is currently
implemented, only enough response parsing to reboot or resize instance are implemented.

Install
-------

Available on Nuget as [Austin.Linode](https://www.nuget.org/packages/Austin.Linode/).

[![NuGet](https://img.shields.io/nuget/v/Austin.Linode.svg)](https://www.nuget.org/packages/Austin.Linode/)

Dependencies
------------

 - .NET Framework 4.0 (It should compile just fine on .NET 3.5, a trivial amount of LINQ is used that would have to be removed to compile for .NET 2.0)
 - [Newtonsoft Json.NET](http://www.newtonsoft.com/json)


License
-------

Austin.Linode is licensed under a three-clause BSD license.


Areas for improvement
---------------------

 - Parse more API responses.
 - Batch requests.
 - Better errors (like an error enumeration).
