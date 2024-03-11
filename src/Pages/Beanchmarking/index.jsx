import "./_style.scss";

export default function Beanchmarking() {
  return (
    <>
      <section className="container mt-6">
        <h4 className="text-center">Beanchmarking</h4>

        <table className="table-produtos mt-7 rounded">
          <thead>
            <tr>
              <th>Código</th>
              <th>Nome</th>
              <th>Etapa</th>
              <th>Status</th>
              <th>Data</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td> 1</td>
              <td>Produto 1</td>
              <td>Relatório - Envio via Email</td>
              <td>Sucesso</td>
              <td>26/05/2000|20:00</td>
            </tr>
            <tr>
              <td> 1</td>
              <td>Produto 1</td>
              <td>Relatório - Envio via Email</td>
              <td>Sucesso</td>
              <td>26/05/2000|20:00</td>
            </tr>
            <tr>
              <td> 1</td>
              <td>Produto 1</td>
              <td>Relatório - Envio via Email</td>
              <td>Sucesso</td>
              <td>26/05/2000|20:00</td>
            </tr>
          </tbody>
        </table>
      </section>
    </>
  );
}
