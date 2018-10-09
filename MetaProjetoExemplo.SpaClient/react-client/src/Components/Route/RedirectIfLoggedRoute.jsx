import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { auth } from '../../Lib/auth';



const RedirectIfLoggedRoute = ({ component: Component, to, ...props}) => {
  const urlToRedirect = to
  const isLogged = auth.userIsLogged()
  return (
    <Route {...props} render={componentProps => (
      isLogged ? 
      <Redirect to={{
        pathname: urlToRedirect,
        state: { from: componentProps.location }
      }}/>
      : <Component {...componentProps} /> 
    )}/>
  )

}

export default RedirectIfLoggedRoute