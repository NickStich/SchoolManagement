import React, { Component } from 'react';

export class AddStudentPage extends Component {
  constructor(props) {
    super(props);

    // Initialize state
    this.state = {
      firstName: '',
      lastName: '',
      grade: '',
    };
  }

  // Handle input changes and update state
  handleInputChange = (e) => {
    this.setState({
      [e.target.name]: e.target.value,
    });
  };

  // Handle the add student logic
  handleAddStudent = async () => {
    const { firstName, lastName, grade } = this.state;

    try {
      const response = await fetch('https://localhost:44412/student', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          firstName,
          lastName,
          grade,
        }),
      });

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      // Handle success, e.g., show a success message or redirect
      console.log('Student added successfully');
    } catch (error) {
      // Handle error, e.g., show an error message
      console.error('Error adding student:', error.message);
    }
  };

  render() {
    const { firstName, lastName, grade } = this.state;

    return (
      <div>
        <h2>Add Student</h2>
        <form>
          <div>
            <label>First Name:</label>
            <input
              type="text"
              name="firstName"
              value={firstName}
              onChange={this.handleInputChange}
            />
          </div>
          <div>
            <label>Last Name:</label>
            <input
              type="text"
              name="lastName"
              value={lastName}
              onChange={this.handleInputChange}
            />
          </div>
          <div>
            <label>Grade:</label>
            <input type="text" name="grade" value={grade} onChange={this.handleInputChange} />
          </div>
          <button type="button" onClick={this.handleAddStudent}>
            Add Student
          </button>
        </form>
      </div>
    );
  }
}