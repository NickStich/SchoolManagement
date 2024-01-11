import React, { Component, useState, useEffect } from 'react';
import Modal from 'react-modal';
import AddStudentPage from './AddStudentPage.js';
import './StudentList.css';
import { getStudents, addStudent, editStudent, deleteStudent, getFileByName } from '../../api.js'

import userImg from '../../assets/user.png'

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

    // Create a ref for the file input
    this.fileInputRef = React.createRef();
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
        this.handleGetStudents(); 
      } catch (error) {
        console.error('Error deleting student:', error.message);
      }
  };

  handleItemClick = async (studentId) => {

    const fileName = this.state.students.find(x => x.id === studentId)?.profilePictureFileName;

    if(fileName){
      this.getProfilePicture(studentId)
      .then((expandedStudentImage) => {
        this.setState(
          (prevState) => ({
            expandedStudentId: prevState.expandedStudentId === studentId ? null : studentId,
            expandedStudentImage,
          }),
          () => {
            // If the image is present, trigger the file input click event
            if (this.state.expandedStudentImage) {
              console.log(this.fileInputRef.current);
              console.log('fsdfsdf');
            }
          });
      })
      .catch((error) => {
        console.error('Error loading image:', error);
      });
    }
    else {
      this.setState((prevState) => ({
        expandedStudentId: prevState.expandedStudentId === studentId ? null : studentId,
        expandedStudentImage: userImg
      }));
    }
  };

  getProfilePicture = async (studentId) => {
    const fileName = this.state.students.find(x => x.id === studentId)?.profilePictureFileName;

    try {
      const blob = await getFileByName(fileName);
      const result = URL.createObjectURL(blob);
      return result;
    } catch (error) {
      console.error('Error loading image:', error);
    }
  }

  render() {
    const { students, isModalOpen, selectedStudent, expandedStudentImage  } = this.state;

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
                  <div>
                    <div className="expanded-content">
                      <img src={expandedStudentImage || userImg} alt="My Image" />


                    </div>
                    <input
                      type="file"
                      ref={this.fileInputRef}
                      // style={{ display: 'none' }}
                      onChange={(e) => {
                        // Handle the file upload logic here
                        console.log('File uploaded:', e.target.files[0]);
                      }}

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

