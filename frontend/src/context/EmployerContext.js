import React, { createContext, useState, useEffect } from 'react';
import { login, getUser,getUserPhoneNumber,addPhone,updatePhone,deletePhone,getUserInfo } from '../api/api';

const EmployerContext = createContext(); 

export const EmployerProvider = ({ children }) => {
  const [userInfo, setUserInfo] = useState(null);
  const [employers, setEmployers] = useState([]);
  

  const getUserInfoToEdit = async (id) => {
    try {
      const response = await getUser(id);
      setUserInfo(response); 
    } catch (error) {
      console.error('Get user data failed', error);
      throw error;
    }
  };

  const getUserPhone = async (id) =>{
    return await getUserPhoneNumber(id,(vari)=>{});
  }

  const addPhoneNumber = async (phone) =>{
    const response = await addPhone(phone,(vari)=>{});
  }

  const updatePhoneNumber = async (phone) =>{
    const response = await updatePhone(phone,(vari)=>{});
  }

  const deletePhoneNumber = async (userId,phoneId) => {
    console.log("deletando");
    const response = await deletePhone(userId,phoneId,()=>{});
  }

  const getEmployers = async (currentPage, itemsPerPage,document) => {
    try {
      const response = await getUserInfo(currentPage,itemsPerPage,document);
      setEmployers(response);  
    } catch (error) {
      console.error('Get user data failed', error);
      throw error;
    }
  };

  return (
    <EmployerContext.Provider value={{ userInfo,employers,getUserInfoToEdit,setUserInfo,getUserPhone,addPhoneNumber,updatePhoneNumber,deletePhoneNumber,getEmployers}}>
      {children}
    </EmployerContext.Provider>
  );
};


export { EmployerContext };
