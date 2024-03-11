import { Link } from "react-router-dom";
import "./_style.scss";
import "react";

export default function Header() {
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
                Beachmarking Logo
              </Link>
            </li>
            <li>
              <Link className="nav-link" to={"/configuracao"}>
                Configurações
              </Link>
            </li>
          </ul>
        </nav>
        <button className="btnSair btnRed">
        <Link to={"/login"} className="linkSair">
          Sair
        </Link>
        </button>
      </header>
    </>
  );
}
