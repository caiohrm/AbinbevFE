import React, { useContext, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import { AuthProvider, AuthContext } from './context/AuthContext';  
import LoginPage from './pages/LoginPage';


import Main from './pages/Main';  
function App() {
  const { user } = useContext(AuthContext); 
  
  useEffect(() => {
    console.log(user);
  }, [user]);  

  return (
      <Router>
        <Routes>
          <Route 
            path="/login" 
            element={user ? <Navigate to="/dashboard" /> : <LoginPage />} 
          />
          
          <Route 
            path="/dashboard" 
            element={user ? <Main /> : <Navigate to="/login" />} 
          />
          <Route 
            path="/" 
            element={<Navigate to="/login" />} 
          />
        </Routes>
      </Router>
  );
}

export default App;
