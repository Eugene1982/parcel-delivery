import * as API from '../utils/API'
import { GET_DEPARTMENTS, ADD_DEPARTMENT } from '../utils/constants'


export function getDepartments() {
    return dispatch => {
      API.getDepartments().then(departments => {
        dispatch({
          type: GET_DEPARTMENTS,
          departments
        })
      })
    }
  }

  export function addDepartment(department) {
    return dispatch => {
      API.addDepartment(department).then(p => {
        dispatch({
          type: ADD_DEPARTMENT,
          post: p
        })
      })
    }
  }
  
  


