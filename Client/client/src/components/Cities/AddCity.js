import React, { useState, useEffect, useContext } from "react";
import useHttp from "../../hooks/useHttp";
import AuthContext from "../../store/auth-context";
import { useHistory, useParams } from "react-router-dom";
import ConflictModal from "../UI/Modals/ConflictModal";
import BusLineCitiesList from "./BusLineCitiesList";

function AddCity() {
  const history = useHistory();
  const params = useParams();
  const { busLineId } = params;
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [conflictData, setConflictData] = useState(null);
  const [availableCities, setAvailableCities] = useState(null);

  useEffect(() => {
    async function getAvailableCities() {
      const requestConfig = {
        url: `https://localhost:44386/api/cities/available/${busLineId}`,
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
      setAvailableCities(data);
    }

    getAvailableCities();
  }, [busLineId, sendRequest, token]);

  async function addCityHandler(addData) {
    const requestConfig = {
        url: `https://localhost:44386/api/buslines/${busLineId}/add-city`,
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + ctx.token,
        },
        body: JSON.stringify({
          cityId: addData.cityId,
          timestamp: availableCities.timestamp,
          override: addData.override === true,
        }),
      };
      const data = await sendRequest(requestConfig);
      if (data.hasConflict) {
        setConflictData({
          data: { ...addData, override: true },
          title: "Conflict",
          message: "There was conflict while editing bus line. Override changes?",
        });
        return;
      }
      setConflictData(null);
      setInfoData({
        title: data.hasError ? "Error" : "Success",
        message: data.hasError ? data.message : "City added successfully",
      });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  function hideSuccessModalHandler() {
    setInfoData(null);
    history.replace(`/bus-lines/${busLineId}`);
  }

  async function confirmConflictHandler() {
    await addCityHandler(conflictData.data);
  }

  function closeConflictHandler() {
    setConflictData(null);
    history.replace(`/bus-lines/${busLineId}`);
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
      {availableCities !== null && <BusLineCitiesList items={availableCities.cities} text="Add" onClick={addCityHandler} />}
    </React.Fragment>
  );
}

export default AddCity;
