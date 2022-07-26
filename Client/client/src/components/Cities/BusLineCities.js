import BusLineCitiesList from "./BusLineCitiesList";
import React, { useState, useContext } from "react";
import AuthContext from "../../store/auth-context";
import useHttp from "../../hooks/useHttp";
import ConflictModal from "../UI/Modals/ConflictModal";

function BusLineCities(props) {
  const ctx = useContext(AuthContext);
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [conflictData, setConflictData] = useState(null);

  async function removeCityHandler(removeData) {
    const requestConfig = {
      url: `https://localhost:44386/api/buslines/${props.id}/remove-city`,
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        cityId: removeData.cityId,
        timestamp: props.timestamp,
        override: removeData.override === true,
      }),
    };
    const data = await sendRequest(requestConfig);
    if (data.hasConflict) {
      setConflictData({
        data: { ...removeData, override: true },
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
    props.onSuccess();
  }

  async function confirmConflictHandler() {
    await removeCityHandler(conflictData.data);
  }

  function closeConflictHandler() {
    setConflictData(null);
  }

  return (
    <React.Fragment>
      <ConflictModal
        isLoading={isLoading}
        infoData={infoData}
        conflictData={conflictData}
        hideErrorModalHandler={hideErrorModalHandler}
        hideSuccessModalHandler={hideSuccessModalHandler}
        confirmConflictHandler={confirmConflictHandler}
        closeConflictHandler={closeConflictHandler}
      />
      <BusLineCitiesList items={props.items} onRemove={removeCityHandler} />
    </React.Fragment>
  );
}

export default BusLineCities;
