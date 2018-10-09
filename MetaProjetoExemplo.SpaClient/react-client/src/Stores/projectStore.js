import { projectActionTypes } from '../Actions/projectAction'


const initialState = {
  loading: false,
  projects: []
}


export const projectStore = (state = initialState, action) => {
  switch (action.type) {
    case projectActionTypes.FETCH_PROJECTS_DATA: 
      return {
        ...state,
        loading: true
      }
    case projectActionTypes.RECEIVE_PROJECTS_DATA:
      return {
        ...state,
        loading: false,
        projects: Array.isArray(action.payload) ? action.payload : []
      }
    case projectActionTypes.FAILURE_PROJECTS_DATA:
      return {
        ...state,
        loading: false,
      }
    
    default:
      return state;
  }
}