# CTeleport.FlightWrapper

This project demonstrates establish a .NET-based microservices architecture that can be quickly and very easily scaled and adapted to client requirements.
We did not want to have to make any changes to the code or settings of individual services but control the system just by orchestrating containers in Docker.

The result is a simple microservices architecture that can be easily scaled with just a few changes in container settings. The scaling of 
the application is handled by two open-source components: Ocelot, which is a gateway and load balancer, and HashiCorp Consul*, the identity-based network 
service which acts as a service discovery agent.

On the other hand we are going to look at one of the key challenges with building microservices: resilience. 
