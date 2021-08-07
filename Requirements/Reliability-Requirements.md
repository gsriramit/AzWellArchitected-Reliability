# Introduction
Availability targets and recovery targets allow you to measure the uptime and downtime of your workloads. Having clearly defined targets is crucial in order to have a goal to work and measure against. In addition to these targets, there are many other requirements you should consider to improve reliability requirements and meet business expectations.
Building resiliency (recovering from failures) and availability (running in a healthy state without significant downtime) into your apps begins with gathering requirements. 
### Questions
- How much downtime is acceptable?
- How much does potential downtime cost your business?
- What are your customer's availability requirements ?
- How much do you invest in making your application highly available?
- What is the risk versus the cost?

# Reliability Requirements

## Availability Targets
1. **SLI** - Service Level Indicator can be derived from Metrics Over Time. This is often indicated using the percentile system. 95th or the 99th percentile. 95th percentile is the highest value out of the values that are left after eliminating the top 5%. The top 5% is identified after the numbers are sorted either in ascending or the descending order.
	- **Availability Requirement** - The Availability of the system (assumed) or the uptime should be *99.99%*
    - The requirement translates to an affordable downtime of *4.32 minutes/month* or approximately *0.144* minutes per day. 
    - The availability of the customer facing application can be used for the SLI calculation
    - The availability at the 95th percentile can be calculated after determining the availability of the app per 24 hours for 30-31 days
2. **SLO** - Service Level Objective is a binding target for a collection of SLIs. Objective of achieving the targeted SLI over a longer period of time, say over an year. 
	- Requirement : The availability of the customer facing app should be 99.99% when calculated over the monthly SLI data points over an year  
3. **SLA** -Service Level Agreement. An agreement of what the service provider guarantees if the service fails to provide the promised SLO. This can be a refund of money or free credits etc.,
	(Example: Service Credits if the 95th percentile homepage SLI succeeds less than 99.5% over trailing year)
4. **Error Budget** - Representation of the SLO of a service in minutes/month
	- 99.99% of uptime/month would mean 0.01% of downtime/month that is affordable
	- 0.01% = 0.0001 for 30 or 31 days (Or) 0.0001 * 30 * 24 * 60 = 4.32 mins/month  
	- **Note**: The chaos experiments are supposed to be run in production eventually. This means that the outage or system down time (if created) created from these experiments would borrow from the 43.2 minutes of acceptable downtime per month.
	    Care should be taken not to exhaust the error budget which would leave us no room to tolerate real time production incidents.


## Recovery Targets
1. **Recovery Time Objective (RTO)** — the maximum acceptable time an application is unavailable after an incident 
    - **Requirement** - The maximum acceptable recovery time of the primary Customer App after an incident cannot exceed 5 minutes
    - The maximum acceptable recovery time of the affected database instances cannot exceed 30 seconds
2. **Recovery Point Objective (RPO)** — the maximum duration of data loss that is acceptable during a disaster
    - **Requirement** - The maximum acceptable data loss of the customer's Identity and ECommerce Application Database cannot exceed 5 seconds

## Directly Impacted SLI & SLO of System's Performance
When the system's availability is challenged, the design should be dictating a suitable mechanism to continue to serve the user requests. The experience can be made minimal until the system is restored back to its 100% working state, application components can retry or take an alternate path to serve the end-users for a limited period of time or display an error message and stop supporting the impacted user flow/transaction.
However, in this challenged state, there would be other SLIs/SLOs of the system that would be affected directly.
1. An example can be the response time as a performance indicator. If the system is not available or runs in a degraded state, the **Response Time SLO** can exceed the allowed limits very quickly and we would start exhausting the error budget for that specific metric.

It is important to keep the other KPIs of the system when designing for Availability and Recovery. 

## Azure Recovery Options
| **Deployment type** | **BCDR technology** |**Expected RTO for SQL Server** |**Expected RPO for SQL Server**|
|-----------------|-----------------|----------------------------|---------------------------|
|SQL Server on an Azure infrastructure as a service (IaaS) virtual machine (VM) or at on-premises.|Always On availability group|The time taken to make the secondary replica as primary.|Because replication to the secondary replica is asynchronous, there's some data loss.|
|SQL Server on an Azure IaaS VM or at on-premises.|Failover clustering (Always On FCI)|The time taken to fail over between the nodes.|Because Always On FCI uses shared storage, the same view of the storage instance is available on failover.|
|SQL Server on an Azure IaaS VM or at on-premises.|Replication with Azure Site Recovery|RTO is typically less than 15 minutes. To learn more, read the RTO SLA provided by Site Recovery.|One hour for application consistency and five minutes for crash consistency. If you are looking for lower RPO, use other BCDR technologies.|

Refer to [Set up disaster recovery for SQL Server](https://docs.microsoft.com/en-us/azure/site-recovery/site-recovery-sql) for the DR options available for SQL DBs hosted in Azure (IAAS and PAAS)




