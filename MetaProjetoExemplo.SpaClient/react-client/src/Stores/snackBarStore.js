import { snackBarActionTypes } from '../Actions/snackBarAction'


const initialState = {
  open: false,
  message: ''
}

export const snackBarStore = (state = initialState, action) => {
  switch (action.type) {
    case snackBarActionTypes.SNACK_BAR_OPEN:
      return {
        open: true,
        message: action.payload.message
      }
    case snackBarActionTypes.SNACK_BAR_CLOSE:
      return {
        open: false,
        message: ''
      }
    default:
      return state
  }
}