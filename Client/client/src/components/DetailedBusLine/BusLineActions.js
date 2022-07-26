import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import { Link } from "react-router-dom";
import classes from "./BusLineActions.module.css";
import React, { useState, useContext } from "react";
import AuthContext from "../../store/auth-context";
import ConflictModal from "../UI/Modals/ConflictModal";
import useHttp from "../../hooks/useHttp";

function BusLineActions(props) {
  const ctx = useContext(AuthContext);
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [conflictData, setConflictData] = useState(null);

  async function deleteHandler() {
    const requestConfig = {
      url: `https://localhost:44386/api/buslines/${props.id}`,
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        cityId: props.id,
        timestamp: props.timestamp,
        override: conflictData !== null,
      }),
    };
    const data = await sendRequest(requestConfig);
    if (data.hasConflict) {
      setConflictData({
        data: { override: true },
        title: "Conflict",
        message: "There was conflict while editing bus line. Override changes?",
      });
      return;
    }
    setConflictData(null);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "Bus line deleted successfully",
    });
  }

  async function duplicateHandler() {
    const requestConfig = {
      url: `https://localhost:44386/api/buslines/${props.id}/duplicate`,
      method: "POST",
      headers: {
        Authorization: "Bearer " + ctx.token,
      },
    };
    const data = await sendRequest(requestConfig);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "Bus line duplicated successfully",
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
    await deleteHandler();
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
      <Card className={classes.item}>
        <Link to={`/bus-lines/${props.id}/add-city`} className={classes.button}>
          Add City
        </Link>
        <Link to={`/bus-lines/${props.id}/add-bus`} className={classes.button}>
          Add Bus
        </Link>
        <Button className={classes.button} onClick={duplicateHandler}>Duplicate</Button>
        <Button className={classes.button} onClick={deleteHandler}>
          Delete
        </Button>
      </Card>
    </React.Fragment>
  );
}

export default BusLineActions;
