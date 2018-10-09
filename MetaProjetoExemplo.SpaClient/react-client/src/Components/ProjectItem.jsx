import React from 'react'
import { withStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper'
import Typography from '@material-ui/core/Typography'
import moment from 'moment'
import Button from '@material-ui/core/Button'
import { connect } from 'react-redux';
import { finalizeProjectAction } from '../Actions/projectAction';

const styles = theme => ({
  card: {
    width: '20%',
    margin: '10px 0',
    paddingTop: theme.spacing.unit * 2,
    paddingBottom: theme.spacing.unit * 2,
    padding: 10
  },
  date: {
    marginRight: theme.spacing.unit * 1
  },
  content: { 
    display: 'flex', 
    flexDirection: 'row', 
    alignItems: 'center',
    padding: 20
  },
  button: {
    width: '100%'
  }
});
const ProjectItem = ({ classes, project, finalize }) => (
  <Paper className={classes.card} elevation={1}>
    <Typography variant="h5" component="h3">
      {project.title}
    </Typography>
    <div className={classes.content}>
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
    <Button 
      disabled={!project.isActive} 
      color="secondary" 
      className={classes.button}
      onClick={() => finalize(project.id)}
    >
      Finalizar
    </Button>
  </Paper>
)


export default withStyles(styles)(connect(null, dispatch => ({
  finalize: id => dispatch(finalizeProjectAction(id))
}))(ProjectItem))