import { auth } from '../Lib/auth'
import { authActionTypes } from '../Actions/authAction'


const initialState = {
  isLogged: auth.userIsLogged(),
  isSending: false
}


export const authStore = (state = initialState, action) => {
  switch (action.type) {
    case authActionTypes.SEND_LOGIN:
      return {
        ...state,
        isSending: true
      }
    case authActionTypes.SUCCESS_LOGIN:
      return {
        ...state,
        isLogged: true,
        isSending: false
      }
    case authActionTypes.FAILURE_LOGIN:
      return {
        ...state,
        isSending: false
      }
    default:
      return state
  }
}