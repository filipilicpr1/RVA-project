import React, { useReducer, useRef, useContext, useState } from "react";
import classes from "./RegisterForm.module.css";
import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import Input from "../UI/Inputs/Input";
import Select from "../UI/Inputs/Select";
import useHttp from "../../hooks/useHttp";
import AuthContext from "../../store/auth-context";
import InfoModal from "../UI/Modals/InfoModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import { useHistory } from "react-router-dom";

const USER_TYPES = [
  {
    id: 1,
    content: "GUEST",
  },
  {
    id: 2,
    content: "ADMIN",
  },
];

const initialInputState = {
  isUsernameValid: false,
  isUsernameTouched: false,
  isPasswordValid: false,
  isPasswordTouched: false,
  isNameValid: false,
  isNameTouched: false,
  isLastNameValid: false,
  isLastNameTouched: false,
};

function inputReducer(state, action) {
  if (action.type === "USERNAME_CHANGE") {
    return { ...state, isUsernameValid: action.value };
  }
  if (action.type === "USERNAME_BLUR") {
    return { ...state, isUsernameTouched: true };
  }
  if (action.type === "PASSWORD_CHANGE") {
    return { ...state, isPasswordValid: action.value };
  }
  if (action.type === "PASSWORD_BLUR") {
    return { ...state, isPasswordTouched: true };
  }
  if (action.type === "NAME_CHANGE") {
    return { ...state, isNameValid: action.value };
  }
  if (action.type === "NAME_BLUR") {
    return { ...state, isNameTouched: true };
  }
  if (action.type === "LASTNAME_CHANGE") {
    return { ...state, isLastNameValid: action.value };
  }
  if (action.type === "LASTNAME_BLUR") {
    return { ...state, isLastNameTouched: true };
  }
  return initialInputState;
}

function RegisterForm() {
  const history = useHistory();
  const { isLoading, sendRequest } = useHttp();
  const ctx = useContext(AuthContext);
  const [infoData, setInfoData] = useState(null);
  const [inputState, dispatchInput] = useReducer(
    inputReducer,
    initialInputState
  );
  const usernameRef = useRef();
  const passwordRef = useRef();
  const nameRef = useRef();
  const lastNameRef = useRef();
  const userTypeRef = useRef();
  const isFormValid =
    inputState.isUsernameValid &&
    inputState.isPasswordValid &&
    inputState.isNameValid &&
    inputState.isLastNameValid;

  function usernameChangeHandler() {
    dispatchInput({
      type: "USERNAME_CHANGE",
      value: usernameRef.current.value.trim().length !== 0,
    });
  }

  function usernameBlurHandler() {
    dispatchInput({ type: "USERNAME_BLUR" });
  }

  function passwordChangeHandler() {
    dispatchInput({
      type: "PASSWORD_CHANGE",
      value: passwordRef.current.value.trim().length !== 0,
    });
  }

  function passwordBlurHandler() {
    dispatchInput({ type: "PASSWORD_BLUR" });
  }

  function nameChangeHandler() {
    dispatchInput({
      type: "NAME_CHANGE",
      value: nameRef.current.value.trim().length !== 0,
    });
  }

  function nameBlurHandler() {
    dispatchInput({ type: "NAME_BLUR" });
  }

  function lastNameChangeHandler() {
    dispatchInput({
      type: "LASTNAME_CHANGE",
      value: lastNameRef.current.value.trim().length !== 0,
    });
  }

  function lastNameBlurHandler() {
    dispatchInput({ type: "LASTNAME_BLUR" });
  }

  function hideErrorModalHandler() {
    setInfoData(null);
  }

  function hideSuccessModalHandler() {
    setInfoData(null);
    history.replace('/');
  }

  async function submitHandler(event) {
    event.preventDefault();

    const requestConfig = {
      url: "https://localhost:44386/api/users",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + ctx.token,
      },
      body: JSON.stringify({
        username: usernameRef.current.value,
        password: passwordRef.current.value,
        name: nameRef.current.value,
        lastName: lastNameRef.current.value,
        userType: userTypeRef.current.value,
      }),
    };
    const data = await sendRequest(requestConfig);
    setInfoData({
      title: data.hasError ? "Error" : "Success",
      message: data.hasError ? data.message : "User successfully added",
    });
  }

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {infoData && (
        <InfoModal
          title={infoData.title}
          message={infoData.message}
          onConfirm={infoData.title === "Error" ?  hideErrorModalHandler : hideSuccessModalHandler}
        />
      )}
      <Card className={classes.register}>
        <section className={classes.title}>
          <h1>Add New User</h1>
        </section>
        <form onSubmit={submitHandler}>
          <Input
            ref={usernameRef}
            type="text"
            id="username"
            label="Username:"
            isValid={inputState.isUsernameValid}
            touched={inputState.isUsernameTouched}
            onChange={usernameChangeHandler}
            onBlur={usernameBlurHandler}
          />
          <Input
            ref={passwordRef}
            type="password"
            id="password"
            label="Password:"
            isValid={inputState.isPasswordValid}
            touched={inputState.isPasswordTouched}
            onChange={passwordChangeHandler}
            onBlur={passwordBlurHandler}
          />
          <Input
            ref={nameRef}
            type="text"
            id="name"
            label="Name:"
            isValid={inputState.isNameValid}
            touched={inputState.isNameTouched}
            onChange={nameChangeHandler}
            onBlur={nameBlurHandler}
          />
          <Input
            ref={lastNameRef}
            type="text"
            id="lastName"
            label="Last Name:"
            isValid={inputState.isLastNameValid}
            touched={inputState.isLastNameTouched}
            onChange={lastNameChangeHandler}
            onBlur={lastNameBlurHandler}
          />
          <Select
            ref={userTypeRef}
            id="userType"
            label="User Type:"
            items={USER_TYPES}
          />
          <Button type="submit" disabled={!isFormValid}>
            Submit
          </Button>
        </form>
      </Card>
    </React.Fragment>
  );
}

export default RegisterForm;
