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
// import lampRedButton from "../../assets/lampRed.svg";
import emailActiveButton from "../../assets/emailActive.svg";
import CustomModal from "../../components/CustomModal";
import { ToastContainer, toast } from "react-toastify";
import spinner from "../../assets/spinner1.svg";
import { validateEmail } from "../../Utils/functions";

Modal.setAppElement("#root");

export default function Produtos() {
  const [modalIsOpenDelete, setIsOpenDelete] = useState(false);
  const [modalIsOpenEdit, setIsOpenEdit] = useState(false);
  const [modalIsOpenConfig, setModalIsOpenConfig] = useState(false);
  const [modalIsOpenBenchmarking, setModalIsOpenBenchmarking] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [products, setProducts] = useState([]);
  const [formEdit, setFormEdit] = useState({});
  const [formContact, setFormContact] = useState({});
  const [processando, setProcessando] = useState(false);
  const [BenchmarkingInProgress, setBenchmarkingInProgress] = useState(false);
  const [benchmarkingProductId, setBenchmarkingProductId] = useState(null);
  const [selectedProductForEdit, setSelectedProductForEdit] = useState(null);
  const [selectedProductForEmailModal, setSelectedProductForEmailModal] =
    useState(null);
  const [selectedProductForDelete, setSelectedProductForDelete] =
    useState(null);

  function handleModalDelete(produto) {
    console.log(produto);
    setIsOpenDelete(!modalIsOpenDelete);
    setSelectedProductForDelete(produto);
  }

  function handleModalEdit(produto) {
    console.log(produto);

    setIsOpenEdit(!modalIsOpenEdit);
    setSelectedProductForEdit(produto);
  }

  const handleEmailModal = (produto) => {
    console.log(produto);

    setModalIsOpenConfig(!modalIsOpenConfig);
    setSelectedProductForEmailModal(produto);
  };

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
      API.put("/Produtos/" + produto.id, formEdit)
        .then(() => {
          toast.success(`Sucesso ao editar produto ${produto.descricao}`);
          handleModalEdit();
          getProducts();
        })
        .catch((error) => {
          console.log(error);
          toast.error(`Error ao editar produto ${produto.descricao}`);
        });
    }
  };

  const processBenchmarking = (product) => {
    console.log(processando);
    if (processando) return;

    getProducts();

    if (product.branchmarking == null) {
      setProcessando(true);
      setBenchmarkingInProgress(true);
      setBenchmarkingProductId(product.id);
      API.get("/Crawler/benchmarking/" + product.id)
        .then((response) => {
          if (response.status == 200) {
            getProducts();
            toast.success(
              `Sucesso ao fazer benchmarking do produto ${product.descricao}`
            );
          } else {
            toast.success(
              `Error ao fazer benchmarking do produto ${product.descricao}`
            );
          }
        })
        .then((error) => {
          console.log(error);
        })
        .finally(() => {
          setBenchmarkingInProgress(false);
          setProcessando(false);
        });
    } else {
      handleModalBenchmarking();
      return;
    }
  };

  const sendEmail = (product) => {
    console.log(processando + "processando");
    if (processando) return;
    const dataContact = JSON.parse(localStorage.getItem("dataContact"));
    console.log(product.id);

    if (statusEmail(product) == 0 && product.branchmarking != null) {
      setProcessando(true);

      console.log("opa");
      API.get(
        `/Crawler/enviar-email/${product.id}?userEmail=${
          dataContact.email
        }&userWhatsapp=${
          dataContact.whatsapp == "79988353265" ? "null" : dataContact.whatsapp
        }`
      )
        .then((response) => {
          console.log(response);

          toast.success(
            `Sucesso ao enviar relatório do produto ${product.descricao}`
          );
          getProducts();
        })
        .catch(() => {
          toast.error(
            `Error ao enviar relatório do produto ${product.descricao}`
          );
        })
        .finally(() => {
          console.log("passou no finally");
          setProcessando(false);
        });
    } else {
      toast.warning(
        `Confira se foi feito benchmarking para o produto ${product.descricao}`
      );
      setProcessando(false);
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

  const handleChangeConfig = (e) => {
    const { name, value } = e.target;
    setFormContact((prevData) => ({ ...prevData, [name]: value }));
    console.log("Form Data:", formContact);
  };

  const handleSubmitConfig = (product) => {
    if (formContact.email.trim() === "" || formContact.whatsapp.trim() === "") {
      toast.error("Preencha todos os campos");
      return;
    }

    if (
      formContact.whatsapp.length < 11 &&
      validateEmail(formContact.email) != false
    ) {
      toast.error("Campos inválidos");
      return;
    }

    console.log("aqio");

    localStorage.setItem("dataContact", JSON.stringify(formContact));

    console.log(product.id);
    sendEmail(product);

    handleEmailModal();
  };

  const handleModalBenchmarking = () => {
    setModalIsOpenBenchmarking(!modalIsOpenBenchmarking);
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
              <th className="preco-response">Preço </th>
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
                          BenchmarkingInProgress &&
                          benchmarkingProductId === product.id
                            ? spinner
                            : product.branchmarking != null
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
                        onClick={
                          statusEmail(product) == 0
                            ? () => handleEmailModal(product)
                            : null
                        }
                        alt=""
                      />{" "}
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={editButton}
                        onClick={() => handleModalEdit(product)}
                        alt=""
                      />
                    </a>
                    <a>
                      <img
                        className="icon-m"
                        src={closeButton}
                        onClick={() => handleModalDelete(product)}
                        alt=""
                      />
                    </a>
                  </td>

                  {product.branchmarking != null && (
                    <CustomModal
                      isOpen={modalIsOpenBenchmarking}
                      onRequestClose={handleModalBenchmarking}
                      title={`Relatório do produto ${product.descricao}`}
                      content={
                        <>
                          <h6 className="mt-2">Loja:</h6>
                          <span className="gray-1">
                            {product.branchmarking.loja == 1
                              ? "Magazine Luiza"
                              : "Mercado Livre"}
                          </span>
                          <h6 className=" mt-1">Economia:</h6>

                          <span className="green-normal">
                            R${product.branchmarking.economia}
                          </span>

                          <h6 className="mt-1">Status Email:</h6>
                          <span className="gray-1">
                            {product.branchmarking.statusEmail == 1
                              ? "Enviado"
                              : "Não enviado"}
                          </span>

                          <h6 className="mt-1">Link:</h6>
                          <a
                            className="link-produto"
                            target="_blank"
                            href={product.branchmarking.link}
                          >
                            Produto
                          </a>
                        </>
                      }
                    />
                  )}
                </tr>
              ))}
          </tbody>
        </table>
        <div className="flex-center"></div>
      </section>

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
                className="inputEmail"
                id="zap"
                value={formContact.whatsapp}
                placeholder="Digite seu whatsapp"
                onChange={handleChangeConfig}
              />
            </div>

            <div className="flex-center mt-2">
              <button
                className="btnGreen mr-2"
                onClick={() => handleSubmitConfig(selectedProductForEmailModal)}
              >
                Enviar
              </button>
              <button className="btnRed " onClick={() => handleEmailModal()}>
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
              <label htmlFor="estoqueAtual">Estoque Mínimo:</label>
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
                onClick={() => handleSubmitEdit(selectedProductForEdit)}
              >
                Editar
              </button>
              <button className="btnRed mt-2" onClick={() => handleModalEdit()}>
                Cancelar
              </button>
            </div>
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
              onClick={() => deleteProduto(selectedProductForDelete)}
            >
              Sim
            </button>
            <button className="btnRed mt-2" onClick={() => handleModalDelete()}>
              Não
            </button>
          </div>
        }
      />
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
        theme="light"
      />
    </>
  );
}
