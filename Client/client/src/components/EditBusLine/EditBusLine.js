import EditBusLineForm from "./EditBusLineForm";
import React, { useEffect, useState, useContext } from "react";
import useHttp from "../../hooks/useHttp";
import LoadingModal from "../UI/Modals/LoadingModal";
import InfoModal from "../UI/Modals/InfoModal";
import AuthContext from "../../store/auth-context";
import { useParams, useHistory } from "react-router-dom";

function EditBusLine() {
  const history = useHistory();
  const params = useParams();
  const { busLineId } = params;
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [conflictData, setConflictData] = useState(null);
  const [busLine, setBusLine] = useState(null);

  useEffect(() => {
    async function getBusLine() {
      const requestConfig = {
        url: `https://localhost:44386/api/buslines/${busLineId}`,
        headers: {
          Authorization: "Bearer " + token,
        },
      };
      const data = await sendRequest(requestConfig);
      if (data.hasError) {
        setInfoData({
          title: "Error",
          message: data.message,
        });
        return;
      }
      setBusLine(data);
    }

    getBusLine();
  }, [busLineId, token, sendRequest]);

  async function onEditBusLine(editData) {
    const requestConfig = {
      url: "https://localhost:44386/api/buslines",
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        id: busLine.id,
        label: editData.label,
        busLineType: editData.busLineType,
        timestamp: busLine.timestamp,
        override: editData.override === true,
      }),
    };
    const data = await sendRequest(requestConfig);
    if (data.hasConflict) {
      setConflictData({
        data: { ...editData, override: true },
        title: "Conflict",
        message: "There was conflict while editing bus line. Override changes?",
      });
      return;
    }
    setConflictData(null);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "Bus line edited successfully",
    });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  function hideSuccessModalHandler() {
    setInfoData(null);
    history.replace(`/bus-lines/${busLine.id}`);
  }

  async function confirmConflictHandler() {
    await onEditBusLine(conflictData.data);
  }

  function closeConflictHandler() {
    setConflictData(null);
    history.replace(`/bus-lines/${busLine.id}`);
  }

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {infoData !== null && conflictData === null && (
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
      {infoData === null && conflictData !== null && (
        <InfoModal
          title={conflictData.title}
          message={conflictData.message}
          onConfirm={confirmConflictHandler}
          onClose={closeConflictHandler}
        />
      )}
      {busLine !== null && <EditBusLineForm onSubmit={onEditBusLine} />}
    </React.Fragment>
  );
}

export default EditBusLine;
