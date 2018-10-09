import { auth } from "../Lib/auth";
import { openSnackBar } from "./snackBarAction";



export const authActionTypes = {
  SEND_LOGIN: 'SEND_LOGIN',
  SUCCESS_LOGIN: 'SUCCESS_LOGIN',
  FAILURE_LOGIN: 'FAILURE_LOGIN'
}


export const loginAction = (email, password) => async dispatch => {
  dispatch({
    type: authActionTypes.SEND_LOGIN
  })
  const result = await auth.login(email, password)

  if (result === true) {
    dispatch({
      type: authActionTypes.SUCCESS_LOGIN
    })
  } else {
    dispatch({
      type: authActionTypes.FAILURE_LOGIN
    })
    dispatch(openSnackBar("Login inválido!"))
  }

  return result;
}

