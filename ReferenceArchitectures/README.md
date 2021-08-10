# Highly Available Multi-Region Web Application

![Well-Archictected-Reliability - Reliability-Reference Architecture](https://user-images.githubusercontent.com/13979783/128624598-f75d633c-a2c8-40a5-b55f-609f128bd9db.png)

# Deployment References

### Sample Ecommerce App Source Code Repo  
https://github.com/dotnet-architecture/eShopOnWeb
This is an Asp.Net 5.0 application that is available is both the monolithic and microservices flavors(using docker containers) in Microsoft's Azure GitHub Repo. To keep things simple, we will be using the monolith flavor of the application. The application can be packaged and deployed to the VM scalesets. As the scalesets need a golden image to be provided as a part of the ARM template, an image with the web application and application insights needs to be created.  
These are the steps that I used to avoid redundant manual work
1. Create a separate VM (Windows Server 2016) that will serve as the application staging VM
2. Complete the environment settings in the machine
   - Install the Internet Information services (IIS) as a Windows Server Feature
   - Create a separate website for the target application
   - Deploy to the target machines IIS either from Visual Studio (VS 2019 has this cool feature of allowing deployments to Azure Virtual Machines from the IDE after you have authenticated and signed into the appropriate subscription) (OR)
   - Create a separate DevOps pipeline that builds and deploys the app to the target
   - Install the .Net Core Hosting Bundle  
   https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-5.0
3. Modify the connection strings in app-settings if you intend on reading these securely from key vault
   - The connection strings will have to be modified at this point to swithc from the in-memory DB to the SQL Always on Availability group connection string
5. Test the connectivity of the application to the Database
6. Create a snapshot of the Virtual Machine
7. Create a Managed disk from this snapshot
8. Create a new VM from this Managed Image
9. Sysprep the new VM using the Generalize settings. Note: This makes the VM disk not usable anymore.
   - Note: This creates a managed image. This is an Azure Resource recognized by the Azure Resource Manager.
   - If this image needs to be shared across multiple teams then the image can be saved to a Shared image Gallery and consumed from there
11. Use this managed image for the creation of the VMSS. This ensures that when the scaleset scales out the newly created instance will have exactly the setup that is required for our application to continue to function with negligle to minimal latency (only what is required to spin a new a VM from the base image) 

![Well-Archictected-Reliability - Scalesets-BaseImage](https://user-images.githubusercontent.com/13979783/128895595-b392253f-8337-4f86-b20d-4287faf27449.png)

## Deploying WebApp and creating base images through GitHub action
The following repo provides a more automated way to achieve the creation of the base image for the VM Scale Sets  
https://github.com/Azure/build-vm-image#end-to-end-sample-workflows

### VMSS Zonal and Zone Redundant Deployments  
https://github.com/Azure/vm-scale-sets/tree/master/preview/zones  
Vmss fronted by load balancers and options to execute custom script  
https://github.com/Azure/azure-quickstart-templates/blob/master/201-vmss-custom-script-windows/azuredeploy.json

### Prerequisites before creating a WSFC  
https://docs.microsoft.com/en-us/windows-server/failover-clustering/create-failover-cluster#verify-the-prerequisites

### Creating a SQL Always On - Step by Step  
https://techcommunity.microsoft.com/t5/itops-talk-blog/step-by-step-creating-a-sql-server-always-on-availability-group/ba-p/648772

### Configuring a listener for a SQL Always-On (the next 2 articles have information on a multi-subnet AG)  
https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server?view=sql-server-ver15  

https://www.sqlservercentral.com/articles/sql-server-always-on-availability-group-ag-listener-step-by-step-guide  

### Creating an Always ON AG and attaching DBs to it through PowerShell  
https://www.mssqltips.com/sqlservertip/5012/create-and-configure-sql-server-2016-always-on-availability-groups-using-windows-powershell/

### Configuring an Azure ILB that will use the AG Listener's Static IP as its Private IP Address  
https://docs.microsoft.com/en-us/azure/azure-sql/virtual-machines/windows/availability-group-load-balancer-portal-configure

### Testing: Connecting to SQL AG using different connecting string formats
https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/listeners-client-connectivity-application-failover?view=sql-server-ver15

