import { UPLOAD_DOCUMENT_SUCCESS, CLEAR_RESULTS } from '../utils/constants'

export default function parcels(state = [], action) {
    switch (action.type) {
        case UPLOAD_DOCUMENT_SUCCESS:
            return action.parcels
        case CLEAR_RESULTS:
            return []
        default:
            return state
    }
}