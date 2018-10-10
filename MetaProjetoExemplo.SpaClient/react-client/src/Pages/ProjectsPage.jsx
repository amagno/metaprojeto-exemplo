import React from 'react'
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import ProjectForm from '../Components/ProjectForm';
import Divider from '@material-ui/core/Divider'
import ProjectList from '../Components/ProjectList'
import { connect } from 'react-redux'
import LinearProgress from '@material-ui/core/LinearProgress';
import Button from '@material-ui/core/Button'
import { auth } from '../Lib/auth';
class ProjectsPage extends React.Component {
  handleLogout = () => {
    auth.logout()
    this.props.history.push(['/login'])
  }
  render() {
    const { loading } = this.props;
    console.log('LOADING', loading)
    return (
      <div>
        <Paper style={{ display: 'flex', flexDirection: 'column', padding: '10px' }} elevation={1}>
          <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
            <Typography variant="h5" component="h3">
              Gerênciamento de Projetos
            </Typography>
            <Button type="submit" variant="contained" color="secondary" onClick={this.handleLogout}>Logout</Button>
          </div>
          <Typography component="p">
            Aplicação de exemplo "Gerênciamento de Projetos", um usuário "Gerente de projetos" pode adicionar vários projetos, desde 
            que o projeto a ser adicionado não interceda os projetos já existentes
          </Typography>
          <ProjectForm />
        </Paper>
        { loading ? <LinearProgress variant="query" /> : null }
        <Divider />
        <ProjectList />
      </div>
    );
  }
}



const styles = theme => ({
  root: {
    ...theme.mixins.gutters(),
    paddingTop: theme.spacing.unit * 2,
    paddingBottom: theme.spacing.unit * 2,
  },
});


export default withStyles(styles)(connect(state => ({
  loading: state.projectManagement.loading
}))(ProjectsPage));