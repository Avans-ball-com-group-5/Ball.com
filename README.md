# Ball.com

## Project Folder Structure:
1. Make a solution per service.
2. Add a folder for each service.
3. Use proper feature branching.
4. Delete feature branch on merge with dev, no fast forward.
5. Merge Dev to main with a pull request.

## Setting up a masstransit consumer:
1. Add the following nuget packages into the project where you want to create a consumer:
```
    <PackageReference Include="MassTransit" Version="8.2.2" />
    <PackageReference Include="MassTransit.Abstractions" Version="8.2.2" />
    <PackageReference Include="MassTransit.AspNetCore" Version="7.3.1" />
    <PackageReference Include="MassTransit.Extensions.DependencyInjection" Version="7.3.1" />
    <PackageReference Include="MassTransit.Newtonsoft" Version="8.2.2" />
    <PackageReference Include="MassTransit.RabbitMQ" Version="8.2.2" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
```
1. Add a class that implements the IConsumer interface.
2. Add a class that implements the ConsumerDefinition class.
2. Add the consumer to the ConfigureBusEndpoints method in the Program.cs file.
4. Profit.
5. You can use a backgroundservice to check if the consumer works. For more info check the BusSenderBackgroundService in the CustomerService
#### For more info check Customer Consumer in the CostumerService.

## Setting up docker to run services locally:
1. Install Docker Desktop.
2. Pull the masstransit/rabbitmq image(latest is not fine just pull masstransit/rabbitmq).
3. Run the image with the following command (or in docker desktop, either is fine):
```
docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```
4. Go to localhost:15672 in your browser and log in with guest/guest.
5. See incoming messages in the Queues tab.
6. If they land in skipped or error something is going wrong, use the backgroundservice to test this, works better than injecting via rabbitMq manager

### RabbitMq & Masstransit quick guide:
https://masstransit.io/quick-starts/rabbitmq

# Project Requirements and Description:
Customer: Ball.com, a retailer going global.

## The different processes as described by various stakeholders:

### 1. A customer places an order from a pre-determined set of products. 
### 2. An order can contain anywhere from one to twenty different items.
### 3. The items for the order are picked from a large warehouse and packaged together.
### 4. The order package is sent to the customer using a logistics company. 
### 5. The logistics company allowed to deliver the package is selected by lowest price.
### 6. Trusted third-party suppliers are allowed to add their products to the pre-determined set of products.
### 7. The customer can keep track of the status of its order.
### 8. A customer is allowed to pay using either a forward- or after-pay system.
### 9. The service department answers all customer’s questions. 
