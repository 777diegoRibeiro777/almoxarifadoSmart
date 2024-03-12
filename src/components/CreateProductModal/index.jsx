/* eslint-disable react/prop-types */
import { useState } from "react";
import "./_style.scss";
import { API } from "../../api/API";

export default function CreateProductModal({ handleShowModal }) {
  const [formData, setFormData] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
    console.log("Form Data:", formData);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (
      formData.descricao.trim() === "" ||
      formData.estoqueAtual <= 0 ||
      formData.estoqueMinimo <= 0
    ) {
      alert("Preencha todos os campos obrigatórios");
      return;
    }

    console.log("Form Data:", formData);

    API.post("/Produtos", formData).then((response) => {
      console.log("Response:", response);
    });

    handleShowModal();
  };

  return (
    <div className="container-product-modal">
      <div className="content-product-modal">
        <div className="flex-column-center">
          <h4 className="mb-5 ">Novo Produto</h4>
        </div>
        <div className="">
          <form className="form-product-modal" onSubmit={handleSubmit}>
            <div className="flex-column-center">
              <label htmlFor="descricao">Nome</label>
              <input type="text" name="descricao" onChange={handleChange} />
            </div>
            <div className="flex-column-center">
              <label htmlFor="estoqueAtual">Estoque Atual</label>
              <input type="text" name="estoqueAtual" onChange={handleChange} />
            </div>
            <div className="flex-column-center">
              <label htmlFor="estoqueMinimo">Estoque Mínimo</label>
              <input type="text" name="estoqueMinimo" onChange={handleChange} />
            </div>
            <div className="flex-center mt-3">
              <button type="submit" className="btnGreen w-50 mr-2">
                Salvar
              </button>
              <button
                type="submit"
                onClick={handleShowModal}
                className="btnRed w-50"
              >
                Cancelar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
