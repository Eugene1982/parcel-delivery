import { UPLOAD_DOCUMENT_SUCCESS } from '../utils/constants'

export default function parcels(state = [], action) {
    switch (action.type) {
        case UPLOAD_DOCUMENT_SUCCESS:
            return action.parcels
        default:
            return state
    }
}