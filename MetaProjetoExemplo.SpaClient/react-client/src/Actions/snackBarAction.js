

export const snackBarActionTypes = {
  SNACK_BAR_OPEN: 'SNACK_BAR_OPEN',
  SNACK_BAR_CLOSE: 'SNACK_BAR_CLOSE'
}


export const openSnackBar = message => ({
  type: snackBarActionTypes.SNACK_BAR_OPEN,
  payload: {
    message
  }
})
export const closeSnackBar = () => ({
  type: snackBarActionTypes.SNACK_BAR_CLOSE
})