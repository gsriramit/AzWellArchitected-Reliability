# System's Availability Baselining
This step in the process of designing and deploying reliable systems involves examining the system for a couple of key availability metrics, **Service Level Indicator(SLI)** and the **Service Level Objective(SLO)**.  
Once the system has been designed(using the Design principles and the checklist), we would be ready to deploy a version of the system in the DEV environment. After the system has been deployed to the DEV environment in a single region, we can examine the system for the the SLI and SLA and hence assert the design decisions.

**Note**: This process is in line with [Google's SRE principles](https://sre.google/workbook/implementing-slos)  
Following is an excerpt from the chapter that talks about implementing SLOs
>### Modeling User Journeys
>While all of the techniques discussed in this chapter will be beneficial to your organization, ultimately SLOs should center on improving the customer experience. Therefore, you should write SLOs in terms of user-centric actions.  
>You can use critical user journeys to help capture the experience of your customers.  

## System Setup
To simplify things, this specific exercise was alone performed using Azure Managed Services- Azure App Service for the web application and Azure SQL DB(Serverless) for the application database

![SLICalculationApplicationMap](https://user-images.githubusercontent.com/13979783/128731610-d75b1cac-b434-4bab-888e-c3ea94fa45a1.PNG)

Following are the steps performed in the examining the system's availability.
1. Two critical user flows are considired to test the behavior of the system.
   - In this case we have considered the "Catalog search" and the "Checkout" flows
2. The application is load tested using "N" distinct user profiles
   - Note: JMeter was used to record the 2 specified user flows separately and then execute the same for a higher number of threads(users) and ramp-up time
3. As the application is already instrumented for observability using Azure Application Insights, information about the HTTP requests, dependency calls (successful and failed), performance in terms of time taken to complete the request etc., are collected at Application Insights.
4. The load test is run for 30 distinct users for a ramp-up period of 900 seconds(15 minutes)
5. The HTTP Requests Metrics is downloaded as a CSV file (the respository has 2 distinct files, 1 per user journey)
   - [Request Data for Catalog Search](SLITestMetricsData/Userflow_CatalogSearch_Query_15mins-Data.csv)
   - [Request Data for Checkout](SLITestMetricsData/Userflow_Locate&AddToBasket_Query_15mins-Data.csv)
6. SLI is calculated using the basic formula 
> **(Total number of successful HTTP Requests)/(Total number of Requests)**
7. Data that is captured for 15 minutes is now extrapolated for "N" hours. Please note that the test was run only for 15 minutes as this was sufficient for this prototype. In real-world, the load test needs to be run for multiple different durations (1, 8, 12 and 24 hours) so as to get enough of data points to calculate the SLI for a considerably long uptime of the system 




