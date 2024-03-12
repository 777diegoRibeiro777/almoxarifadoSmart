import { useState } from "react";
import "./_style.scss";
import { ToastContainer, toast } from "react-toastify";
import { useAuthContext } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";

export default function Login() {
  const [form, setForm] = useState();
  const { loginAuthContext } = useAuthContext();
  const navigate = useNavigate();

  async function handleLogin(e) {
    e.preventDefault();

    if (form.user === "admin" && form.password === "admin") {
      loginAuthContext({
        user: form.user,
        password: form.password,
      });
      navigate("/produtos");
    } else {
      toast.error("Usuário ou senha inválidos");
    }
  }

  function onChange(e) {
    const { value, name } = e.target;
    setForm({ ...form, [name]: value });
  }

  console.log(form);

  return (
    <>
      <section className="container mt-6">
        <div className="row">
          <div className="grid-2"></div>
          <div
            className="grid-8
              container-form-login
            "
          >
            <div className="flex-center"></div>
            <h4 className="text-center">Olá, faça o login para continuar.</h4>
            <form
              onSubmit={(e) => handleLogin(e)}
              className="mt-6 form-login flex-column-center"
            >
              <input
                type="text"
                name="user"
                placeholder="Digite seu usuário"
                id="user"
                className="search w-100"
                autoComplete="off"
                onChange={onChange}
              />
              <input
                type="password"
                name="password"
                placeholder="Digite sua senha"
                className="search mt-2 w-100"
                id="password"
                autoComplete="off"
                onChange={onChange}
              />

              <button type="submit" className="btn b-0 btn-login w-100 mt-3">
                Entrar
              </button>
            </form>
          </div>
        </div>
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
      </section>
    </>
  );
}
