** Purpose of a Health Model System
A health model system helps in determining and indicating the "healthy" and "unhealthy" states of the system (application). As stated in the offocial documentation >The health model should be able to surface the health of critical system flows or key subsystems to ensure appropriate operational prioritization is applied.  
The raw obsrvability data that is obtained from the system components need to be processed accordingly so that the entire reliability/availability state of the system should be determinable.  
To be able to determine the state of the system, we should  
- Determine the critical system flows (ones that affect the end-user)
- Determine the critical system components from the critical system flows
- Identify the parameters of the critical system components that help us determine if the component is healthy or otherwise
- Instrument all the components in the system appropriately to produce the necessary monitoring data
- Determine the health of the components and any cascasing effect on the rest of the ecosystem from the metrics, logs & traces
  - The monitoring data should be inclusive of the application performance data also
- Indicate the system and components' status using the Traffic Light model

** Architecture
