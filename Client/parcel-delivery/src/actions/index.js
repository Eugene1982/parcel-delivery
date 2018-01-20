import * as API from '../utils/API'
import { GET_DEPARTMENTS } from '../utils/constants'


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
  


