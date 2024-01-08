import React, { Component } from 'react';
import './AddStudentPage.css'

import {addStudent} from '../../api';

export class AddStudentPage extends Component {
  constructor(props) {
    super(props);

    this.state = {
      firstName: '',
      lastName: '',
      grade: '',
      id: '',
    };
  }

  componentDidMount() {
    const { initialData } = this.props;
    if (initialData) {
      this.setState({
        firstName: initialData.firstName || '',
        lastName: initialData.lastName || '',
        grade: initialData.grade || '',
        id: initialData.id || '',
      });
    }
  }
  

  handleChange = (e) => {
    const { name, value } = e.target;
    this.setState({
      [name]: value,
    });
  };

  handleSubmit = (e) => {
    e.preventDefault();
    const { firstName, lastName, grade, id } = this.state;
    const newStudent = {
      firstName,
      lastName,
      grade,
      id
    };
    this.props.onSubmit(newStudent);
    this.props.onClose();
    this.setState({
      firstName: '',
      lastName: '',
      grade: '',
      id: '',
    });
  };

  render() {
    const { firstName, lastName, grade } = this.state;

    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          First Name:
          <input
            type="text"
            name="firstName"
            value={firstName}
            onChange={this.handleChange}
          />
        </label>
        <label>
          Last Name:
          <input
            type="text"
            name="lastName"
            value={lastName}
            onChange={this.handleChange}
          />
        </label>
        <label>
          Grade:
          <input
            type="text"
            name="grade"
            value={grade}
            onChange={this.handleChange}
          />
        </label>
        <button type="submit">{this.props.initialData ? 'Edit Student' : 'Add Student'}</button>
      </form>
    );
  }
}

export default AddStudentPage;
