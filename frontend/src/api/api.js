import axios from 'axios';
import { useReducer } from 'react';


const api = axios.create({
  baseURL: "http://localhost:7017", 
  headers: {
    'Content-Type': 'application/json',
  },
});

export const login = async (document, password) => {
  try {
    console.log("realizando login");  
    const response = await api.post('/Authentication', { document, password });
    return response.data;
  } catch (error) {
    console.error("Error logging in", error);
    throw error;
  }
};

export const updateUser = async (userdata,setErrorMessage) => {
  try {
    const token = localStorage.getItem('token');  
    console.log(userdata);
    
    const response = await api.put('/Employer/'+userdata.id, 
      userdata,{
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log(response.data);
    setErrorMessage("");
    return response.data;
  } catch (error) {
    console.error("Error fetching user info", error);
    setErrorMessage(error.response.data);
  }
};


export const getUser = async (id) => {
  try {
    const token = localStorage.getItem('token');  
    const response = await api.get('/Employer/'+id, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching user info", error);
    throw error;
  }
};

export const deleteUser = async (id) => {
  try {
    const token = localStorage.getItem('token');  
    const response = await api.delete('/Employer/'+id, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching user info", error);
    throw error;
  }
};

export const getUserInfo = async (page, count, document) => {
  try {
    const token = localStorage.getItem('token');  

    const response = await api.get('/Employer/', {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {
        page,      
        count,      
        document    
      }
    });

    return response.data;
  } catch (error) {
    console.error("Error fetching user info", error);
    throw error;
  }
};
  
  export const createUser = async (userData, setErrorMessage) => {
    const token = localStorage.getItem('token');  

    try {
      console.log(userData);
      const response = await api.post(
        'Employer', 
        userData,
        {
          headers: {
            'Authorization': `Bearer ${token}`,  
          }
        }
      );
      setErrorMessage('');
      alert('Usuário criado com sucesso!');  
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  };

  export const addPhone = async (phone,setErrorMessage) =>{
    const token = localStorage.getItem('token');  
    try {
      const response = await api.post(
        'PhoneNumber/'+phone.userId,
        phone,
        {
          headers: {
            'Authorization': `Bearer ${token}`,  
          }
        }
      );
      setErrorMessage('');
      alert('Phone added!');  
      return response;
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  }

  export const deletePhone = async (userId,phoneId,setErrorMessage) =>{
    const token = localStorage.getItem('token');  
    try {
      const response = await api.delete(
        'PhoneNumber/'+userId+'/'+phoneId,
        {
          headers: {
            'Authorization': `Bearer ${token}`,  
          }
        }
      );
      setErrorMessage('');
      alert('Phone deleted!');  
      return response;
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  }

  export const updatePhone = async (phone,setErrorMessage) =>{
    const token = localStorage.getItem('token');  
    try {
      const response = await api.put(
        'PhoneNumber/'+phone.employerId+'/2',
        phone,
        {
          headers: {
            'Authorization': `Bearer ${token}`,  
          }
        }
      );
      setErrorMessage(''); 
      return response;
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  }

  export const getUserPhoneNumber = async (id, setErrorMessage) => {
    const token = localStorage.getItem('token');  
    try {
      const response = await api.get(
        'PhoneNumber/'+id,
        {
          headers: {
            'Authorization': `Bearer ${token}`,  
          }
        }
      );
      setErrorMessage('');
      return response;
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  };

  export default api;