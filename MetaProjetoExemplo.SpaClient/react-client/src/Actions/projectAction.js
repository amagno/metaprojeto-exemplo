import { auth } from '../Lib/auth'
import { openSnackBar } from './snackBarAction';


const url = 'http://localhost:5000/api/project-management'


export const projectActionTypes = {
  FETCH_PROJECTS_DATA: 'FETCH_PROJECTS_DATA',
  RECEIVE_PROJECTS_DATA: 'RECEIVE_PROJECTS_DATA',
  FAILURE_PROJECTS_DATA: 'FAILURE_PROJECTS_DATA'
}

export const receiveProjects = data => ({
  type: projectActionTypes.RECEIVE_PROJECTS_DATA,
  payload: data
})

export const fetchProjects = () => ({
  type: projectActionTypes.FETCH_PROJECTS_DATA
})

export const failProjects = () => ({
  type: projectActionTypes.FAILURE_PROJECTS_DATA
})

export const createProject = project => async dispatch => {
  try {
    await auth.http().post(url, project)
    dispatch(fecthProjectsAction())
    dispatch(openSnackBar('projeto criado com sucesso'))
  } catch (error) {
    dispatch(handleProjectsFailAction('erro ao criar projeto'))
  }
}
export const finalizeProjectAction = id => async dispatch => {
  try {
    await auth.http().get(`${url}/finalize/${id}`)
    dispatch(fecthProjectsAction())
    dispatch(openSnackBar('projeto finalizado com sucesso'))
  } catch (error) {
    dispatch(handleProjectsFailAction('erro ao finalizar projeto'))
  }
}
export const fecthProjectsAction = () => async dispatch => {
  try {
    const response = await auth.http().get(url)
    dispatch(receiveProjects(response.data.projects))
  } catch (error) {
    dispatch(handleProjectsFailAction('erro ao buscar projetos'))
  }
}

export const handleProjectsFailAction = message => dispatch => {
  dispatch(openSnackBar(message))
  dispatch(failProjects())
}

