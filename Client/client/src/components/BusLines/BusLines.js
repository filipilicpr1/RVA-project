import BusLinesList from "./BusLinesList";
import React, { useState, useContext, useEffect } from "react";
import AuthContext from "../../store/auth-context";
import useHttp from "../../hooks/useHttp";
import { useHistory } from "react-router-dom";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import classes from "./BusLines.module.css";
import BusLineFilter from "./BusLineFIlter";

function filterBusLines(busLines, filters) {
  return busLines.filter((item) => {
    const busLineLabelFilter = item.label
      .toLowerCase()
      .includes(filters.busLineLabel);
    const cityFilter =
      filters.city === "" ||
      item.cities.filter((city) =>
        city.name.toLowerCase().includes(filters.city)
      ).length !== 0;
    const busNameFilter =
      filters.busName === "" ||
      item.buses.filter((bus) =>
        bus.name.toLowerCase().includes(filters.busName)
      ).length !== 0;
    const busLabelFilter =
      filters.busLabel === "" ||
      item.buses.filter((bus) =>
        bus.label.toLowerCase().includes(filters.busLabel)
      ).length !== 0;
    return busLineLabelFilter && cityFilter && busNameFilter && busLabelFilter;
  });
}

let isInitial = true;

function BusLines() {
  const history = useHistory();
  const ctx = useContext(AuthContext);
  const token = ctx.token;
  const { isLoading, sendRequest } = useHttp();
  const [busLines, setBusLines] = useState(null);
  const initialFilter = {
    busLineLabel: "",
    busName: "",
    busLabel: "",
    city: "",
  };
  const [filter, setFilter] = useState(initialFilter);
  const [errorData, setErrorData] = useState(null);
  const [getToggle, setGetToggle] = useState(false);

  let filteredBusLines;
  if (busLines !== null) {
    filteredBusLines = filterBusLines(busLines, filter);
  }

  useEffect(() => {
    async function getBusLines() {
      const requestConfig = {
        url: "https://localhost:44386/api/buslines",
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
      setBusLines(data);
      isInitial = false;
    }
    if (isInitial) {
      getBusLines();
    }
    const timer = setTimeout(async () => {
      await getBusLines();
      setGetToggle((prevState) => !prevState);
    }, 10000);

    return () => {
      clearTimeout(timer);
    };
  }, [token, sendRequest, getToggle]);

  function hideModalHandler() {
    setErrorData(null);
    history.replace("/");
  }

  function applyFilter(data) {
    setFilter(data);
  }

  function removeFilter() {
    setFilter(initialFilter);
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
      {busLines !== null && busLines.length !== 0 && (
        <BusLineFilter onApply={applyFilter} onRemove={removeFilter} />
      )}
      {busLines !== null && <BusLinesList items={filteredBusLines} />}
      {busLines !== null && busLines.length === 0 && (
        <section className={classes["no-bus-lines"]}>
          <h1>No Bus Lines!</h1>
        </section>
      )}
    </React.Fragment>
  );
}

export default BusLines;
