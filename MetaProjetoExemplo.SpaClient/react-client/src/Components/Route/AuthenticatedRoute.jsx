import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { auth } from '../../Lib/auth';

const AuthenticatedRoute = ({ component: Component, ...props }) => {
  const urlToRedirect = '/login'
  const isLogged = auth.userIsLogged()
  return (
    <Route {...props} render={componentProps => (
      isLogged ? 
      <Component {...componentProps} /> :
      <Redirect to={{
        pathname: urlToRedirect,
        state: { from: componentProps.location }
      }}/>
    )}/>
  )

}


export default AuthenticatedRoute