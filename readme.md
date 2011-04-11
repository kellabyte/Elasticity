# What is Elasticity?
Elasticity is an implementation of the [Scheduler-Agent-Supervisor](http://vasters.com/clemensv/CommentView,guid,83f937f7-b838-43d0-ad61-74605eceafa2.aspx) pattern as blogged by Clemens Vasters. The goals for Elasticity is to build a solid foundation for facilitating scalable and resilient services.

The idea is you would run the Schedulers and Supervisor on any machine you wish and you would run as many instances of Agents as you require for handling various tasks. The scheduler accepts jobs (with many tasks) and distributes them to the Agents who know how to handle these tasks. There is more to it than that but that is a simple example.

You would implement the parts that push jobs & tasks into the Scheduler and then implement your Agents to execute the tasks. This framework will help handle the infrastructure requires to scale while being resilient to faults.

## Status
Elasticity is brand new and I am learning the pieces as I put it together. Currently this is very much a work in progress. This document will change as the project gets on it's feet and is functional. 

Right now the code base is in flux as I transition to a DDD/CQRS architecture. For anyone wanting to study my DDD/CQRS work please ignore anything in the root of the project and pay attention to the following directories:

* \CommandHandlers
* \Commands
* \Domain
* \Events

## Unit Tests
Please read Tests\Elasticity.Tests\DomainTests.cs for tests against the domain. It's early. Give me some time :)

## Ideas in the bucket (not guaranteed)
Some of the ideas I have for Elasticity is as follows:

* Provide a default implementation for queues, data storage and communications.
* Modular approach to queues, data and communications so that they are extensible to support whatever people building services wish to use and not force technology choice.
* Elasticity will be designed with Event Sourcing at the base so every service built gets Event Sourcing for free. The current plan is to implement DDD/CQRS.
* Replaying of events from the event data store.
* Schedulers, Supervisors and Agents will use Cassandra for auto-discovery making the Scheduler capable of dynamically detecting Agents.
* Schedulers, Supervisors and Agents will Cassandra for no single point of failure and to distribute the event store.