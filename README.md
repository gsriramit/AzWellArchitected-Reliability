# About this Repo
This repository contains artifacts that help in designing and implementing systems according to the Azure Well-Architected Framework. We would be using reference architectures from Azure's High Availability Reference Architectures collection, thereby serving as a guide to "Learn building a highly reliable system yourself (DIY)"

# Reference Architectures
The system that we use for the discussion of a variety of topics in this repository will be a combination of the 2 architectures that are specified below. The first architecture shows a simple 2-Tier web application built using regional resiliency features. The second architecture can be considred an extension of the same system on to a secondary region with infrastructure & application redundancy.  
**Note**: The system that you are trying to build can be one that is running on Kubernetes. However that path should be adopted only if you are fairly comfortable with containers and kubernetes.

**IaaS: Web application with relational database**  
https://docs.microsoft.com/en-us/azure/architecture/high-availability/ref-arch-iaas-web-and-db

**Multi-region N-tier application**  
https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/n-tier/multi-region-sql-server

# Sections

1. [Reference Architecure](ReferenceArchitectures) 
2. [Requirement Analysis](Requirements)
   - Availability Targets     
   - Recovery Targets
3. [Design](Design)
   - [Reliability Checklist](Design/ReliabilityChecklist)
   - [Dependency Analysis](Design/DependencyAnalysis)
     - Dependency Mapping & Microsoft Resiliency Modeling(RMA)
   - [Failure Mode Effect Analysis (FMEA)](Design/FMEA)
   - Observability Setup
4. [System Reliability Baseline](SystemReliabilityBaseline)
   - [SLA Estimator](SystemReliabilityBaseline/SLAEstimator)
5. [Reliability Policies](ReliabilityPolicies)
6. Testing & Experimentation
   - System Performance Testing (Load)
   - [Chaos Experiments](ChaosExperiments)
7. [Health Model System](HealthModelSystem)

# Areas of Extensibility
1. Chaos engineering for scenarios that experiment regional failover 
   - Frontend fails( backend does not)
   - Backend fails (frontend does not but the health probe would fail)
2. Traffic Manager being a SPOF
   - Using an alternate global load balancing solution (manual change of piublic DNS A records to point to the fallback load balancer)
   - The design can also be modified to use Azure Front Door as the primary global load balancer and have azure traffic manager as the backup option 
3. Implementation of resiliency at the application level
   - https://github.com/mspnp/samples/tree/master/Reliability/RetryPatternSample
