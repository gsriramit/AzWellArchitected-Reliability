import axios from 'axios';

export const fetchHealthInfo = () => {
    return (dispatch) => {
        dispatch({
            type: 'FETCH_HEALTHDATA'
        });
    };
}

export const fetchHealthDataSuccess = (healthData) => {
    return (dispatch) => {
        dispatch({
            type: 'FETCH_HEALTHDATA_SUCCESS',
            payload: healthData
        });
    }
}

export const fetchHealthDataFailure = (errorMessage) => {
    return (dispatch) => {
        dispatch({
            type: 'FETCH_HEALTHDATA_FAILURE',
            payload: errorMessage
        });
    }
}

export const healthDataFromServer = () => async (dispatch) => {
    await axios.get("https://azhealthmodelsystem-dev01.azurewebsites.net/api/HealthSystemDashboardProcessor?code=gdxBc4I3bIRs5Ck07Pqoqlgj5nQrV9RXAhr9QHBDTb0PoLcTRjfzoA==")
    .then(response => {
        dispatch(fetchHealthDataSuccess(response.data));
    })
    .catch(error => {
        dispatch(fetchHealthDataFailure(error.message));
    })
}

export const updateSignalRUpdates = (signalRUpdate) => {
    return (dispatch) => {
        dispatch({
            type : 'SIGNALR_NOTIFICATION',
            payload: signalRUpdate
        })
    }
}