import React from 'react'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { ValidatorForm, TextValidator } from 'react-material-ui-form-validator';
import { withStyles } from '@material-ui/core'
import { connect } from 'react-redux';
import { loginAction } from '../Actions/authAction';


const styles = {
  container: {
    width: '70%',
    padding: '20px 100px',
    margin: '200px auto', display: 'flex',
    flexDirection: 'column'
  },
  button: {
    height: '50px'
  }
}
class LoginPage extends React.Component {
  state = {
    email: '',
    password: ''
  }
  handleChange = name => event => {
    this.setState({
      [name]: event.target.value
    })
  }
  handleSubmit = async event => {
    event.preventDefault()
    const { email, password } = this.state
    const result = await this.props.dispatch(loginAction(email, password))

    if (result === true) {
      this.props.history.push(['/'])
    }
  }
  render() {
    return (
      <div>
        <ValidatorForm onSubmit={this.handleSubmit} className={this.props.classes.container}>
          <Typography variant="h2" gutterBottom style={{}}>
            Login
          </Typography>
          <TextValidator
            id="email_form"
            label="E-mail"
            value={this.state.email}
            onChange={this.handleChange('email')}
            margin="normal"
            variant="outlined"
            validators={['required', 'isEmail']}
            name="email"
            errorMessages={['campo obrigatório', 'deve ser um e-mail válido']}
          />
          <TextValidator
            id="password_form"
            label="Senha"
            value={this.state.password}
            onChange={this.handleChange('password')}
            margin="normal"
            variant="outlined"
            type="password"
            validators={['required']}
            name="password"
            errorMessages={['campo obrigatório']}
          />
          <Button id="login_button" disabled={this.props.isSending} type="submit" className={this.props.classes.button} variant="outlined" color="primary">
            Login
          </Button>
        </ValidatorForm>
      </div>
    )
  }
}


export default withStyles(styles)(connect(state => ({ ...state.auth }))(LoginPage));