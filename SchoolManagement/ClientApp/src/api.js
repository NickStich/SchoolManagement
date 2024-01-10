const BASE_URL = 'https://localhost:7148'


export const addStudent = async (student) => {
        const { firstName, lastName, grade } = student;
    
        try {
          const response = await fetch(`${BASE_URL}/student`, {
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
}

export const getStudents = async () => {
    try {
        const response = await fetch(`${BASE_URL}/student`);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching student data:', error);
        throw error; // Rethrow the error for the calling code to handle
    }
  };

  export const editStudent = async (studentId, updatedData) => {
    try {
      const response = await fetch(`${BASE_URL}/student/${studentId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedData),
      });
  
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
  
      // Handle success, e.g., show a success message or redirect
      console.log('Student edited successfully');
    } catch (error) {
      // Handle error, e.g., show an error message
      console.error('Error editing student:', error.message);
      throw error;
    }
  };


  export const deleteStudent = async (studentId) => {
    try {
      const response = await fetch(`${BASE_URL}/student/${studentId}`, {
        method: 'DELETE',
      });
  
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
  
      // Handle success, e.g., show a success message or redirect
      console.log('Student deleted successfully');
    } catch (error) {
      // Handle error, e.g., show an error message
      console.error('Error deleting student:', error.message);
      throw error;
    }
  };