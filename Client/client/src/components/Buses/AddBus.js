import AddBusForm from "./AddBusForm";
import ConflictModal from "../UI/Modals/ConflictModal";
import React, { useState, useEffect, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";
import AuthContext from "../../store/auth-context";
import useHttp from "../../hooks/useHttp";

function AddBus() {
  const history = useHistory();
  const params = useParams();
  const { busLineId } = params;
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [conflictData, setConflictData] = useState(null);
  const [manufacturers, setManufacturers] = useState(null);
  const [busLine, setBusLine] = useState(null);

  useEffect(() => {
    async function getManufacturers() {
      const requestConfig = {
        url: `https://localhost:44386/api/manufacturers/`,
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
      setManufacturers(data);
    }

    getManufacturers();
  }, [token, sendRequest]);

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

  async function addBusHandler(addData) {
    const manufacturerId = manufacturers.reduce(
      (previousValue, currentValue) =>
        currentValue.name === addData.manufacturer
          ? currentValue.id
          : previousValue,
      0
    );
    const requestConfig = {
        url: "https://localhost:44386/api/buses",
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + ctx.token,
        },
        body: JSON.stringify({
          name: addData.name,
          label: addData.label,
          busLineId: busLine.id,
          manufacturerId: manufacturerId,
          timestamp: busLine.timestamp,
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
        message: data.hasError ? data.message : "Bus added successfully",
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
    await addBusHandler(conflictData.data);
  }

  function closeConflictHandler() {
    setConflictData(null);
    history.replace(`/bus-lines/${busLine.id}`);
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
      {manufacturers !== null && busLine !== null && (
        <AddBusForm
          title="New Bus"
          items={manufacturers}
          onSubmit={addBusHandler}
        />
      )}
    </React.Fragment>
  );
}

export default AddBus;
