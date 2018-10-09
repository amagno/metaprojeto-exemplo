import React from 'react';
import { BrowserRouter, Route, Redirect } from 'react-router-dom';
import LoginPage from './Pages/LoginPage';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import { auth } from './Lib/auth';
import ProjectsPage from './Pages/ProjectsPage';

const theme = createMuiTheme({
  typography: {
    useNextVariants: true,
  },
});
const AuthenticatedRoute = ({ component: Component, ...props }) => {
  const urlToRedirect = '/login'
  return <Route {...props} render={componentProps => (
    auth.userIsLogged() ? 
    <Component {...componentProps} /> :
    <Redirect to={{
      pathname: urlToRedirect,
      state: { from: componentProps.location }
    }}/>
  )}/>

}
const RedirectIfLoggedRoute = ({ component: Component, to, ...props,  }) => {
  const urlToRedirect = to
  return <Route {...props} render={componentProps => (
    auth.userIsLogged() ? 
    <Redirect to={{
      pathname: urlToRedirect,
      state: { from: componentProps.location }
    }}/>
    : <Component {...componentProps} /> 
  )}/>

}


class App extends React.Component {
  render() {
    return (
      <BrowserRouter>
        <MuiThemeProvider theme={theme}>
          {/* <Route exact path="/login" component={LoginPage} /> */}
          <RedirectIfLoggedRoute exact path="/login" component={LoginPage} to="/" />
          <AuthenticatedRoute exact path="/" component={ProjectsPage} />
        </MuiThemeProvider>
      </BrowserRouter>
    );
  }
}



export default App;
