# Highly Available Multi-Region Web Application

![Well-Archictected-Reliability - Reliability-Reference Architecture](https://user-images.githubusercontent.com/13979783/128624598-f75d633c-a2c8-40a5-b55f-609f128bd9db.png)

# Deployment References

### Sample Ecommerce App Source Code Repo  
https://github.com/dotnet-architecture/eShopOnWeb

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

