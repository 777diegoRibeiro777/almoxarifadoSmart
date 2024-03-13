import { useEffect, useState } from "react";
import "./_style.scss";
import { API } from "../../api/API";
import { Pagination } from "antd";

export default function Beanchmarking() {
  const [logs, setLogs] = useState([]);
  const [numPagina, setNumPagina] = useState(1);

  useEffect(() => {
    API.get(`Log?page=${numPagina}&pageSize=10`).then((response) => {
      setLogs(response.data);
    });
  }, [numPagina]);

  const onchange = (page) => {
    setNumPagina(page);
  };

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
            {logs &&
              logs.map((log) => {
                return (
                  <tr key={log.iDlOG}>
                    <td>{log.iDlOG}</td>
                    <td>{log.usuarioRobo}</td>
                    <td>{log.etapa}</td>
                    <td>{log.informacaoLog}</td>
                    <td>
                      {log.dateLog
                        .split("T")[0]
                        .replace("-", "/")
                        .replace("-", "/")
                        .replace("-", "/")}
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </table>
        <Pagination defaultCurrent={numPagina} total={50} onChange={onchange} />
      </section>
    </>
  );
}
