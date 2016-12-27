Austin.Linode, a .NET library for the Linode API
------------------------------------------------

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

Enhacement Ideas
----------------

 - Actually parse more API responses.
 - Batch requests.
 - Support more .NET platforms
   - Specifically, .NET Standard and older versions of .NET Framework. I'm
     waiting for the final .NET CLI tooling to ship, since it will change soon.
 - Better errors (like an error enumeration).

API Testing Status
------------------

This is a list of the different APIs that are tested by the IntegrationTest project.

- account
  - [ ] [account.estimateinvoice](https://www.linode.com/api/account/account.estimateinvoice)
  - [ ] [account.info](https://www.linode.com/api/account/account.info)
  - [ ] [account.paybalance](https://www.linode.com/api/account/account.paybalance)
  - [ ] [account.updatecard](https://www.linode.com/api/account/account.updatecard)
- avail
  - [x] [avail.datacenters](https://www.linode.com/api/utility/avail.datacenters)
  - [x] [avail.distributions](https://www.linode.com/api/utility/avail.distributions)
  - [x] [avail.kernels](https://www.linode.com/api/utility/avail.kernels)
  - [x] [avail.linodeplans](https://www.linode.com/api/utility/avail.linodeplans)
  - [ ] [avail.nodebalancers](https://www.linode.com/api/utility/avail.nodebalancers)
  - [ ] [avail.stackscripts](https://www.linode.com/api/utility/avail.stackscripts)
- domain
  - domain.resource
    - [ ] [domain.resource.create](https://www.linode.com/api/domain/domain.resource.create)
    - [ ] [domain.resource.delete](https://www.linode.com/api/domain/domain.resource.delete)
    - [ ] [domain.resource.list](https://www.linode.com/api/domain/domain.resource.list)
    - [ ] [domain.resource.update](https://www.linode.com/api/domain/domain.resource.update)
  - [ ] [domain.create](https://www.linode.com/api/domain/domain.create)
  - [ ] [domain.delete](https://www.linode.com/api/domain/domain.delete)
  - [ ] [domain.list](https://www.linode.com/api/domain/domain.list)
  - [ ] [domain.update](https://www.linode.com/api/domain/domain.update)
- image
  - [ ] [image.delete](https://www.linode.com/api/image/image.delete)
  - [ ] [image.list](https://www.linode.com/api/image/image.list)
  - [ ] [image.update](https://www.linode.com/api/image/image.update)
- linode
  - linode.config
    - [x] [linode.config.create](https://www.linode.com/api/linode/linode.config.create)
    - [ ] [linode.config.delete](https://www.linode.com/api/linode/linode.config.delete)
    - [x] [linode.config.list](https://www.linode.com/api/linode/linode.config.list)
    - [ ] [linode.config.update](https://www.linode.com/api/linode/linode.config.update)
  - linode.disk
    - [ ] [linode.disk.create](https://www.linode.com/api/linode/linode.disk.create)
    - [x] [linode.disk.createfromdistribution](https://www.linode.com/api/linode/linode.disk.createfromdistribution)
    - [ ] [linode.disk.createfromimage](https://www.linode.com/api/linode/linode.disk.createfromimage)
    - [ ] [linode.disk.createfromstackscript](https://www.linode.com/api/linode/linode.disk.createfromstackscript)
    - [x] [linode.disk.delete](https://www.linode.com/api/linode/linode.disk.delete)
    - [ ] [linode.disk.duplicate](https://www.linode.com/api/linode/linode.disk.duplicate)
    - [ ] [linode.disk.imagize](https://www.linode.com/api/linode/linode.disk.imagize)
    - [x] [linode.disk.list](https://www.linode.com/api/linode/linode.disk.list)
    - [ ] [linode.disk.resize](https://www.linode.com/api/linode/linode.disk.resize)
    - [ ] [linode.disk.update](https://www.linode.com/api/linode/linode.disk.update)
  - linode.ip
    - [ ] [linode.ip.addprivate](https://www.linode.com/api/linode/linode.ip.addprivate)
    - [ ] [linode.ip.addpublic](https://www.linode.com/api/linode/linode.ip.addpublic)
    - [ ] [linode.ip.list](https://www.linode.com/api/linode/linode.ip.list)
    - [ ] [linode.ip.setrdns](https://www.linode.com/api/linode/linode.ip.setrdns)
    - [ ] [linode.ip.swap](https://www.linode.com/api/linode/linode.ip.swap)
  - [x] [linode.boot](https://www.linode.com/api/linode/linode.boot)
  - [ ] [linode.clone](https://www.linode.com/api/linode/linode.clone)
  - [x] [linode.create](https://www.linode.com/api/linode/linode.create)
  - [x] [linode.delete](https://www.linode.com/api/linode/linode.delete)
  - [x] [linode.job.list](https://www.linode.com/api/linode/linode.job.list)
  - [ ] [linode.kvmify](https://www.linode.com/api/linode/linode.kvmify)
  - [x] [linode.list](https://www.linode.com/api/linode/linode.list)
  - [ ] [linode.mutate](https://www.linode.com/api/linode/linode.mutate)
  - [ ] [linode.reboot](https://www.linode.com/api/linode/linode.reboot)
  - [ ] [linode.resize](https://www.linode.com/api/linode/linode.resize)
  - [x] [linode.shutdown](https://www.linode.com/api/linode/linode.shutdown)
  - [x] [linode.update](https://www.linode.com/api/linode/linode.update)
  - [ ] [linode.webconsoletoken](https://www.linode.com/api/linode/linode.webconsoletoken)
- nodebalancer
  - nodebalancer.config
    - [ ] [nodebalancer.config.create](https://www.linode.com/api/nodebalancer/nodebalancer.config.create)
    - [ ] [nodebalancer.config.delete](https://www.linode.com/api/nodebalancer/nodebalancer.config.delete)
    - [ ] [nodebalancer.config.list](https://www.linode.com/api/nodebalancer/nodebalancer.config.list)
    - [ ] [nodebalancer.config.update](https://www.linode.com/api/nodebalancer/nodebalancer.config.update)
  - nodebalancer.node
    - [ ] [nodebalancer.node.create](https://www.linode.com/api/nodebalancer/nodebalancer.node.create)
    - [ ] [nodebalancer.node.delete](https://www.linode.com/api/nodebalancer/nodebalancer.node.delete)
    - [ ] [nodebalancer.node.list](https://www.linode.com/api/nodebalancer/nodebalancer.node.list)
    - [ ] [nodebalancer.node.update](https://www.linode.com/api/nodebalancer/nodebalancer.node.update)
  - [ ] [nodebalancer.create](https://www.linode.com/api/nodebalancer/nodebalancer.create)
  - [ ] [nodebalancer.delete](https://www.linode.com/api/nodebalancer/nodebalancer.delete)
  - [ ] [nodebalancer.list](https://www.linode.com/api/nodebalancer/nodebalancer.list)
  - [ ] [nodebalancer.update](https://www.linode.com/api/nodebalancer/nodebalancer.update)
- stackscript
  - [ ] [stackscript.create](https://www.linode.com/api/stackscript/stackscript.create)
  - [ ] [stackscript.delete](https://www.linode.com/api/stackscript/stackscript.delete)
  - [ ] [stackscript.list](https://www.linode.com/api/stackscript/stackscript.list)
  - [ ] [stackscript.update](https://www.linode.com/api/stackscript/stackscript.update)
- [x] [api.spec](https://www.linode.com/api/utility/api.spec)
- [ ] [test.echo](https://www.linode.com/api/utility/test.echo)
- [ ] [user.getapikey](https://www.linode.com/api/user/user.getapikey)
