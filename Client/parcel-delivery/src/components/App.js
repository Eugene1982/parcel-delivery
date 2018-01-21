import React, { Component } from 'react';
import { connect } from 'react-redux'
import Modal from 'react-modal'
import './App.css';
import { getDepartments,  addDepartment} from '../actions';
import AddControl from './AddControl'

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
    this.closeModal()
  }


  render() {
    const {departments} = this.props
    const {modalOpen} = this.state
    return (
      <div>
          <ul>
          {departments.map((item) => (
            <li key={item.Name} >
             {item.Name}
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
    );
  }
}

const mapStateToProps = (state) => {
  const { departments } = state
  return {
    departments
  }
}

export default connect(
  mapStateToProps
)(App)
