import React, { Component } from 'react';
import { connect } from 'react-redux'
import './App.css';
import { getDepartments } from '../actions';

class App extends Component {
  componentDidMount() {
    const { dispatch } = this.props
    dispatch(getDepartments())
  }


  render() {
    const {departments} = this.props
    return (
      <div>
          <ul>
          {departments.map((item) => (
            <li key={item.Name} >
             {item.Name}
            </li>
          ))}
        </ul>
      
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

