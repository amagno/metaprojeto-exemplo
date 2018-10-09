import React from 'react'
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import ProjectForm from '../Components/ProjectForm';
import Divider from '@material-ui/core/Divider'
import ProjectList from '../Components/ProjectList';


class ProjectsPage extends React.Component {
  render() {
    const { classes } = this.props;

    return (
      <div>
        <Paper className={classes.root} elevation={1}>
          <Typography variant="h5" component="h3">
            This is a sheet of paper.
          </Typography>
          <Typography component="p">
            Paper can be used to build surface or other elements for your application.
          </Typography>
          <ProjectForm />
        </Paper>
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


export default withStyles(styles)(ProjectsPage);