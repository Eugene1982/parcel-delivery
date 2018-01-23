import React, { Component } from 'react'

class ParcelDetail extends Component {
    render() {
        const { parcels, departmentName} = this.props
        let filteredParcels = parcels.filter(p => p.DepartmentName === departmentName)
        return (
            <div>
                {filteredParcels.map((item) => (
                    <div>
                        {item.Weight}
                        {item.Price}
                    </div>
                ))}

            </div>
        )
    }
}

export default ParcelDetail