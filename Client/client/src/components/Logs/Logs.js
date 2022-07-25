import LogsList from "./LogsList";
import React, { useContext, useEffect, useState } from "react";
import InfoModal from "../UI/Modals/InfoModal";
import useHttp from "../../hooks/useHttp";
import LoadingModal from "../UI/Modals/LoadingModal";
import AuthContext from "../../store/auth-context";
import { useHistory } from "react-router-dom";
import classes from './Logs.module.css'

function Logs() {
  const history = useHistory();
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [logs, setLogs] = useState(null);
  const [errorData, setErrorData] = useState(null);

  useEffect(() => {
    async function getLogs() {
      const requestConfig = {
        url: "https://localhost:44386/api/users/logs",
        headers: {
          Authorization: "Bearer " + token,
        },
      };
      const data = await sendRequest(requestConfig);
      if (data.hasError) {
        setErrorData({
          title: "Error",
          message: data.message,
        });
        return;
      }
      setLogs(data);
    }
    getLogs();
  }, [token, sendRequest]);

  function hideModalHandler() {
    setErrorData(null);
    history.replace("/");
  }

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {errorData && (
        <InfoModal
          title={errorData.title}
          message={errorData.message}
          onConfirm={hideModalHandler}
        />
      )}
      {logs !== null && <LogsList items={logs} />}
      {(logs !== null && logs.length === 0) && (
        <section className={classes['no-logs']}>
          <h1>No Logs!</h1>
        </section>
      )}
    </React.Fragment>
  );
}

export default Logs;
