import { GET_DEPARTMENTS, ADD_DEPARTMENT } from '../utils/constants'

export default function departments(state = [], action) {
    switch (action.type) {
        case GET_DEPARTMENTS:
            return action.departments
        case ADD_DEPARTMENT:
            return [...state, action.department]
  
        default:
            return state
    }
}