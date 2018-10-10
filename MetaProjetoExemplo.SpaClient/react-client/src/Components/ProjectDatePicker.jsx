import React from 'react';
import PropTypes from 'prop-types';
import TextValidator from 'react-material-ui-form-validator/lib/TextValidator';
import { DatePicker } from 'material-ui-pickers'


const dateFormat = 'DD/MM/YYYY'

const handleShouldDisableDate = projects => date => {
  const active = projects.filter(p => p.isActive)
  return active.some(p => {
    return date.isBetween(p.startDate, p.finishDate) 
    || date.diff(p.startDate, 'day') === 0 
    || date.diff(p.finishDate, 'day') === 0
  })
}

const ProjectDatePicker = ({ projects, name, ...props }) => (
  <DatePicker
            {...props}
            keyboard
            format={dateFormat}
           
            // handle clearing outside => pass plain array if you are not controlling value outside
            mask={value => (value ? [/\d/, /\d/, '/', /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/] : [])}
            disableOpenOnEnter
            animateYearScrolling={false}
            disablePast
            shouldDisableDate={handleShouldDisableDate(projects)}
            TextFieldComponent={props => (
              <TextValidator
                {...props}
                name={name}
                validators={['required']}
                errorMessages={['this field is required']}
                InputLabelProps={{
                  shrink: true,
                }}
                />
            )}
          />
)

ProjectDatePicker.propTypes = {
  name: PropTypes.string.isRequired,
  projects: PropTypes.array.isRequired
}

export default ProjectDatePicker
