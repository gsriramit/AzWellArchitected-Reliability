# System's Availability Baselining
This step in the process of designing and deploying reliable systems involves examining the system for a couple of key availability metrics, **Service Level Indicator(SLI)** and the **Service Level Objective(SLO)**.  
Once the system has been designed(using the Design principles and the checklist), we would be ready to deploy a version of the system in the DEV environment. After the system has been deployed to the DEV environment in a single region, we can
examine the system for the the SLI and SLA.  
  
**Note**: This process is in line with [Google's SRE principles](https://sre.google/workbook/implementing-slos)  
Excerpt from the chapter that talks about implementing SLOs
## Modeling User Journeys
>While all of the techniques discussed in this chapter will be beneficial to your organization, ultimately SLOs should center on improving the customer experience. Therefore, you should write SLOs in terms of user-centric actions.  
>You can use critical user journeys to help capture the experience of your customers.  

Following are the steps that performed in the examining the system's availability.
