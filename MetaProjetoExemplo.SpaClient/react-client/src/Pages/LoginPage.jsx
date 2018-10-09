import React from 'react'
import Button from '@material-ui/core/Button'
import { withStyles } from '@material-ui/core'
import Typography from '@material-ui/core/Typography'
import { ValidatorForm, TextValidator } from 'react-material-ui-form-validator';
import { auth } from '../Lib/auth';
import SimpleSnackBar from '../Components/SnackBar';
import { withRouter } from 'react-router-dom'


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
    data: {
      email: '',
      password: ''
    },
    fail: false
  }
  handleChange = name => event => {
    this.setState({
      ...this.state,
      data: {
        ...this.state.data,
        [name]: event.target.value
      }
    })
  }
  setErrorForm = result => {
    console.log('set error form', result)
  }
  handleFailClose = () => {
    console.log('handle close')
    this.setState({
      ...this.state,
      fail: false,
      isSending: false
    })
  }
  handleSubmit = async event => {
    this.setState({
      ...this.state,
      isSending: true
    })
    const { email, password } = this.state.data
    const result = await auth.login(email, password)

    this.setState({
      ...this.state,
      isSending: false
    })
        
    if (result === false) {
      this.setState({
        ...this.state,
        fail: true
      })
    } else {
      this.props.history.push(['/'])
    }
  }
  render() {
    return (
      <div>
        <ValidatorForm onSubmit={this.handleSubmit} className={this.props.classes.container}>
          <Typography variant="subtitle2" gutterBottom>
            Login
        </Typography>
          <TextValidator
            id="email_form"
            label="E-mail"
            value={this.state.data.email}
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
            value={this.state.data.password}
            onChange={this.handleChange('password')}
            margin="normal"
            variant="outlined"
            type="password"
            validators={['required']}
            name="password"
            errorMessages={['campo obrigatório']}
          />
          <Button disabled={this.state.isSending} type="submit" className={this.props.classes.button} variant="outlined" color="primary">
            Login
          </Button>
        </ValidatorForm>
        <SimpleSnackBar handleClose={this.handleFailClose} open={this.state.fail} message="login error" />
      </div>
    )
  }
}


export default withStyles(styles)(withRouter(LoginPage));