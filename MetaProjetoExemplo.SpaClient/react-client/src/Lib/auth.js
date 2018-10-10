import axios from 'axios'

const url = 'http://localhost:5000/api/auth'
const userInfoKey = 'user-info'


const setUserInfo = (userInfo) => {
  const str = JSON.stringify(userInfo);
  localStorage.setItem(userInfoKey, str)
}
const userIsLogged = () => {
  return !!localStorage.getItem(userInfoKey)
}
const getUserToken = () => {
  if (!userIsLogged()) {
    return null;
  }

  const data = JSON.parse(localStorage.getItem(userInfoKey))

  if (!data.token) {
    throw new Error('invalid token on storage')
  }

  return data.token
}
const logout = () => {
  localStorage.removeItem(userInfoKey)
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
    return false;
  }
}
const http = () => {
  const instance = axios.create({
    headers: {'Authorization': `Bearer ${getUserToken()}`},
    validateStatus: status => {
      return (status >= 200 && status < 300) || status === 401;
    }
  });

  instance.interceptors.response.use(response => {
    if (response.status === 401) {
      logout();
      window.location.reload();
    }

    return response
  })

  return instance
}

export const auth = {
  login,
  userIsLogged,
  getUserToken,
  http,
  logout
}