import * as API from '../utils/API'
import { GET_DEPARTMENTS, ADD_DEPARTMENT, DELETE_DEPARTMENT, UPLOAD_DOCUMENT_SUCCESS, UPLOAD_DOCUMENT_FAIL, CLEAR_RESULTS } from '../utils/constants'


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
                department
            })
        })
    }
}

export function deleteDepartment(name){
    return dispatch => {
        API.deleteDepartment(name).then(p => {
            dispatch({
                type: DELETE_DEPARTMENT,
                name
            })
        })
    }
}

export function uploadSuccess(parcels) {
    return {
        type: UPLOAD_DOCUMENT_SUCCESS,
        parcels,
    };
}

export function uploadFail(error) {
    return {
        type: UPLOAD_DOCUMENT_FAIL,
        error,
    };
}

export function uploadParcels(file) {
    return dispatch => {
        API.sendParcels(file)
            .then(response => dispatch(uploadSuccess(response)))
            .catch(error => error.then(message => dispatch(uploadFail(message))))
    }
}

export function clearParcels() {
    return {
        type: CLEAR_RESULTS
    }
}


