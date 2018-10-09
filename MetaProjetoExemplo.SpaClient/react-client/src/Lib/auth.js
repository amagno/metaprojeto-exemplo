import axios from 'axios'

const url = 'http://localhost:5000/api/auth'
const userInfoKey = 'user-info'


const setUserInfo = (userInfo) => {
  const str = JSON.stringify(userInfo);
  localStorage.setItem(userInfoKey, str)
}
const getUserToken = () => {
  const data = JSON.parse(localStorage.getItem(userInfoKey))

  if (!data.token) {
    throw new Error('invalid token on storage')
  }

  return data.token
}
const userIsLogged = () =>{
  return !!localStorage.getItem(userInfoKey)
}
const login = async (email, password) => {
  try {
    const response = await axios.post(`${url}/login`, {
      email,
      password
    });
    const { token, userIdentifier } = response.data
    if (!token || !userIdentifier) {
      throw new Error('login response is invalid')
    }

    setUserInfo({ token, userIdentifier })
    return true;
  } catch (error) {
    console.log('ERROR ERRRO', error)
    return false;
  }

}


export const auth = {
  login,
  userIsLogged,
  getUserToken
}