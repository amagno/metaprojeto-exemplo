import React from 'react'
import ProjectItem from './ProjectItem'
import { connect } from 'react-redux'
import { fecthProjectsAction } from '../Actions/projectAction'

class ProjectList extends React.Component {
  componentDidMount = async () => {
    this.props.dispatch(fecthProjectsAction())
  }
  render() {
    const { projects } = this.props
    return (
      <div style={{ marginTop: '20px', display: 'flex', flexDirection: 'row', flexWrap: 'wrap', justifyContent: 'space-around' }}>
        {projects.map((p, i) => 
          <ProjectItem project={p} key={i} />
        )}
      </div>
    );
  }
}


export default connect(state => ({
  projects: state.projectManagement.projects
}))(ProjectList);
