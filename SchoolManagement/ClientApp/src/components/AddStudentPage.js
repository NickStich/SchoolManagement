import React, { Component } from 'react';
import './AddStudentPage.css'

import {addStudent} from '../api';

export class AddStudentPage extends Component {
  constructor(props) {
    super(props);

    console.log(props);

    this.state = {
      firstName: '',
      lastName: '',
      grade: '',
    };
  }

  componentDidMount() {
    const { initialData } = this.props;
    console.log(initialData);
    if (initialData) {
      this.setState({
        formData: {
          firstName: initialData.firstName || '',
          lastName: initialData.lastName || '',
          grade: initialData.grade || '',
        },
      });
    }
  }

  // handleSubmit = (e) => {
  //   e.preventDefault();
  //   const { firstName, lastName, grade } = this.state;
  //   const newStudent = {
  //     firstName,
  //     lastName,
  //     grade,
  //   };
  //   addStudent(newStudent);
  //   // Optionally, you can reset the form fields here
  //   this.setState({
  //     firstName: '',
  //     lastName: '',
  //     grade: '',
  //   });
  // };

  handleChange = (e) => {
    const { name, value } = e.target;
    this.setState({
      [name]: value,
    });
  };

  handleSubmit = (e) => {
    e.preventDefault();
    const { firstName, lastName, grade } = this.state;
    const newStudent = {
      firstName,
      lastName,
      grade,
    };
    addStudent(newStudent);
    // Optionally, you can reset the form fields here
    this.setState({
      firstName: '',
      lastName: '',
      grade: '',
    });
  };

  render() {
    const { firstName, lastName, grade } = this.props.initialData;

    console.log(this.state);

    console.log(this.props.initialData);

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
