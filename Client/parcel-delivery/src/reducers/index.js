import { combineReducers } from 'redux'

import departments from './departments'
import parcels from './parcels'

const rootReducer = combineReducers({
    departments,
    parcels
})

export default rootReducer

