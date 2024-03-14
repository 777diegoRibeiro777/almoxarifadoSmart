import { Link } from "react-router-dom";
import "./_style.scss";
import "react";
import { useAuthContext } from "../../context/AuthContext";

export default function Header() {
  const { logoutAuthContext } = useAuthContext();

  return (
    <>
      <header className="flex-space-between">
        <nav>
          <ul className="flex-space-between g-2">
            <li>
              <Link className="nav-link" to={"/"}>
                Produtos
              </Link>
            </li>
            <li>
              <Link className="nav-link" to={"/requisicao"}>
                Requisição
              </Link>
            </li>
            <li>
              <Link className="nav-link" to={"/beanchmarking"}>
                Beachmarking Log
              </Link>
            </li>
            <li>
              <Link className="nav-link" to={"/configuracao"}>
                Configurações
              </Link>
            </li>
          </ul>
        </nav>
        <button onClick={logoutAuthContext} className="btnSair btnRed">
          Sair
        </button>
      </header>
    </>
  );
}
