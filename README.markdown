Austin.Linode, a .NET library for the Linode API
-----------------------------------------------------------------

An easy use way to use the Linode API from C#.  Currently only implements enough
api actions to reboot or resize a node.  It's pretty easy to add new ones though.

Dependencies
------------

 - .NET Framework 4.0 (Actully should be fine to use on the 2.0 runtime, you will just need to recompile and get the 2.0 version of Json.NET)
 - [Json.NET](http://json.codeplex.com/)


License
-------

Austin.Linode is licensed under a three-clause BSD license.


Areas for improvement
---------------------

 - Currently limited number of API actions implemented.
 - Batch requests.
