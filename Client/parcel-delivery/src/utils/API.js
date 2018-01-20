const api = process.env.ORIGIN || 'http://localhost:9000/api'

const headers = {
  'Accept': 'application/json'
}

export const getDepartments = () =>
  fetch(`${api}/departments`, { headers })
    .then(res => res.json())
  
