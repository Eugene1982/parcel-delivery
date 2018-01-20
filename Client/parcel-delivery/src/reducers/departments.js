import { GET_DEPARTMENTS } from '../utils/constants'

export default function departments(state = [], action) {
    switch (action.type) {
        case GET_DEPARTMENTS:
            return action.departments
        default:
            return state
    }
}