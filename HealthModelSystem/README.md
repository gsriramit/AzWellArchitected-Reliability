## Purpose of a Health Model System
A health model system helps in determining and indicating the "healthy" and "unhealthy" states of the system (application). As stated in the official documentation "*The health model should be able to surface the health of critical system flows or key subsystems to ensure appropriate operational prioritization is applied*".  
The raw obsrvability data that is obtained from the system components need to be processed accordingly so that the entire reliability/availability state of the system becomes determinable.  
To be able to determine the state of the system, we should  
- Determine the critical system flows (ones that affect the end-user)
- Determine the critical system components from the critical system flows
- Identify the parameters of the critical system components that help us determine if the component is healthy or otherwise
- Instrument all the components in the system appropriately to produce the necessary monitoring data
- Determine the health of the components and any cascasing effect on the rest of the ecosystem from the metrics, logs & traces
  - The monitoring data should be inclusive of the application performance data also
- Indicate the system and components' status using the Traffic Light model

## Architecture
![Well-Archictected-Reliability - Reliability-HealthModelSystem](https://user-images.githubusercontent.com/13979783/140267961-e665995b-b718-4514-a35c-61ce40ac290f.png)

### Health Model Architecture Components
- Configuration of observability/monitoring settings (at component level)
- Subscription to Azure Service and Resource Health Alerts
- Push or Pull based Alert Processors
- Light-weight no-sql db to persist the health model signals
- Health status change event processor
- Azure Signal-R service for near-real-time updates
- Dashboard UI app for the SRE/DevOps Team
- Connections to automation runbooks (possible extensions)

### Traffic Light model
- Green – Healthy
- Amber and/or Red – Needs attention & Unhealthy 



