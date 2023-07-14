# CTeleport.FlightWrapper


#SCALABLE MICROSERVICES ARCHITECTURE WITH .NET#
This project demonstrates establish a .NET-based microservices architecture that can be quickly and very easily scaled and adapted to client requirements.
We did not want to have to make any changes to the code or settings of individual services but control the system just by orchestrating containers in Docker.

The result is a simple microservices architecture that can be easily scaled with just a few changes in container settings. The scaling of 
the application is handled by two open-source components: Ocelot, which is a gateway and load balancer, and HashiCorp Consul*, the identity-based network 
service which acts as a service discovery agent.

#RESILIENT MICROSERVICES in .NET WITH POLLY
On the other hand we are going to look at one of the key challenges with building microservices: resilience. 

SOLUTION STRUCT *******************************************************************************

	Clean code with clear naming
	Comments and ReadMe
	Work with asynchronous
	HttpClient implementation and additional settings
	Work with checking of incoming parameters
	Work with dependency injection

	-------------------------------------------------------------------------------------------

	Added FluentValidation 

	Added GenerateDocumentationFile to show comments on swagger UI

	Added Exceptionhandling middleware

	Added Serilog writing file/console
CACHING *****************************************************************************************

	Added Simply MemoryCaching for httpclient response : Scrutor 

	Establish distributed caching services------------------------------------------------------


UNIT TESTING*************************************************************************************

	Added UnitTest for Controllers / Services with Moq

RESILIENT*****************************************************************************************

	Added Polly  WaitAndRetryPolicy, CircuitBreakerPolicy, FallbackPolicy

	Added AspNetCoreRateLimiting (demonstrate DDOS attack)

	!!!Throttling is a particular process of applying rate-limiting to an API endpoint.

	!!!Queues the incoming requests, then serves them to the API endpoint at a rate that the API can process gracefully.

SCALING*******************************************************************************************

	Added Redis DistributedCache

	docker activation => <projectname>.csproj.user :  <ActiveDebugProfile>Docker</ActiveDebugProfile>
