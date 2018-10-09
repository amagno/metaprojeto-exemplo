import React from 'react';
import Button from '@material-ui/core/Button';
import Snackbar from '@material-ui/core/Snackbar';
import SnackbarContent from '@material-ui/core/SnackbarContent'


const Action = ({ handleClick }) =>  (
  <Button color="secondary" size="small" onClick={handleClick}>
    Ok
  </Button>
)
const Content = ({ handleClose, message }) => (
  <SnackbarContent
      message={message}
      action={<Action handleClick={handleClose} />}
  />
)
class SimpleSnackbar extends React.Component {
  test = () => {
    console.log('test')
  }
  render() {
    return (
      <div>
        {console.log('snack bar', this.props)}
        <Snackbar
          open={this.props.open}
          autoHideDuration={6000}
          onClose={this.props.handleClose}
        >
          <Content handleClose={this.props.handleClose} message={this.props.message} />
        </Snackbar>
      </div>
    );
  }
}

export default SimpleSnackbar;
