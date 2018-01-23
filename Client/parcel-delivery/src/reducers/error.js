import { UPLOAD_DOCUMENT_SUCCESS, UPLOAD_DOCUMENT_FAIL, CLEAR_RESULTS } from '../utils/constants'

export default function error(state = null, action) {
    switch (action.type) {
        case UPLOAD_DOCUMENT_SUCCESS:
        case CLEAR_RESULTS:
            return null
        case UPLOAD_DOCUMENT_FAIL:
            return action.error
        default:
            return state
    }
}