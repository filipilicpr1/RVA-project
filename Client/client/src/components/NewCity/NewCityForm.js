import classes from "./NewCityForm.module.css";
import React, { useState, useRef } from "react";
import Card from "../UI/Card/Card";
import Input from "../UI/Inputs/Input";
import Select from "../UI/Inputs/Select";
import Button from "../UI/Button/Button";


function NewCityForm(props) {
  const nameRef = useRef();
  const [isNameValid, setIsNameValid] = useState(false);
  const [isNameTouched, setIsNameTouched] = useState(false);
  const countryRef = useRef();

  function nameChangeHandler() {
    setIsNameValid(nameRef.current.value.trim().length !== 0);
  }

  function nameBlurHandler() {
    setIsNameTouched(true);
  }

  function submitHandler(event) {
    event.preventDefault();
    props.onSubmit({
      name: nameRef.current.value,
      country: countryRef.current.value,
    });
  }

  return (
    <Card className={classes['new-city']}>
      <section className={classes.title}>
        <h1>{props.title}</h1>
      </section>
      <form onSubmit={submitHandler}>
        <Input
          ref={nameRef}
          type="text"
          id="name"
          label="Name:"
          isValid={isNameValid}
          touched={isNameTouched}
          onChange={nameChangeHandler}
          onBlur={nameBlurHandler}
        />
        <Select
          ref={countryRef}
          id="country"
          label="Country:"
          items={props.items}
        />
        <Button type="submit" disabled={!isNameValid}>
          Submit
        </Button>
      </form>
    </Card>
  );
}

export default NewCityForm;
