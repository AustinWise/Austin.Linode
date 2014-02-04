Austin.Linode, a .NET library for the Linode API
-----------------------------------------------------------------

An easy use way to use the Linode API from C#.  While every API call is currently
implemented, only enough response to reboot or resize instance are implemented.

Dependencies
------------

 - .NET Framework 4.0 (It should compile just fine on .NET 3.5, a trivial amount of LINQ is used that would have to be removed to compile for .NET 2.0)
 - [Json.NET](http://json.codeplex.com/)


License
-------

Austin.Linode is licensed under a three-clause BSD license.


Areas for improvement
---------------------

 - Parse more API responses.
 - Batch requests.
