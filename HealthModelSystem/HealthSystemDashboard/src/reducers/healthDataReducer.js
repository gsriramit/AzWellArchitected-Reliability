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
        default:
            return state;
    }
}


