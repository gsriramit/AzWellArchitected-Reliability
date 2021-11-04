# Composite SLA
A composite SLA captures the end-to-end SLA across all application components and dependencies. It is calculated using the individual SLAs of Azure services housing application components and provides an important indicator of designed availability in relation to customer expectations and targets. Make sure the composite SLA of all components and dependencies on the critical paths are understood. 
The [Service Level Agreement Estimator](https://github.com/mspnp/samples/tree/master/Reliability/SLAEstimator) shows how to calculate the SLA of your architecture.
Composite SLA of the system (Single Region) = SLA of Component1 * SLA of Component 2 and so on
The composite SLA for a multiregion deployment is calculated as follows:

N is the composite SLA for the application deployed in one region.
R is the number of regions where the application is deployed.
The expected chance that the application fails in all regions at the same time is ((1 − N) ^ R). For example, if the single-region SLA is 99.95%:

The combined SLA for two regions = (1 − (1 − 0.9995) ^ 2) = 99.999975%
The combined SLA for four regions = (1 − (1 − 0.9995) ^ 4) = 99.999999%

# Dependency Mapping & Composite SLA 
User Flow: Read data for the catalog page and write data for item checkout

| Dependency Name                          | Traffic Manager                    | Azure DNS     | Azure Application Gateway                                | Virtual Machine ScaleSet (Web-Tier)                       | Internal Load Balancer (L4)                          | Virtual Machines for SQL Server (DB-Tier)                                       | Domain Controllers                                                     |
|------------------------------------------|------------------------------------|---------------|----------------------------------------------------------|-----------------------------------------------------------|------------------------------------------------------|---------------------------------------------------------------------------------|------------------------------------------------------------------------|
| **Dependency Criticality**                   | Critical                           | Critical      | Critical                                                 | Critical                                                  | Critical                                             | Critical                                                                        | Critical                                                               |
| **Standalone SLO/SLA**                       | 99.99%                             | 100%          | 99.95%                                                   | 99.99%                                                    | 99.99%                                               | 99.99%                                                                          | 99.99%                                                                 |
| **SPOF?**                                    | No                                 | No            | No                                                       | No                                                        |                                                      | No                                                                              | No                                                                     |
| **Design Considerations**                    | (Azure Managed Cluster of DNS LBs) | Azure Managed | (Highly-Available v2 gateway SKU that is zone-redundant) | Minimum of 2 instances deployed across Availability Zones | Highly available Zone-Redundant LB from Standard SKU | Windows Server Failover Cluster Instances with SQL Always-On Availability Group | Highly-Available Zone redundant Domain controllers hosted on Azure VMs |
| **Composite SLA (Single Region Deployments** | **99.90%**                             |               |                                                          |                                                           |                                                      |                                                                                 |                                                                        |
| **Composite SLA (Multi- Region Deployments** | **99.999900%**                         |

# Microsoft Resiliency Modelling Analysis Diagram


![RMA](/Design/DependencyAnalysis/Images/FMEA_RMA.png)
