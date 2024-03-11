import React, { useState } from "react";
import "./_style.scss";
import { API } from "../../api/API";
import closeButton from "../../assets/close.svg"
import modalCloseButton from "../../assets/closeIcon.svg"

export default function CreateProductModal({ handleShowModal }) {
  const [formData, setFormData] =
    useState (
      {
        id: 0,
        descricao: "",
        preco: 0,
        estoqueAtual: 0,
        estoqueMinimo: 0,
      }
    )

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (
      formData.descricao.trim() === "" ||
      formData.preco <= 0 ||
      formData.estoqueAtual < 0 ||
      formData.estoqueMinimo < 0
    ) {
      alert("Preencha todos os campos obrigatórios");
      return;
    }

    console.log("Form Data:", formData);

    // API.post("/Produtos", formData).then((response) => {
    //   console.log("Response:", response);
    // });

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
            <label htmlFor="preco">Preço</label>
            <input type="text" name="preco" onChange={handleChange} />
          </div>
          <div className="flex-column-center">
            <label htmlFor="estoqueAtual">Estoque Atual</label>
            <input type="text" name="estoqueAtual" onChange={handleChange} />
          </div>
          <div className="flex-column-center">
            <label htmlFor="estoqueMinimo">Estoque Mínimo</label>
            <input type="text" name="estoqueMinimo" onChange={handleChange} />
          </div>
          <div className="flex-center mt-2">
            <button type="submit" className="btnGreen w-100">
              Salvar
            </button>
          </div>
          <div className="flex-center mt-2">
            <button type="submit" onClick={handleShowModal} className="btnRed w-100">
              Cancelar
            </button>
          </div>
        </form>
      </div>
      </div>
    </div>
  );
}
