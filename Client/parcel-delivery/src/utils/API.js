const api = process.env.ORIGIN || 'http://localhost:9000/api'

const headers = {
    'Accept': 'application/json'
}

export const getDepartments = () =>
    fetch(`${api}/departments`, { headers })
        .then(res => res.json())

export const addDepartment = (body) =>
    fetch(`${api}/departments`, {
        method: 'POST',
        headers: {
            ...headers,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })

export const sendParcels = (body) =>
    fetch(`${api}/parcels`, {
        method: 'POST',
        headers: {
            'Accept': 'application/xml',
            'Content-Type': 'application/xml'
        },
        body
    }).then(res => res.json())



