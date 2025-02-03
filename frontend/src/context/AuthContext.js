import React, { createContext, useState, useEffect } from 'react';
import { login, getUserInfo } from '../api/api';

const AuthContext = createContext();  

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem('token'));


  const handleLogin = async (docNumber, password) => {
    try {
      const response = await login(docNumber, password);
      setToken(response.token);
      localStorage.setItem('token', response.token);
      setUser(response); // Store user info
    } catch (error) {
      console.error('Login failed', error);
      throw error;
    }
  };

  const handleLogout = () => {
    setToken(null);
    localStorage.removeItem('token');
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, token, handleLogin, handleLogout }}>
      {children}
    </AuthContext.Provider>
  );
};

export { AuthContext };
