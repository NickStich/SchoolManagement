import React, { Component, useState, useEffect } from 'react';
import Modal from 'react-modal';
import AddStudentPage from './AddStudentPage.js';
import './StudentList.css';
import { getStudentById, getStudents, addStudent, editStudent, deleteStudent, getFileByName, uploadStudentProfilePicture } from '../../api.js'

import userImg from '../../assets/user.png'
import chevronDownIcon from '../../assets/chevronDownIcon.png'
import chevronUpIcon from '../../assets/chevronUpIcon.png'

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

    this.fileInputRef = React.createRef();
  }

  componentDidMount() {
    this.handleGetStudents();
  }

  //#region API calls

  handleGetStudents = () => {
    getStudents()
      .then((data) => {
        this.setState({ students: data, isModalOpen: false, });
      })
      .catch((error) => {
        console.error('Error fetching student data:', error);
      });
  }

  handleGetStudent = async (studentId) => {
    try {
      const data = await getStudentById(studentId);
      console.log(data);
      return data;
    } catch (error) {
      console.error('Error fetching student data:', error);
      throw error;
    }
  }

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

  //#endregion

  //#region Modal

  openModal = () => {
    this.setState({ isModalOpen: true, selectedStudent: null });
  };

  openEditModal = (student) => {
    this.setState({ isModalOpen: true, selectedStudent: student });
  };

  closeModal = () => {
    this.setState({ isModalOpen: false });
    this.handleGetStudents();
  };
  
  //#endregion

  handleItemClick = async (studentId) => {
    const fileName = this.state.students.find(x => x.id === studentId)?.profilePictureFileName;

    if(fileName){
      this.displayStudentProfilePicture(studentId);
    }
    else {
      this.setState((prevState) => ({
        expandedStudentId: prevState.expandedStudentId === studentId ? null : studentId,
        expandedStudentImage: userImg
      }));
    }
  };

  updateStateStudentById = (studentId, updatedStudentData) => {
    this.setState((prevState) => {
      const updatedStudents = prevState.students.map((student) => {
        if (student.id === studentId) {
          return { ...student, ...updatedStudentData };
        }
        return student;
      });
  
      return { students: updatedStudents };
    });
  };

  //#region Profile Picture

  displayStudentProfilePicture = async (studentId) => {
    this.getProfilePicture(studentId)
    .then((expandedStudentImage) => {
      this.setState(
        (prevState) => ({
          expandedStudentId: prevState.expandedStudentId === studentId ? null : studentId,
          expandedStudentImage,
        }),
        () => {
          if (this.state.expandedStudentImage) {
            console.log(this.fileInputRef.current);
          }
        });
    })
    .catch((error) => {
      console.error('Error loading image:', error);
    });
  }

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

  handleFileUpload = async (studentId, e) => {
    const file = e.target.files[0];
  
    await uploadStudentProfilePicture(studentId, file);
    const studentWithPicture = await this.handleGetStudent(studentId);

    this.updateStateStudentById(studentId, studentWithPicture)
    this.displayStudentProfilePicture(studentId);
  }

  //#endregion

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
              
            >
                <img
                  className="chevron-icon"
                  src={this.state.expandedStudentId === student.id ? chevronUpIcon : chevronDownIcon}
                  alt="Chevron Icon"
                  onClick={() => this.handleItemClick(student.id)}
                />

                <span className="student-name">
                  {student.firstName} {student.lastName}
                </span>
                <span className="student-grade">  Grade: {student.grade}</span>


              {this.state.expandedStudentId === student.id && (
                <div>
                  <div className="expanded-content">
                    <img src={expandedStudentImage || userImg} alt="My Image" />
                  </div>
                  <input
                    type="file"
                    ref={this.fileInputRef}
                    onChange={(e) => {
                      console.log('File uploaded:', e.target.files[0]);
                      this.handleFileUpload(this.state.expandedStudentId, e);
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

