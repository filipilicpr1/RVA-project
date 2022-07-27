import NewCityForm from "./NewCityForm";
import React, { useEffect, useState, useContext } from "react";
import useHttp from "../../hooks/useHttp";
import AuthContext from "../../store/auth-context";
import { useHistory } from "react-router-dom";
import LoadingModal from "../UI/Modals/LoadingModal";
import InfoModal from "../UI/Modals/InfoModal";

function NewCity() {
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [countries, setCountries] = useState(null);
  const [infoData, setInfoData] = useState(null);
  const history = useHistory();

  useEffect(() => {
    async function getCountries() {
      const requestConfig = {
        url: `https://localhost:44386/api/countries/`,
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
      setCountries(data);
    }

    getCountries();
  }, [token, sendRequest]);

  async function newCityHandler(newData) {
    const countryId = countries.reduce(
      (previousValue, currentValue) =>
        currentValue.name === newData.country ? currentValue.id : previousValue,
      0
    );
    const requestConfig = {
      url: "https://localhost:44386/api/cities",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        name: newData.name,
        countryId: countryId,
      }),
    };
    const data = await sendRequest(requestConfig);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "City successfully created",
    });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  function hideSuccessModalHandler() {
    setInfoData(null);
    history.replace("/");
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
      {countries !== null && (
        <NewCityForm
          title="New City"
          items={countries}
          onSubmit={newCityHandler}
        />
      )}
    </React.Fragment>
  );
}

export default NewCity;
