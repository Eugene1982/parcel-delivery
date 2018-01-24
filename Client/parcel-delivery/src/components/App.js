import React, { Component } from 'react'
import { connect } from 'react-redux'
import Modal from 'react-modal'
import { Route, Link, withRouter } from 'react-router-dom';
import './App.css';
import { getDepartments, addDepartment, deleteDepartment, uploadParcels, clearParcels } from '../actions';
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
    dispatch(addDepartment(department))
    dispatch(clearParcels())
    this.closeModal()
  }

  deleteDepartment = (name) => {
    const { dispatch } = this.props
    dispatch(deleteDepartment(name))
    dispatch(clearParcels())
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

  clear = () => {
    const { dispatch } = this.props
    dispatch(clearParcels())
  }

  render() {
    const { departments, parcels, error } = this.props
    const { modalOpen } = this.state
   
    if (error) {
      return (
        <div>
          <div className="error-message">Server Error: {error}</div>
          <Link to="/" onClick={this.clear}>Return to main page</Link>
        </div>
      )
    }
    return (

      <div>
        <Route exact path="/" render={() => (
          <div className="dep-list">
            Parcels: <input type="file" onChange={this.handleFileUpload} />
            <h3>Departments</h3>
            <ul>
              {departments.map((item) => (
                <li key={item.Name}>
                  <Link to={`/${item.Name}`}> {item.Name} 
                    <div className="detail-counter">Amount: <p>{this.getParcelsCounter(item.Name)}</p></div>
                  </Link>
                  <button onClick={() => this.deleteDepartment(item.Name)}>Delete Department</button>
                </li>
              ))}
            </ul>
            <div>
              <button onClick={() => this.openModal()}>Add Department</button>
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
          (<ParcelDetail departmentName={props.match.params.departmentName} parcels={parcels} />)
        } />

      </div>
    );
  }
}

const mapStateToProps = (state) => {
  const { departments, parcels, error } = state
  return {
    departments,
    parcels,
    error
  }
}

export default withRouter(connect(
  mapStateToProps
)(App))

