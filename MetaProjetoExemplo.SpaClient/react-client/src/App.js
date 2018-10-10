import React from 'react'
import { BrowserRouter } from 'react-router-dom'
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles'
import { Provider } from 'react-redux'
import { store } from './Stores/store';
import MuiPickersUtilsProvider from 'material-ui-pickers/utils/MuiPickersUtilsProvider';
import MomentUtils from 'material-ui-pickers/utils/moment-utils';
import moment from 'moment'


import AuthenticatedRoute from './Components/Route/AuthenticatedRoute'
import RedirectIfLoggedRoute from './Components/Route/RedirectIfLoggedRoute'
import ProjectsPage from './Pages/ProjectsPage'
import LoginPage from './Pages/LoginPage'
import SimpleSnackBar from './Components/SnackBar'

const locale = 'ru'
moment.locale(locale)

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
            <MuiPickersUtilsProvider utils={MomentUtils} moment={moment} locale={locale}>
              <RedirectIfLoggedRoute exact path="/login" component={LoginPage} to="/" />
              <AuthenticatedRoute exact path="/" component={ProjectsPage} />
              <SimpleSnackBar />
            </MuiPickersUtilsProvider>
            
          </MuiThemeProvider>
        </BrowserRouter>
      </Provider> 
    );
  }
}



export default App;
