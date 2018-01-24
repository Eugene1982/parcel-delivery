import { GET_DEPARTMENTS, ADD_DEPARTMENT, DELETE_DEPARTMENT } from '../utils/constants'

export default function departments(state = [], action) {
    switch (action.type) {
        case GET_DEPARTMENTS:
            return action.departments
        case ADD_DEPARTMENT:
            return [...state, action.department]
        case DELETE_DEPARTMENT:
            return state.filter(item => item.Name !== action.name)
        default:
            return state
    }
}