import { combineReducers } from 'redux'

import departments from './departments'
import parcels from './parcels'
import error from './error'

const rootReducer = combineReducers({
    departments,
    parcels,
    error
})

export default rootReducer

