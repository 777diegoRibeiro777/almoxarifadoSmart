import "./_style.scss";
// import lampGreenIcon from "../../assets/icons/icons8-light-green.png";
// import lampRedIcon from "../../assets/icons/icons8-light-red.png";
import emailIcon from "../../assets/Icons/icons8-nova-mensagem-48.png";
import deleteIcon from "../../assets/Icons/icons8-remover-48.png";
import editIcon from "../../assets/Icons/icons8-editar-48.png";
import CreateProductModal from "../../components/CreateProductModal";
import { useEffect, useState } from "react";
import Modal from "react-modal";
import { API } from "../../api/API";
import playButton from "../../assets/play.svg"
import closeButton from "../../assets/close.svg"
import modalCloseButton from "../../assets/closeButtonCircle.svg"
import editButton from "../../assets/edit.svg"
import emailBlockButton from "../../assets/emailGrey.svg"
import lampBlueButton from "../../assets/lampBlue.svg"
import lampRedButton from "../../assets/lampRed.svg"
import emailActiveButton from "../../assets/emailActive.svg"

Modal.setAppElement("#root")


export default function Produtos() {
   const [modalIsOpenDelete, setIsOpenDelete] = useState(false);
   const [modalIsOpenEdit, setIsOpenEdit] = useState(false);  
   const [showModal, setShowModal] = useState(false);
   //const [products, setProducts] = useState([]);

   function openModalDelete() {
    setIsOpenDelete(true);
   }

   function closeModalDelete() {
    setIsOpenDelete(false);
   }

   function openModalEdit() {
    setIsOpenEdit(true);
   }

   function closeModalEdit() {
    setIsOpenEdit(false);
   }
  // useEffect(() => {
  //   API.get("/Produtos").then((response) => {
  //     setProducts(response.data.data);
  //   });
  // });

  const handleShowModal = () => {
    setShowModal(!showModal);
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
            {/* {products &&
              products.map((product) => (
                <tr key={product.id}>
                  <td>{product.id}</td>
                  <td>{product.descricao}</td>
                  <td>R$ {product.preco}</td>
                  <td>{product.estoqueAtual}</td>
                  <td>{product.estoqueMinimo}</td>
                  <td className="icons">
                    <a>
                      <img className="icon-s btn-play" src={playIcon} alt="" />
                    </a>
                    <a>
                      <img className="icon-s" src={emailIcon} alt="" />
                    </a>
                    <a>
                      <img className="icon-s" src={editIcon} alt="" />
                    </a>
                    <a>
                      <img className="icon-s" src={deleteIcon} alt="" />
                    </a>
                  </td>
                </tr>
              ))} */}
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
                      <img className="icon-m" src={editButton} onClick={openModalEdit} alt="" />
                      <Modal 
                        isOpen={modalIsOpenEdit}
                        onRequestClose={closeModalEdit}
                        contentLabel="Exemple Modal"
                        overlayClassName="modal-overlay"
                        className="modal-content" 
                      >
                        {/* <a>
                          <img src={modalCloseButton} alt="botão fechar" onClick={closeModal} className="icon-m closeModalBtn" />
                        </a> */}
                        <h4 className="text-center textModal">edit</h4>
                        <hr />

                      </Modal>
                    </a>
                    <a>
                      <img className="icon-m" src={closeButton} onClick={openModalDelete}  alt="" />
                      <Modal 
                        isOpen={modalIsOpenDelete}
                        onRequestClose={closeModalDelete}
                        contentLabel="Exemple Modal"
                        overlayClassName="modal-overlay"
                        className="modal-content" 
                      >
                        {/* <a>
                          <img src={modalCloseButton} alt="botão fechar" onClick={closeModal} className="icon-m closeModalBtn" />
                        </a> */}
                        <h4 className="text-center textModal">Deseja realmente excluir</h4>
                        <hr />
                        <div className="flex-center">
                          <button className="btnGreen mt-2 mr-2 ">Sim</button>
                          <button className="btnRed mt-2 " onClick={closeModalDelete}>Não</button>
                        </div>
                      </Modal>
                    </a>
                  </td>
              </tr>
              <tr>
                <td>20</td>
                <td>Notebook HP</td>
                <td>0</td>
                <td>200</td>
                <td>100</td>
                <td className="icons">

                    <a>
                      <img className="icon-m" src={playButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={emailBlockButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={editButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={closeButton} alt="" />
                    </a>
                  </td>
              </tr>
              <tr>
                <td>30</td>
                <td>TV Samsung 42</td>
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
                      <img className="icon-m" src={editButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={closeButton} alt="" />
                    </a>
                  </td>
              </tr>
              <tr>
                <td>40</td>
                <td>Resma A4</td>
                <td>0</td>
                <td>200</td>
                <td>100</td>
                <td className="icons">

                    <a>
                      <img className="icon-m" src={lampRedButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={emailBlockButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={editButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={closeButton} alt="" />
                    </a>
                  </td>
              </tr>
              <tr>
                <td>50</td>
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
                      <img className="icon-m" src={editButton} alt="" />
                    </a>
                    <a>
                      <img className="icon-m" src={closeButton} alt="" />
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
