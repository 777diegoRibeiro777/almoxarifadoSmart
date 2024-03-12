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
// import modalCloseButton from "../../assets/closeButtonCircle.svg";
import editButton from "../../assets/edit.svg";
import emailBlockButton from "../../assets/emailGrey.svg";
import lampBlueButton from "../../assets/lampBlue.svg";
// import lampRedButton from "../../assets/lampRed.svg";
import emailActiveButton from "../../assets/emailActive.svg";
import CustomModal from "../../components/CustomModal";

Modal.setAppElement("#root");

export default function Produtos() {
  const [modalIsOpenDelete, setIsOpenDelete] = useState(false);
  const [modalIsOpenEdit, setIsOpenEdit] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [products, setProducts] = useState([]);

  function handleModalDelete() {
    setIsOpenDelete(!modalIsOpenDelete);
  }

  function handleModalEdit() {
    setIsOpenEdit(!modalIsOpenEdit);
  }

  useEffect(() => {
    getProducts();
  }, []);

  const handleShowModal = () => {
    setShowModal(!showModal);
  };

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
        })
        .then((error) => {
          console.log(error);
        });
    } else {
      return;
    }
  };

  const sendEmail = (product) => {
    if (statusEmail(product) == 0) {
      API.get(
        `/Crawler/enviar-email/${product.id}?userEmail=lerocha644@gmail.com&userWhatsapp=null`
      )
        .then((response) => {
          console.log(response);
          getProducts();
        })
        .catch((error) => {
          console.log(error);
        });
    }
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
                      <a>
                        <img
                          className="icon-m"
                          src={
                            product.branchmarking != null
                              ? lampBlueButton
                              : playButton
                          }
                          onClick={() => processBenchmarking(product)}
                          alt=""
                        />
                      </a>{" "}
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={
                          statusEmail(product) == 1
                            ? emailBlockButton
                            : emailActiveButton
                        }
                        onClick={() => sendEmail(product)}
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
                      <CustomModal
                        isOpen={modalIsOpenEdit}
                        onRequestClose={handleModalEdit}
                        title="Editar Produto"
                        content={<p>Conteúdo do modal de edição</p>}
                      />{" "}
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
                      />{" "}
                    </a>
                  </td>
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
                    content={<p>Conteúdo do modal de edição</p>}
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
      {showModal && <CreateProductModal handleShowModal={handleShowModal} />}
    </>
  );
}
