import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button'
import moment from 'moment'
import { ValidatorForm } from 'react-material-ui-form-validator';
import TextValidator from 'react-material-ui-form-validator/lib/TextValidator';
import { connect } from 'react-redux'
import { createProject } from '../Actions/projectAction'
import ProjectDatePicker from './ProjectDatePicker'



const styles = theme => ({
  container: {
    display: 'flex',
    flexWrap: 'wrap',
    flexDirection: 'column',
  },
  textField: {
    marginLeft: theme.spacing.unit,
    marginRight: theme.spacing.unit,
    margin: '10px 0'
  },
});

class PorjectForm extends React.Component {
  state = {
    title: '',
    finishDate: null,
    startDate: null,
  }
  handleChange = name => event => {
    const { value } = event.target

    this.setState(state => ({
      ...state,
      [name]: value
    }))
  }
  handleSubmit = async event => {
    event.preventDefault()
    const { title, startDate, finishDate } = this.state

    
    const data = {
      title,
      startDate: moment(startDate).format(),
      finishDate: moment(finishDate).format()
    }
    this.props.dispatch(createProject(data))
  }
  handleDateChange = name => date => {
    this.setState(state => ({
      ...state,
      [name]: date
    }))
  }
  render() {
    const { classes } = this.props
    return (
      <ValidatorForm onSubmit={this.handleSubmit} className={classes.container} noValidate>
        <TextValidator
          id="date"
          label="Título"
          type="text"
          name="title"
          value={this.state.title}
          onChange={this.handleChange('title')}
          // defaultValue="2017-05-24"
          className={classes.textField}
          validators={['required']}
          errorMessages={['this field is required']}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <ProjectDatePicker 
          projects={this.props.projects} 
          name="startDate" 
          label="Data de começo" 
          className={classes.textField} 
          value={this.state.startDate}
          onChange={this.handleDateChange('startDate')}
        />
        <ProjectDatePicker 
          projects={this.props.projects} 
          name="finishDate" 
          label="Data de fim" 
          className={classes.textField} 
          value={this.state.finishDate}
          onChange={this.handleDateChange('finishDate')}
        />
        <Button type="submit" variant="contained" color="primary">Criar</Button>
      </ValidatorForm>
    );
  }
}

PorjectForm.propTypes = {
  classes: PropTypes.object.isRequired,
  projects: PropTypes.array.isRequired
};

export default withStyles(styles)(connect(state => ({
  projects: state.projectManagement.projects
}))(PorjectForm));
