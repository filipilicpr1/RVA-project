import BusLine from "./BusLine";
import React, { useState, useEffect, useContext, useCallback } from "react";
import AuthContext from "../../store/auth-context";
import useHttp from "../../hooks/useHttp";
import { useHistory, useParams } from "react-router-dom";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import BusLineCities from "../Cities/BusLineCities";

let isInitial = true;

function DetailedBusLine() {
  const history = useHistory();
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [busLine, setBusLine] = useState(null);
  const [errorData, setErrorData] = useState(null);
  const [getToggle, setGetToggle] = useState(false);
  const params = useParams();
  const { busLineId } = params;

  useEffect(() => {
    isInitial = true;
  }, []);

  const getBusLine = useCallback(async () => {
    const requestConfig = {
      url: `https://localhost:44386/api/buslines/${busLineId}`,
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
    setBusLine(data);
    isInitial = false;
  }, [sendRequest, busLineId, token]);


  useEffect(() => {
    // async function getBusLine() {
    //   const requestConfig = {
    //     url: `https://localhost:44386/api/buslines/${busLineId}`,
    //     headers: {
    //       Authorization: "Bearer " + token,
    //     },
    //   };
    //   const data = await sendRequest(requestConfig);
    //   if (data.hasError) {
    //     setErrorData({
    //       title: "Error",
    //       message: data.message,
    //     });
    //     return;
    //   }
    //   setBusLine(data);
    //   isInitial = false;
    // }
    if (isInitial) {
      getBusLine();
    }
    const timer = setTimeout(async () => {
      await getBusLine();
      setGetToggle((prevState) => !prevState);
    }, 10000);

    return () => {
      clearTimeout(timer);
    };
  }, [getToggle, getBusLine]);

  function hideModalHandler() {
    setErrorData(null);
    history.replace("/bus-lines");
  }

  async function successHandler() {
    isInitial = true;
    await getBusLine();
  }

  return (
    <React.Fragment>
      {isLoading && isInitial && <LoadingModal />}
      {errorData && (
        <InfoModal
          title={errorData.title}
          message={errorData.message}
          onConfirm={hideModalHandler}
        />
      )}
      {busLine !== null && (
        <BusLine
          label={busLine.label}
          busLineType={busLine.busLineType}
          id={busLine.id}
        />
      )}
      {busLine !== null && (
        <BusLineCities
          items={busLine.cities}
          id={busLine.id}
          timestamp={busLine.timestamp}
          onSuccess={successHandler}
        />
      )}
    </React.Fragment>
  );
}

export default DetailedBusLine;
