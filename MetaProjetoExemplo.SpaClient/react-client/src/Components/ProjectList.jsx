import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import { project } from '../Lib/project';
import moment from 'moment'
const styles = theme => ({
  root: {
    ...theme.mixins.gutters(),
    paddingTop: theme.spacing.unit * 2,
    paddingBottom: theme.spacing.unit * 2,
  },
  date: {
    marginRight: theme.spacing.unit * 1
  }
});
const ProjectItem = ({ classes, project }) => (
  <Paper className={classes.root} elevation={1}>
    <Typography variant="h5" component="h3">
      {project.title}
    </Typography>
  <div style={{ display: 'flex', flexDirection: 'row' }}>
    <Typography component="p" className={classes.date}>
      <strong>Começo: </strong>
      {moment(project.startDate).format('ll')}
    </Typography>
    <Typography component="p" className={classes.date}>
      <strong>Final: </strong>
      {moment(project.finishDate).format('ll')}
    </Typography>
    <Typography component="p" className={classes.date}>
      <strong>Situação: </strong>
      {project.isActive ? 'Ativo' : 'Finalizado'}
    </Typography>
  </div>
  </Paper>
)
class ProjectList extends React.Component {
  state = {
    projects: []
  }
  componentDidMount = async () => {
    const { projects } = await project.getProjects()
    console.log('did mount')
    this.setState(state => ({
      ...state,
      projects: Array.isArray(projects) ? projects : []
    }))
  }
  render() {
    const { classes } = this.props;

    return (
      <div>
        {this.state.projects.map((p, i) => 
          <ProjectItem  classes={classes} project={p} key={i} />
        )}
      </div>
    );
  }
}

ProjectList.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(ProjectList);
