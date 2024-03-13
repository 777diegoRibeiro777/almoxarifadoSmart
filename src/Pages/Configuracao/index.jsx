import "./_style.scss";
import { useEffect, useState } from "react";
import { FadeLoader } from "react-spinners";
import { ToastContainer, toast } from "react-toastify";
import { validateEmail } from "../../Utils/functions";

export default function Configuracao() {
  const [formContact, setFormContact] = useState({});

  useEffect(() => {
    const dataContact = JSON.parse(localStorage.getItem("dataContact"));
    setFormContact(dataContact);
    console.log("Data Contact:", dataContact);
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormContact((prevData) => ({ ...prevData, [name]: value }));
    console.log("Form Data:", formContact);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (formContact.email.trim() === "" || formContact.whatsapp.trim() === "") {
      toast.error("Preencha todos os campos");
      return;
    }

    // if (
    //   // formContact.zap.length < 11 ||
    //   // validateEmail(formContact.email) != false
    // ) {
    //   toast.error("Campos inválidos");
    //   return;
    // }

    localStorage.setItem("dataContact", JSON.stringify(formContact));

    toast.success("Configurações salvas com sucesso");
  };

  return (
    <>
      <section className=" container">
        <h4 className="titleConfig text-center">Configurações</h4>
        <div className="cardProduto">
          <label htmlFor="email">Email:</label>
          <input
            className="inputEmail" 
            value={formContact.email}
            type="email"
            name="email"
            id="email"
            placeholder="Digite seu email"
            onChange={handleChange}
          />

          <label htmlFor="zap">Whatsapp:</label>
          <input
            className="inputEmail"
            type="text"
            name="whatsapp"
            id="zap"
            value={formContact.whatsapp}
            placeholder="Digite seu whatsapp"
            onChange={handleChange}
          />

          <button className="btnGreen salvarConfig" onClick={handleSubmit}>
            Salvar
          </button>
        </div>
      </section>

      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="dark"
      />
    </>
  );
}
