import axios from 'axios'
import { auth } from './auth';

const url = 'http://localhost:5000/api/project-management'
const authOpt = {
  headers: {'Authorization': `Bearer ${auth.getUserToken()}`}
}
const createProject = async (data) => {
  try {
    const response = await axios.post(url, data, authOpt)

    console.log(response)
    return true;
  } catch(error) {
    console.log(error)
    return false;
  }
}
const getProjects = async () => {
  const response = await axios.get(url, authOpt)

  return response.data
}
export const project = {
  createProject,
  getProjects
}