import React, { Component } from 'react';
import Modal from 'react-modal';
import AddStudentPage from './AddStudentPage.js';
import './StudentList.css';
import { getStudents, addStudent, editStudent, deleteStudent } from '../api'

Modal.setAppElement('#root');

export class StudentList extends Component {
  constructor(props) {
    super(props);
    this.state = {
      students: [],
    };
  }

  componentDidMount() {
    this.handleGetStudents();
  }

  handleGetStudents = () => {
    getStudents()
      .then((data) => {
        this.setState({ students: data, isModalOpen: false, });
      })
      .catch((error) => {
        console.error('Error fetching student data:', error);
      });
  }

  openModal = () => {
    this.setState({ isModalOpen: true });
  };

  openEditModal = (student) => {
    this.setState({ isModalOpen: true, selectedStudent: student });
  };

  closeModal = () => {
    console.log('THIS');
    this.handleGetStudents();
    this.setState({ isModalOpen: false });
  };

  handleAddStudent  = (e) => {
    e.preventDefault();
    const { firstName, lastName, grade } = this.state;
    const newStudent = {
      firstName,
      lastName,
      grade,
    };
    this.closeModal();

    addStudent(newStudent);
    // Optionally, you can reset the form fields here
    // this.setState({
    //   firstName: '',
    //   lastName: '',
    //   grade: '',
    // });
    console.log('this');


  };

  handleEditStudent = async (editedStudent) => {
    try {
        await editStudent(editedStudent.id, editedStudent);
        this.handleGetStudents(); // Refresh the student list after editing
        this.closeModal();
      } catch (error) {
        // Handle error, e.g., show an error message
        console.error('Error editing student:', error.message);
      }
  };
  
  handleDeleteStudent = async (studentId) => {
    try {
        await deleteStudent(studentId);
        this.handleGetStudents(); // Refresh the student list after deletion
      } catch (error) {
        // Handle error, e.g., show an error message
        console.error('Error deleting student:', error.message);
      }
  };

  render() {
    const { students, isModalOpen, selectedStudent } = this.state;


    return (
        <div className="student-list-container">
          <h2>Student List</h2>
          <button onClick={this.openModal}>Add New Student</button>
          <ul className="student-list">
            {students.map((student) => (
              <li key={student.id} className="student-item">
                <span className="student-name">
                  {student.firstName} {student.lastName}
                </span>
                <span className="student-grade">Grade: {student.grade}</span>

                <button onClick={() => this.openEditModal(student)}>Edit</button>
                <button onClick={() => this.handleDeleteStudent(student.id)}>Delete</button>
              </li>
            ))}
          </ul>

          <Modal
        isOpen={isModalOpen}
        onRequestClose={this.closeModal}
        contentLabel={selectedStudent ? "Edit Student Modal" : "Add Student Modal"}
      >
        <AddStudentPage
          onSubmit={selectedStudent ? this.handleEditStudent : this.handleAddStudent}
          initialData={selectedStudent}
        />
        <button onClick={this.closeModal}>Close</button>
      </Modal>
        </div>
      );
  }
}

