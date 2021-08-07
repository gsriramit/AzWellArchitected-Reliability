# System Dependency Maps
The system needs to be equipped to identify its dependencies at the
- Network Level
  - Azure Monitor helps in identifying all the network level dependencies (or infrastructure dependencies if I may) through the "Service Maps" feature. Once all the infrastructure components are deployed, "Azure Monitor for Virtual Machines" helps in installing the **Dependency Agent** in the onboarded VMs.
After allowing sufficient time for the onboarded VMs to push out the network related logs and mterics, Azure plots a graph or a map of all the dependecies of each of the VM/Vmss in the architecture.
An end to end graph would sometimes be helpful in better visualizing the system's dependecies as a whole and also confirm the **criticality of the dependencies** identified in the previous step
- Application Level
  - For all the web-based apps (customer-facing or otherwise), Azure Application Insights helps in determining the dependecies and creating an "Application Dependency Map" 

# Azure Monitor Service Maps

## WebServer dependencies
![WebServer Dependency Graph](/Reliability/Design/DependencyAnalysis/Images/DependencyMap-WebServers.png)

## SQL Server Always on Availability Group
![SQL Server Dependency Graph](/Reliability/Design/DependencyAnalysis/Images/DependencyMap-SQL-PrimaryReplica.png)

## Application Dependency Map (from App-Insights)
![SQL Server Dependency Graph](/Reliability/Design/DependencyAnalysis/Images/ApplicationDependencyMap.png)

