import { useState, useCallback } from "react";

function useHttp() {
  const [isLoading, setIsLoading] = useState(false);
  //const [isError, setIsError] = useState(null);

  const sendRequest = useCallback(async (requestConfig) => {
    setIsLoading(true);
    const response = await fetch(requestConfig.url, {
      method: requestConfig.method || "GET",
      headers: requestConfig.headers || {},
      body: requestConfig.body || null,
    });
    let data = await response.json();
    if (!response.ok) {
        data = {...data, hasError: true};
    }

    setIsLoading(false);
    return data;
  }, []);

  return {
    isLoading: isLoading,
    sendRequest: sendRequest,
  };
}

export default useHttp;