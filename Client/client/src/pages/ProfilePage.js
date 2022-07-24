import UserForm from "../components/UserForms/UserForm";
import { useContext } from "react";
import AuthContext from "../store/auth-context";

function ProfilePage() {
    const ctx = useContext(AuthContext);
  const requestConfig = {
    url: "https://localhost:44386/api/users",
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "Authorization" : "Bearer " + ctx.token
    },
  };
  const input1 = {
    id: "name",
    type: "text",
    label: "Name:",
  };
  const input2 = {
    id: "lastName",
    type: "text",
    label: "Last Name:",
  };

  function createBody(inputData) {
    return JSON.stringify({
        id : ctx.user.id,
        name : inputData.input1,
        lastName : inputData.input2
    });
  }

  return (
    <UserForm
      requestConfig={requestConfig}
      dataHandler={ctx.onUpdate}
      title="Edit name and last name"
      input1={input1}
      input2={input2}
      bodyMaker={createBody}
    />
  );
}

export default ProfilePage;