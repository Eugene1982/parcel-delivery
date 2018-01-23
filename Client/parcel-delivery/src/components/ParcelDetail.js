import React, { Component } from 'react'

class ParcelDetail extends Component {
    render() {
        const { parcels, departmentName} = this.props
        let filteredParcels = parcels.filter(p => p.DepartmentName === departmentName)
        return (
            <div>
                {filteredParcels.map((item) => (
                    <div>
                        <div>Weight: {item.Weight}</div>
                        <div>Price: {item.Price}</div>
                        <div>From: {item.From.Name} {item.From.Street} {item.From.HouseNumber} {item.From.City} {item.From.PostalCode}</div>
                        <div>To: {item.To.Name} {item.To.Street} {item.To.HouseNumber} {item.To.City} {item.To.PostalCode}</div>
                        <hr />
                    </div>
                ))}

            </div>
        )
    }
}

export default ParcelDetail