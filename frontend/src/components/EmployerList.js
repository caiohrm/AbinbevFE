import React, { useState, useEffect,useContext } from 'react';
import { getUserInfo,deleteUser } from '../api/api';
import '../styles/EmployerList.css';  
import ModalWithPhoneList  from '../pages/PhoneEdit'
import '@fortawesome/fontawesome-free/css/all.min.css';
import {EmployerContext} from "../context/EmployerContext"

const EmployerList = () => {
  
  const [totalItems, setTotalItems] = useState(0);
  const { userInfo,getUserInfoToEdit,employers,getEmployers } = useContext(EmployerContext);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(5); 
  const [isModalOpen, setIsModalOpen] = useState(false);
  
  const [searchTerm, setSearchTerm] = useState('');

  const fetchEmployers = async () => {
    try {
      await getEmployers(currentPage - 1, itemsPerPage,'');
      setTotalItems(1);  
    } catch (error) {
      console.error('Erro ao buscar dados', error);
    }
  };

  
  useEffect(() => {
    fetchEmployers();
  }, [currentPage, itemsPerPage]);  

  
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  
  const getPaginatedData = () => {
    return employers.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage);
  };

  
  const totalPages = Math.ceil(totalItems / itemsPerPage);

  
  const handleSearchChange = (e) => {
    const value = e.target.value;
    setSearchTerm(value);

    if (value.length >= 3) {
      getEmployers(currentPage - 1, itemsPerPage,value);
    } else {
      fetchEmployers();
    }
  };

  
  const handleDelete = async (id) => {
    console.log('Excluindo empregador com ID:', id);
    await deleteUser(id);
    fetchEmployers();
  };

  
  const handleEdit = (id) => {
    console.log("Entrou aqui para editar");
    getUserInfoToEdit(id);
  };

  const setModal = (id) => {
    getUserInfoToEdit(id);
    setIsModalOpen(true);
  };
  return (
    <div className="employer-list-container">
      <div className="search-container">
        <input
          type="text"
          placeholder="Buscar por Documento"
          value={searchTerm}
          onChange={handleSearchChange}
          className="search-input"
        />
      </div>

      <table className="employer-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Document Number</th>
            <th>Birth Date</th>
            <th>Actions</th> 
          </tr>
        </thead>
        <tbody>
          {/* Exibindo os empregadores filtrados */}
          {getPaginatedData().length > 0 ? (
            getPaginatedData().map((employer) => (
              <tr key={employer.id}>
                <td>{employer.id}</td>
                <td>{employer.firstName} {employer.lastName}</td>
                <td>{employer.email}</td>
                <td>{employer.docNumber}</td>
                <td>{new Date(employer.birthDate).toLocaleDateString()}</td>
                <td className="action-buttons">
                  <button className="action-btn edit-btn" onClick={() => handleEdit(employer.id)}>
                    <i className="fas fa-pencil-alt"></i>
                  </button>
                  <button className="action-btn delete-btn" onClick={() => handleDelete(employer.id)}>
                    <i className="fas fa-trash"></i>
                  </button>
                  <button className="action-btn edit-btn" onClick={() => setModal(employer.id)}>
                    <i className="fas fa-phone-alt"></i>
                  </button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6">Nenhum empregador encontrado</td>
            </tr>
          )}
        </tbody>
      </table>
      {isModalOpen && (<ModalWithPhoneList setIsModalOpen={setIsModalOpen}/>)}
    </div>
  );
};

export default EmployerList;
