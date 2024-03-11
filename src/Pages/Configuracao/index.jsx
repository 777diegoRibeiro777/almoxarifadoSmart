import "./_style.scss";
export default function Configuracao() {
    return (
      <>
        <section className=" container">
          <h4 className="titleConfig text-center">Configurações</h4>
          <div className="cardProduto">
            <label htmlFor="email">Email:</label>
            <input className="inputEmail" type="email" name="email" id="email" placeholder="Digite seu email"/>

            <label htmlFor="zap">Whatsapp:</label>
            <input type="text" name="zap" id="zap" placeholder="Digite seu whatsapp"/>

            <button className="btnGreen salvarConfig">
              Salvar
            </button>
          </div>
        </section>

      </>
    );
  }