import { projectStore } from './projectStore'
import { combineReducers, createStore, applyMiddleware } from 'redux'
import thunk from 'redux-thunk';
import { snackBarStore } from './snackBarStore';
import { authStore } from './authStore';


const reducers = combineReducers({
  projectManagement: projectStore,
  snackBar: snackBarStore,
  auth: authStore
})

export const store = createStore(
  reducers,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__(),
  applyMiddleware(thunk)
)