import UserForm from "../components/UserForms/UserForm";
import { useContext } from "react";
import AuthContext from "../store/auth-context";

function LoginPage() {
  const ctx = useContext(AuthContext);
  const requestConfig = {
    url: "https://localhost:44386/api/users/login",
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  };
  const input1 = {
    id: "username",
    type: "text",
    label: "Username:",
  };
  const input2 = {
    id: "password",
    type: "password",
    label: "Password:",
  };

  function createBody(inputData) {
    return JSON.stringify({
        username : inputData.input1,
        password : inputData.input2
    });
  }

  return (
    <UserForm
      requestConfig={requestConfig}
      dataHandler={ctx.onLogin}
      title="Log in"
      input1={input1}
      input2={input2}
      bodyMaker={createBody}
    />
  );
}

export default LoginPage;
