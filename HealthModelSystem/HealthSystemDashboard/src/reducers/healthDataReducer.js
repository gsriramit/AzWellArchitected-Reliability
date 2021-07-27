const initialState = {
    healthInfo : [],
    isFetching : false,
    errorMessage: ''
}
export const healthDataReducer = (state = initialState, action) => {
    switch(action.type){
        case 'FETCH_HEALTHDATA':
            return {...state, isFetching: true};
        case 'FETCH_HEALTHDATA_SUCCESS':
            return {...state, healthInfo: action.payload};
        case 'FETCH_HEALTHDATA_FAILURE':
            return {...state, errorMessage: action.payload};
        case 'SIGNALR_NOTIFICATION':
            return {...state, healthInfo : updateSignalRUpdateInstate(state.healthInfo ,action.payload)}
        default:
            return state;
    }
}


const updateSignalRUpdateInstate = (healthInfo, signalRUpdate) => {
    if(healthInfo === [])
        return [];
    else if(healthInfo.length > 0 && signalRUpdate.length > 0) {
        for(let i =0 ; i < signalRUpdate.length; i ++){
            for (let j = 0; j < healthInfo.length; j++){
                if(healthInfo[j].componentName === signalRUpdate[i].ComponentName){
                    healthInfo[j].lastCheckTime = signalRUpdate[i].LastCheckTime;
                    healthInfo[j].overallHealthStatus = signalRUpdate[i].OverallHealthStatus;


                    for(let m = 0; m < signalRUpdate[i].SubComponents.length; m ++){
                        for(let n = 0; n < healthInfo[j].subComponents.length; n ++){

                            if(signalRUpdate[i].SubComponents[m].SubcomponentCategory === healthInfo[j].subComponents[n].subcomponentCategory
                                && signalRUpdate[i].SubComponents[m].SubComponentStatus.length > 0){

                                    for(let a = 0; a < signalRUpdate[i].SubComponents[m].SubComponentStatus.length; a++){
                                        for(let b = 0; b < healthInfo[j].subComponents[n].subComponentStatus.length; b++){

                                            if(healthInfo[j].subComponents[n].subComponentStatus[b].subcomponentName === signalRUpdate[i].SubComponents[m].SubComponentStatus[a].SubcomponentName){
                                                healthInfo[j].subComponents[n].subComponentStatus[b].currentStatus = signalRUpdate[i].SubComponents[m].SubComponentStatus[a].CurrentStatus;
                                                healthInfo[j].subComponents[n].subComponentStatus[b].additionalInfo = signalRUpdate[i].SubComponents[m].SubComponentStatus[a].AdditionalInfo;
                                                healthInfo[j].subComponents[n].subComponentStatus[b].subcomponentCategory = signalRUpdate[i].SubComponents[m].SubComponentStatus[a].SubcomponentCategory;
                                                healthInfo[j].subComponents[n].subComponentStatus[b].lastCheckTime = signalRUpdate[i].SubComponents[m].SubComponentStatus[a].LastCheckTime;
                                            }
                                        }
                                    }



                                }

                        }
                    }

                    //Check over all health status
                    let overAllHealthStatusFlag = true;
                    healthInfo[j].subComponents.forEach((eachSubcomponent) => {
                        eachSubcomponent.subComponentStatus.forEach((eachStatus) => {
                            if(eachStatus.currentStatus === 'UnHealthy' && eachStatus.currentStatus !== 'UnAvailable'){
                                overAllHealthStatusFlag = false;
                            }
                        });
                    });
                    healthInfo[j].overallHealthStatus = overAllHealthStatusFlag;
                }
            }
        }

        return healthInfo;
    }
};


