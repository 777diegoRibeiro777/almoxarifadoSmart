import "./_style.scss";
// import lampGreenIcon from "../../assets/icons/icons8-light-green.png";
// import lampRedIcon from "../../assets/icons/icons8-light-red.png";
// import emailIcon from "../../assets/Icons/icons8-nova-mensagem-48.png";
// import deleteIcon from "../../assets/Icons/icons8-remover-48.png";
// import editIcon from "../../assets/Icons/icons8-editar-48.png";
import CreateProductModal from "../../components/CreateProductModal";
import { useEffect, useState } from "react";
import Modal from "react-modal";
import { API } from "../../api/API";
import playButton from "../../assets/play.svg";
import closeButton from "../../assets/close.svg";
import editButton from "../../assets/edit.svg";
import emailBlockButton from "../../assets/emailGrey.svg";
import lampBlueButton from "../../assets/lampBlue.svg";
import lampRedButton from "../../assets/lampRed.svg";
import emailActiveButton from "../../assets/emailActive.svg";
import CustomModal from "../../components/CustomModal";
import { ToastContainer, toast } from "react-toastify";

Modal.setAppElement("#root");

export default function Produtos() {
  const [modalIsOpenDelete, setIsOpenDelete] = useState(false);
  const [modalIsOpenEdit, setIsOpenEdit] = useState(false);
  const [modalIsOpenConfig, setModalIsOpenConfig] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [products, setProducts] = useState([]);
  const [formEdit, setFormEdit] = useState({});
  const [formContact, setFormContact] = useState({});

  function handleModalDelete() {
    setIsOpenDelete(!modalIsOpenDelete);
  }

  function handleModalEdit() {
    setIsOpenEdit(!modalIsOpenEdit);
  }

  const handleShowModal = () => {
    setShowModal(!showModal);
  };

  useEffect(() => {
    getProducts();
    const dataContact = JSON.parse(localStorage.getItem("dataContact"));
    setFormContact(dataContact);
  }, []);

  const getProducts = () => {
    API.get("/Produtos").then((response) => {
      console.log(response.data.data);
      setProducts(response.data.data);
    });
  };

  const statusEmail = (product) => {
    if (product.branchmarking != null) {
      if (product.branchmarking.statusEmail == 1) {
        return 1;
      }
      return 0;
    } else {
      return 0;
    }
  };

  const processBenchmarking = (product) => {
    if (product.branchmarking == null) {
      API.get("/Crawler/benchmarking/" + product.id)
        .then((response) => {
          console.log(response);
          getProducts();
          toast.success(
            `Sucesso ao fazer benchmarking do produto ${product.descricao}`
          );
        })
        .then((error) => {
          console.log(error);
          toast.success(
            `Error ao fazer benchmarking do produto ${product.descricao}`
          );
        });
    } else {
      return;
    }
  };

  const sendEmail = (product) => {
    const dataContact = JSON.parse(localStorage.getItem("dataContact"));
    console.log(dataContact);
    if (statusEmail(product) == 0) {
      API.get(
        `/Crawler/enviar-email/${product.id}?userEmail=${dataContact.email}&userWhatsapp=${dataContact.whatsapp}`
      )
        .then((response) => {
          console.log(response);
          toast.success(
            `Sucesso ao enviar relatório para produto ${product.descricao}`
          );
          getProducts();
        })
        .catch((error) => {
          toast.success(
            `Error ao enviar relatório para produto ${product.descricao}`
          );

          console.log(error);
        });
    }
  };

  const deleteProduto = (produto) => {
    API.delete("/Produtos/" + produto.id)
      .then(() => {
        getProducts();
        toast.success(`Produto ${produto.descricao} deletado`);
        handleModalDelete();
      })
      .catch((error) => {
        toast.error(`Error ao tentar deletar produto ${produto.descricao} `);

        console.log(error);
      });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormEdit((prevData) => ({ ...prevData, [name]: value }));
    console.log("Form Data:", formEdit);
  };

  const handleSubmitEdit = (produto) => {
    if (formEdit.estoqueAtual <= 0 || formEdit.estoqueMinimo <= 0) {
      alert("Preencha todos os campos obrigatórios");
      return;
    } else {
      API.put("/Produtos/" + produto.id, formEdit).then((response) => {
        console.log("Response:", response);
        getProducts();
      });
    }
  };

  const handleChangeConfig = (e) => {
    const { name, value } = e.target;
    setFormContact((prevData) => ({ ...prevData, [name]: value }));
    console.log("Form Data:", formContact);
  };

  const handleSubmitConfig = (produt) => {
    // if (formContact.email.trim() === "" || formContact.whatsapp.trim() === "") {
    //   toast.error("Preencha todos os campos");
    //   return;
    // }

    // if (
    //   // formContact.zap.length < 11 ||
    //   // validateEmail(formContact.email) != false
    // ) {
    //   toast.error("Campos inválidos");
    //   return;
    // }

    localStorage.setItem("dataContact", JSON.stringify(formContact));

    sendEmail(produt);
    handleEmailModal();
  };

  const handleEmailModal = () => {
    setModalIsOpenConfig(!modalIsOpenConfig);
  };

  return (
    <>
      <section className=" container mt-7">
        <div className="flex-space-between">
          <h4>Gestão de produtos</h4>

          <button onClick={handleShowModal} className="btnGreen">
            Novo Produto
          </button>
        </div>
        <table className="table-produtos mt-5">
          <thead>
            <tr>
              <th>Código</th>
              <th>Nome</th>
              <th>Preço</th>
              <th>Estoque Atual</th>
              <th>Estoque Mínimo</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {products &&
              products.map((product) => (
                <tr key={product.id}>
                  <td>{product.id}</td>
                  <td>{product.descricao}</td>
                  <td>R$ {product.preco}</td>
                  <td>{product.estoqueAtual}</td>
                  <td>{product.estoqueMinimo}</td>
                  <td className="icons">
                    <a>
                      <img
                        className="icon-m"
                        src={
                          product.branchmarking != null
                            ? lampBlueButton
                            : playButton
                        }
                        onClick={() => processBenchmarking(product)}
                      />
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={
                          statusEmail(product) == 1
                            ? emailBlockButton
                            : emailActiveButton
                        }
                        onClick={handleEmailModal}
                        alt=""
                      />{" "}
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={editButton}
                        onClick={handleModalEdit}
                        alt=""
                      />
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={closeButton}
                        onClick={handleModalDelete}
                        alt=""
                      />
                    </a>
                  </td>
                  <CustomModal
                    isOpen={modalIsOpenConfig}
                    onRequestClose={handleEmailModal}
                    title="Informe seus dados"
                    content={
                      <>
                        <p className="my-1 text-center">
                          Para receber o relatório preencha os dados abaixo
                        </p>
                        <div>
                          <label htmlFor="email">Email:</label>
                          <input
                            className="inputEmail"
                            value={formContact.email}
                            type="email"
                            name="email"
                            id="email"
                            placeholder="Digite seu email"
                            onChange={handleChangeConfig}
                          />
                        </div>

                        <div>
                          <label htmlFor="zap">Whatsapp:</label>
                          <input
                            type="text"
                            name="whatsapp"
                            id="zap"
                            value={formContact.whatsapp}
                            placeholder="Digite seu whatsapp"
                            onChange={handleChangeConfig}
                          />
                        </div>

                        <div className="flex-center mt-2">
                          <button
                            className="btnGreen mr-2"
                            onClick={() => handleSubmitConfig(product)}
                          >
                            Enviar
                          </button>
                          <button
                            className="btnRed "
                            onClick={handleEmailModal}
                          >
                            Cancelar
                          </button>
                        </div>
                      </>
                    }
                  />
                  <CustomModal
                    isOpen={modalIsOpenEdit}
                    onRequestClose={handleModalEdit}
                    title="Editar Produto"
                    content={
                      <>
                        <form>
                          <div className="flex-column">
                            <label htmlFor="estoqueAtual " className="mt-3">
                              Estoque Atual:
                            </label>
                            <input
                              className="inputModal"
                              type="estoqueAtual"
                              name="estoqueAtual"
                              id="estoqueAtual"
                              placeholder="Novo Estoque Atual"
                              onChange={handleChange}
                            />
                            <label htmlFor="estoqueAtual">
                              Estoque Mínimo:
                            </label>
                            <input
                              className="inputModal"
                              type="estoqueMinimo"
                              name="estoqueMinimo"
                              id="estoqueMinimo"
                              placeholder="Novo Estoque Mínimo"
                              onChange={handleChange}
                            />
                          </div>
                          <div className="flex-center">
                            <button
                              className="btnGreen mt-2 mr-2"
                              onClick={() => handleSubmitEdit(product)}
                            >
                              Editar
                            </button>
                            <button
                              className="btnRed mt-2"
                              onClick={handleModalEdit}
                            >
                              Cancelar
                            </button>
                          </div>
                        </form>
                      </>
                    }
                  />
                  <CustomModal
                    isOpen={modalIsOpenDelete}
                    onRequestClose={handleModalDelete}
                    title="Deseja realmente excluir?"
                    content={
                      <div className="flex-center">
                        <button
                          className="btnGreen mt-2 mr-2"
                          onClick={() => deleteProduto(product)}
                        >
                          Sim
                        </button>
                        <button
                          className="btnRed mt-2"
                          onClick={handleModalDelete}
                        >
                          Não
                        </button>
                      </div>
                    }
                  />
                </tr>
              ))}
            <tr>
              <td>10</td>
              <td>Resma A4</td>
              <td>0</td>
              <td>200</td>
              <td>100</td>
              <td className="icons">
                <a>
                  <img className="icon-m" src={lampBlueButton} alt="" />
                </a>
                <a>
                  <img className="icon-m" src={emailActiveButton} alt="" />
                </a>
                <a>
                  <img
                    className="icon-m"
                    src={editButton}
                    onClick={handleModalEdit}
                    alt=""
                  />
                  <CustomModal
                    isOpen={modalIsOpenEdit}
                    onRequestClose={handleModalEdit}
                    title="Editar Produto"
                    content={
                      <>
                        <form>
                          <div className="flex-column-center">
                            <input
                              className="inputModal mt-3"
                              type="estoqueAtual"
                              name="estoqueAtual"
                              id="estoqueAtual"
                              placeholder="Novo Estoque Atual"
                              onChange={handleChange}
                            />
                            <label htmlFor="estoqueAtual">
                              Estoque Mínimo:
                            </label>

                            <input
                              className="inputModal"
                              type="estoqueMinimo"
                              name="estoqueMinimo"
                              id="estoqueMinimo"
                              placeholder="Novo Estoque Mínimo"
                              onChange={handleChange}
                            />
                          </div>
                          <div className="flex-center">
                            <button className="btnGreen mt-2 mr-2">
                              Editar
                            </button>
                            <button
                              className="btnRed mt-2"
                              onClick={handleModalEdit}
                            >
                              Cancelar
                            </button>
                          </div>
                        </form>
                      </>
                    }
                  />
                </a>
                <a>
                  <img
                    className="icon-m"
                    src={closeButton}
                    onClick={handleModalDelete}
                    alt=""
                  />
                  <CustomModal
                    isOpen={modalIsOpenDelete}
                    onRequestClose={handleModalDelete}
                    title="Deseja realmente excluir?"
                    content={
                      <div className="flex-center">
                        <button className="btnGreen mt-2 mr-2">Sim</button>
                        <button
                          className="btnRed mt-2"
                          onClick={handleModalDelete}
                        >
                          Não
                        </button>
                      </div>
                    }
                  />
                </a>
              </td>
            </tr>
          </tbody>
        </table>
        <div className="flex-center"></div>
      </section>
      {showModal && (
        <CreateProductModal
          handleShowModal={handleShowModal}
          getProdutos={getProducts}
        />
      )}

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
