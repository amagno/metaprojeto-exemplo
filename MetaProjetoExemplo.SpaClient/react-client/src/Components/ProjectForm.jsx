import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button'
import moment from 'moment'
import { ValidatorForm } from 'react-material-ui-form-validator';
import TextValidator from 'react-material-ui-form-validator/lib/TextValidator';
import { connect } from 'react-redux'
import { createProject } from '../Actions/projectAction';

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
    finishDate: '',
    startDate: ''
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
    console.log(this.state)

    const data = {
      ...this.state,
      startDate: moment(this.state.startDate).format(),
      finishDate: moment(this.state.finishDate).format()
    }
    this.props.dispatch(createProject(data))
  }
  render() {
    const { classes } = this.props;

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
        <TextValidator
          id="date"
          label="Data de começo"
          type="date"
          name="startDate"
          value={this.state.startDate}
          onChange={this.handleChange('startDate')}
          validators={['required']}
          errorMessages={['this field is required']}
          className={classes.textField}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextValidator
          id="date"
          name="finishDate"
          label="Data de fim"
          type="date"
          value={this.state.finishDate}
          onChange={this.handleChange('finishDate')}
          validators={['required']}
          errorMessages={['this field is required']}
          className={classes.textField}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <Button type="submit" variant="contained" color="primary">Criar</Button>
      </ValidatorForm>
    );
  }
}

PorjectForm.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(connect()(PorjectForm));
