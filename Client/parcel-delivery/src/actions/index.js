import * as API from '../utils/API'
import { GET_DEPARTMENTS, ADD_DEPARTMENT, UPLOAD_DOCUMENT_SUCCESS, UPLOAD_DOCUMENT_FAIL } from '../utils/constants'


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
            .catch(error => dispatch(uploadFail(error)))
    }
}


