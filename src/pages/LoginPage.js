import React, { useState, useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import '../styles/Styles.css'; 

const LoginPage = () => {
  const { handleLogin } = useContext(AuthContext);
  const [docNumber, setDocNumber] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null); 
  const [errorVisible, setErrorVisible] = useState(false); 

  const handleSubmit = (e) => {
    e.preventDefault();
    setErrorVisible(false); 
    if (docNumber && password) {
      handleLogin(docNumber, password)
        .catch((err) => {
          setError('Invalid credentials');
          setErrorVisible(true); 
          console.error('Login failed:', err);  
        });
    } else {
      setError('Please fill in both fields');
      setErrorVisible(true); 
    }
  };

  return (
    <div className="login-container">
      <div className="inner-container">
      <h2>Login</h2>
      {error && (
        <div className={`error-message ${errorVisible ? 'visible' : ''}`}>
          {error}
        </div>
      )}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Document Number</label>
          <input
            type="text"
            value={docNumber}
            onChange={(e) => setDocNumber(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Password</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit">Login</button>
      </form>
      </div>
    </div>
  );
};

export default LoginPage;
