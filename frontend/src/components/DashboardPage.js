import React, { useEffect, useState,useContext } from 'react';
import { createUser,updateUser } from '../api/api.js';  
import '../styles/DashboardPage.css';  
import {EmployerContext} from "../context/EmployerContext"

const DashboardPage = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    docNumber: '',
    role: 0,  
    password: '',
    birthDate: '' ,
    id:0, 
  });
  
  const { userInfo,getEmployers } = useContext(EmployerContext);
  
  const [errors, setErrors] = useState([]);

  useEffect(()=>{
    if(userInfo)
      setFormData({
        ...userInfo,
        birthDate: new Date(userInfo.birthDate).toISOString().split('T')[0]
      })
  },[userInfo]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  const clearData = () =>{
    setFormData({
      firstName: '',
      lastName: '',
      email: '',
      docNumber: '',
      role: 0,  
      password: '',
      birthDate: ''
    })
    getEmployers(0,5,'');
  }

  const onSubmit = (e) => {
    e.preventDefault();
    setErrors({})
    const submitData = {
      ...formData,
      role: parseInt(formData.role, 10),  
      birthDate: formData.birthDate ? new Date(formData.birthDate).toISOString() : null,  
    };

    if(formData != null && formData.id == null)
      createUser(submitData, (errorList) => {
        if (errorList && errorList.length > 0) {
          setErrors(errorList);
        }else{
          clearData()
        }

      });
    else{
      console.log("Aqui vamos fazer o update");
      updateUser(submitData,(errorList) => {
        if (errorList && errorList.length > 0) {
          setErrors(errorList);
        }else{
          clearData()
        }

      });
    }
    
  };

  
  const getErrorMessage = (field) => {
    const error = Array.isArray(errors) && errors.find((error) => error.propertyName.toLowerCase() === field.toLowerCase());
    return error ? error.errorMessage : '';
  };

  return (
    <div className="dashboard-container">
      <form onSubmit={onSubmit}>
      <div className="form-row">
      <div className="child">
        <label>First Name:</label>
        <input
          type="text"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          required
        />
        {getErrorMessage('firstName') && (
          <div className="error-message">{getErrorMessage('firstName')}</div>
        )}
      </div>

      <div className="child">
        <label>Last Name:</label>
        <input
          type="text"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          required
        />
        {getErrorMessage('lastName') && (
          <div className="error-message">{getErrorMessage('lastName')}</div>
        )}
      </div>
    </div>
      <div className="form-row">
        <div className="child">
              <label>Email:</label>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
              />
              {getErrorMessage('email') && (
                <div className="error-message">{getErrorMessage('email')}</div>
              )}
          </div>

          <div className="child">
            <label>Document Number:</label>
            <input
              type="text"
              name="docNumber"
              value={formData.docNumber}
              onChange={handleChange}
              required
            />
            {getErrorMessage('docNumber') && (
              <div className="error-message">{getErrorMessage('docNumber')}</div>
            )}
          </div>

          <div className="child">
            <label>Role:</label>
            <select
              name="role"
              value={formData.role}
              onChange={handleChange}
              required
            >
              <option value="0">Select</option>
              <option value="1">Employer</option>
              <option value="2">Manager</option>
              <option value="3">Director</option>
            </select>
            {getErrorMessage('role') && (
              <div className="error-message">{getErrorMessage('role')}</div>
            )}
        </div>
      </div>
      <div className="form-row">
        

        <div className="child">
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            disabled={formData.id != null}
            required
          />
          {getErrorMessage('password') && (
            <div className="error-message">{getErrorMessage('password')}</div>
          )}
        </div>
        <div className="child">
          <label>Birth Date:</label>
          <input
            type="date"
            name="birthDate"
            value={formData.birthDate}
            onChange={handleChange}
            required
          />
          {getErrorMessage('birthDate') && (
            <div className="error-message">{getErrorMessage('birthDate')}</div>
          )}
        </div>
      </div>
      <div className="form-row">
        <button type="submit">Save</button>
        <button hidden={formData.password != null} onClick={clearData}>New employer</button>
        </div>
      </form>

      {/* Alerta de erro estilizado geral */}
      {errors.length > 0 && (
        <div className="error-alert">
          <strong>Error(s):</strong>
            <div>{errors}</div>
        </div>
      )}
    </div>
  );
};

export default DashboardPage;
