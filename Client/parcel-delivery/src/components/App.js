import React, { Component } from 'react'
import { connect } from 'react-redux'
import Modal from 'react-modal'
import { Route, Link, withRouter } from 'react-router-dom';
import './App.css';
import { getDepartments, addDepartment, uploadParcels, clearParcels } from '../actions';
import AddControl from './AddControl'
import ParcelDetail from './ParcelDetail'

class App extends Component {

  state = {
    modalOpen: false
  }

  componentDidMount() {
    const { dispatch } = this.props
    dispatch(getDepartments())
  }

  openModal = () => this.setState(() => ({ modalOpen: true }))
  closeModal = () => this.setState(() => ({ modalOpen: false }))

  addDepartment = (department) => {
    const { dispatch } = this.props
    dispatch(addDepartment(department));
    dispatch(clearParcels())
    this.closeModal()
  }

  handleFileUpload = (event) => {
    const { dispatch } = this.props
    const data = event.target.files[0]
    dispatch(uploadParcels(data))
    event.target.value = null;
  }

  getParcelsCounter = (name) => {
    const { parcels } = this.props
    return parcels.filter(parcel => parcel.DepartmentName === name).length
  }

  render() {
    const { departments, parcels } = this.props
    const { modalOpen } = this.state
    return (

      <div>
        <Route exact path="/" render={() => (
          <div>
            <input type="file" onChange={this.handleFileUpload} />
            <ul>
              {departments.map((item) => (
               <li key={item.Name}>
                  <Link to={`/${item.Name}`}> {item.Name}  - {this.getParcelsCounter(item.Name)}</Link>
                </li>
              ))}
            </ul>
            <div>
              <button onClick={() => this.openModal()}>Add Department</button>
              <button>Delete Department</button>
            </div>


            <Modal
              className='modal'
              overlayClassName='overlay'
              isOpen={modalOpen}
              onRequestClose={this.closeModal}
              contentLabel='Modal'
            >
              {modalOpen && <AddControl saveDepartment={this.addDepartment} />}
            </Modal>

          </div>
        )} />
        <Route exact path="/:departmentName" render={(props) => 
          (<ParcelDetail departmentName={props.match.params.departmentName} parcels={parcels}/>)
          } />
    
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  const { departments, parcels } = state
  return {
    departments,
    parcels
  }
}

export default withRouter(connect(
  mapStateToProps
)(App))

