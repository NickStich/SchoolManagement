import React, { Component } from 'react';
import Modal from 'react-modal';
import AddStudentPage from './AddStudentPage.js';
import './StudentList.css';
import { getStudents, addStudent, editStudent, deleteStudent } from '../../api.js'

Modal.setAppElement('#root');

export class StudentList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      students: [],
      isModalOpen: false,
      selectedStudent: null,
      expandedStudentId: null,
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
    this.setState({ isModalOpen: true, selectedStudent: null });
  };

  openEditModal = (student) => {
    this.setState({ isModalOpen: true, selectedStudent: student });
  };

  closeModal = () => {
    console.log('THIS');
    this.handleGetStudents();
    this.setState({ isModalOpen: false });
  };

  handleAddStudent  = (newStudent) => {
    addStudent(newStudent);
    this.closeModal();
  };

  handleEditStudent = async (editedStudent) => {
    try {
        await editStudent(editedStudent.id, editedStudent);
        this.closeModal();
      } catch (error) {
        console.error('Error editing student:', error.message);
      }
  };
  
  handleDeleteStudent = async (studentId) => {
    try {
        await deleteStudent(studentId);
        this.handleGetStudents(); // Refresh the student list after deletion
      } catch (error) {
        console.error('Error deleting student:', error.message);
      }
  };

  handleItemClick = (studentId) => {
    this.setState((prevState) => ({
      expandedStudentId: prevState.expandedStudentId === studentId ? null : studentId,
    }));
  };

  render() {
    const { students, isModalOpen, selectedStudent } = this.state;

    return (
        <div className="student-list-container">
          <h2>Student List</h2>
          <button onClick={this.openModal}>Add New Student</button>
          <ul className="student-list">
            {students.map((student) => (
            <li
              key={student.id}
              className={`student-item ${this.state.expandedStudentId === student.id ? 'expanded' : ''}`}
              onClick={() => this.handleItemClick(student.id)}
            >
                <span className="student-name">
                  {student.firstName} {student.lastName}
                </span>
                <span className="student-grade">Grade: {student.grade}</span>

                {this.state.expandedStudentId === student.id && (
                  <div className="expanded-content">
                    {/* Render the image or additional content here
                    <img src={student.profilePictureUrl} alt={`Profile of ${student.firstName}`} /> */}
                    <input
                      type="file"
                      accept="image/*"
                    />
                  </div>
                )}

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
          onClose={this.closeModal}
          initialData={selectedStudent}
        />
        <button onClick={this.closeModal}>Close</button>
      </Modal>
        </div>
      );
  }
}

