import React, { useState, useRef } from "react";
import classes from "./UserForm.module.css";
import Card from "../UI/Card/Card";
import Input from "../UI/Inputs/Input";
import Button from "../UI/Button/Button";
import useHttp from "../../hooks/useHttp";
import ErrorModal from "../UI/Modals/ErrorModal";
import LoadingModal from "../UI/Modals/LoadingModal";
import {useHistory} from 'react-router-dom';

function UserForm(props) {
  const history = useHistory();
  const { isLoading, sendRequest } = useHttp();
  const [errorData, setErrorData] = useState(null);
  const input1Ref = useRef();
  const [isInput1Valid, setisInput1Valid] = useState(false);
  const [isInput1Touched, setisInput1Touched] = useState(false);
  const input2Ref = useRef();
  const [isInput2Valid, setisInput2Valid] = useState(false);
  const [isInput2Touched, setIsInput2Touched] = useState();
  const isFormValid = isInput1Valid && isInput2Valid;

  function input1ChangeHandler() {
    setisInput1Valid(input1Ref.current.value.trim().length !== 0);
  }

  function input1BlurHandler() {
    setisInput1Touched(true);
  }

  function input2ChangeHandler() {
    setisInput2Valid(input2Ref.current.value.trim().length !== 0);
  }

  function input2BlurHandler() {
    setIsInput2Touched(true);
  }

  function hideModalHandler() {
    setErrorData(null);
  }

  async function submitHandler(event) {
    event.preventDefault();
    const requestConfig = {...props.requestConfig,
      body : props.bodyMaker({
        input1 : input1Ref.current.value,
        input2 : input2Ref.current.value
      })
    }
    const data = await sendRequest(requestConfig);
    if (data.hasError) {
      setErrorData({
        message: data.message,
      });
      return;
    }
    props.dataHandler(data);
    history.replace('/');
  }

  return (
    <React.Fragment>
      {isLoading && <LoadingModal />}
      {errorData && (
        <ErrorModal
          title="Error"
          message={errorData.message}
          onConfirm={hideModalHandler}
        />
      )}

      <Card className={classes.login}>
        <section className={classes.title}>
          <h1>{props.title}</h1>
        </section>
        <form onSubmit={submitHandler}>
          <Input
            ref={input1Ref}
            type={props.input1.type}
            id={props.input1.id}
            label={props.input1.label}
            isValid={isInput1Valid}
            touched={isInput1Touched}
            onChange={input1ChangeHandler}
            onBlur={input1BlurHandler}
          />
          <Input
            ref={input2Ref}
            type={props.input2.type}
            id={props.input2.id}
            label={props.input2.label}
            isValid={isInput2Valid}
            touched={isInput2Touched}
            onChange={input2ChangeHandler}
            onBlur={input2BlurHandler}
          />
          <Button type="submit" disabled={!isFormValid}>
            Submit
          </Button>
        </form>
      </Card>
    </React.Fragment>
  );
}

export default UserForm;
