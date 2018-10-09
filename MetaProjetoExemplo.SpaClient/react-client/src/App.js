import React from 'react'
import { BrowserRouter } from 'react-router-dom'
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles'
import { Provider } from 'react-redux'
import { store } from './Stores/store';

import AuthenticatedRoute from './Components/Route/AuthenticatedRoute'
import RedirectIfLoggedRoute from './Components/Route/RedirectIfLoggedRoute'
import ProjectsPage from './Pages/ProjectsPage'
import LoginPage from './Pages/LoginPage'
import SimpleSnackBar from './Components/SnackBar'

const theme = createMuiTheme({
  typography: {
    useNextVariants: true,
  },
});

class App extends React.Component {
  render() {
    return (
      <Provider store={store}>
        <BrowserRouter>
          <MuiThemeProvider theme={theme}>
            <RedirectIfLoggedRoute exact path="/login" component={LoginPage} to="/" />
            <AuthenticatedRoute exact path="/" component={ProjectsPage} />
            <SimpleSnackBar />
          </MuiThemeProvider>
        </BrowserRouter>
      </Provider> 
    );
  }
}



export default App;
