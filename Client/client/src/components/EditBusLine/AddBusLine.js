import React, { useState, useContext } from "react";
import EditBusLineForm from "./EditBusLineForm";
import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import useHttp from "../../hooks/useHttp";
import {useHistory} from 'react-router-dom';

function AddBusLine() {
  const ctx = useContext(AuthContext);
  const {isLoading, sendRequest} = useHttp();
  const [infoData, setInfoData] = useState(null);
  const history = useHistory();

  async function addBusLineHandler(addData) {
    const requestConfig = {
      url: "https://localhost:44386/api/buslines",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        label: addData.label,
        busLineType: addData.busLineType,
      }),
    };
    const data = await sendRequest(requestConfig);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "Bus line successfully added",
    });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  function hideSuccessModalHandler() {
    setInfoData(null);
    history.replace("/bus-lines");
  }

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {infoData && (
        <InfoModal
          title={infoData.title}
          message={infoData.message}
          onConfirm={
            infoData.title === "Error"
              ? hideErrorModalHandler
              : hideSuccessModalHandler
          }
        />
      )}
      <EditBusLineForm onSubmit={addBusLineHandler} title="New Bus Line" />
    </React.Fragment>
  );
}

export default AddBusLine;
