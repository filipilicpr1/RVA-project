import React, { useState, useEffect, useContext, useCallback } from "react";
import useHttp from "../../hooks/useHttp";
import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import BusLineCitiesList from "./BusLineCitiesList";

function DeleteCity() {
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [infoData, setInfoData] = useState(null);
  const [cities, setCities] = useState(null);

  const getAllCities = useCallback(async () => {
    const requestConfig = {
        url: `https://localhost:44386/api/cities`,
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
      setCities(data);
  }, [sendRequest, token]);

  useEffect(() => {
    getAllCities();
  }, [getAllCities]);

  async function deleteCityHandler(deleteData) {
    const requestConfig = {
        url: `https://localhost:44386/api/cities/${deleteData.cityId}`,
        method: "DELETE",
        headers: {
          Authorization: "Bearer " + ctx.token,
        },
      };
      const data = await sendRequest(requestConfig);
      setInfoData({
        title: data.hasError ? "Error" : "Success",
        message: data.hasError ? data.message : "City successfully deleted",
      });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  async function hideSuccessModalHandler() {
    setInfoData(null);
    await getAllCities();
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
      {cities !== null && (
        <BusLineCitiesList
          items={cities}
          text="Delete"
          onClick={deleteCityHandler}
        />
      )}
    </React.Fragment>
  );
}

export default DeleteCity;
