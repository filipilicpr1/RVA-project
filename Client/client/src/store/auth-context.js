import React, { useState } from "react";

const AuthContext = React.createContext({
  isLoggedIn: false,
  token: null,
  user: {},
  onLogin: (data) => {},
  onLogout: () => {},
  onUpdate: (data) => {},
});

const initialToken = localStorage.getItem("token");
const initialUser = localStorage.getItem("user");

export function AuthContextProvider(props) {
  const [token, setToken] = useState(initialToken);
  const [user, setUser] = useState(
    initialUser !== null && JSON.parse(initialUser)
  );
  const isLoggedIn = token !== null;

  function loginHandler(data) {
    setToken(data.token);
    localStorage.setItem("token", data.token);
    const newUser = {
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      userType: data.userType,
    };
    setUser(newUser);
    localStorage.setItem("user", JSON.stringify(newUser));
  }

  function logoutHandler() {
    setToken(null);
    setUser(null);
    localStorage.removeItem("token");
    localStorage.removeItem("user");
  }

  function updateHandler(data) {
    setUser((prevUser) => {
      const newUser = { ...prevUser, name: data.name, lastName: data.lastName };
      localStorage.setItem("user", JSON.stringify(newUser));
      return newUser;
    });
  }

  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: isLoggedIn,
        token: token,
        user: user,
        onLogout: logoutHandler,
        onLogin: loginHandler,
        onUpdate: updateHandler,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
}

export default AuthContext;
