import React, { useState,useContext,useEffect } from 'react';
import '../styles/Modal.css';  
import {EmployerContext} from "../context/EmployerContext"

const ModalWithPhoneList = ({setIsModalOpen}) => {
  const [phone, setPhone] = useState({
    countryCode:"55",
    regionCode:"11",
    number:''
  });
  const [tableData, setTableData] = useState([]);
  const [editIndex, setEditIndex] = useState(null);
  
  const {getUserPhone,userInfo,addPhoneNumber,updatePhoneNumber,deletePhoneNumber } = useContext(EmployerContext);
  

  useEffect( ()=>{
    console.log("useredit");
    console.log(userInfo);
    if(userInfo != null)
         getUserPhone(userInfo.id).then((response)=>{
             console.log(response.data);
             setTableData(response.data);
         });
        
  },[userInfo])

  const closeModal = () => {
    setPhone({
        countryCode:"55",
        regionCode:"11",
        number:''
      });  
    setEditIndex(null);  
    setIsModalOpen(false)
  };

  const handlePhoneChange = (e) => {
    const { name, value } = e.target;
    setPhone((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };
  
  const handleSubmit = (e) => {
    e.preventDefault();
    const phoneRegex = /^\+?[1-9]\d{1,14}$/; 
    if (!phoneRegex.test(phone.number)) {
      alert('Por favor, insira um número de telefone válido.');
      return;
    }
    addPhoneNumber({
     ...phone,
     userId:userInfo.id   
    })
    if (editIndex !== null) {
      const updatedTableData = [...tableData];
      updatedTableData[editIndex] = { ...phone };
      updatePhoneNumber({...phone,employerId:userInfo.id});
      setTableData(updatedTableData);
    } else {
        console.log(phone);
      setTableData((prevData) => [...prevData, ... phone ]);
    }
    setPhone({
        countryCode:"55",
        regionCode:"11",
        number:''
      });  
  };


  const handleEdit = (index) => {
    setPhone(tableData[index].phone);
    setEditIndex(index);
  };

  const handleDelete = (index) => {
    console.log(tableData[index]);
    deletePhoneNumber(userInfo.id,tableData[index].id)
    const updatedTableData = tableData.filter((_, idx) => idx !== index);
    setTableData(updatedTableData);
  };

  return (
    <div>
        <div className="modal-overlay">
          <div className="modal-content">
            <h2>{editIndex !== null ? 'Edit phone number' : 'Add new Phone number'}</h2>

            {/* Formulário para adicionar ou editar telefone */}
            <form onSubmit={handleSubmit}>
              <div>
                <label>Phone number</label>
                <input
                  type="tel"
                  name="number"
                  value={phone.number}
                  onChange={handlePhoneChange}
                  required
                  placeholder="Digite o número de telefone"
                />
              </div>

              <button type="submit">{editIndex !== null ? 'Atualizar' : 'Adicionar'}</button>
            </form>

            <h3>Lista de Telefones</h3>
            <table>
              <thead>
                <tr>
                  <th>Telefone</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {tableData.map((row, index) => (
                  <tr key={index}>
                    <td>{row.number}</td>
                    <td>
                    <div style={{"display":"flex"}}>
                    <button className="action-btn edit-btn" onClick={() => handleEdit(index)}>
                        <i className="fas fa-pencil-alt"></i>
                      </button>
                      <button className="action-btn delete-btn" onClick={() => handleDelete(index)}>
                        <i className="fas fa-trash"></i>
                      </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
            <button type="button" onClick={closeModal}>Fechar</button>
          </div>
        </div>
    </div>
  );
};

export default ModalWithPhoneList;
