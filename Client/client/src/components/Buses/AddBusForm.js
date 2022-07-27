import React,{useState, useRef} from 'react';
import Card from '../UI/Card/Card';
import Select from '../UI/Inputs/Select';
import Button from '../UI/Button/Button';
import classes from './AddBusForm.module.css';
import Input from '../UI/Inputs/Input';

function AddBusForm(props) {
    const nameRef = useRef();
    const [isNameValid, setIsNameValid] = useState(false);
    const [isNameTouched, setIsNameTouched] = useState(false);
    const labelRef = useRef();
    const [isLabelValid, setIsLabelValid] = useState(false);
    const [isLabelTouched, setIsLabelTouched] = useState(false);
    const manufacturerRef = useRef();
    const isFormValid = isNameValid && isLabelValid;
  
    function nameChangeHandler() {
      setIsNameValid(nameRef.current.value.trim().length !== 0);
    }
  
    function nameBlurHandler() {
      setIsNameTouched(true);
    }

    function labelChangeHandler() {
        setIsLabelValid(labelRef.current.value.trim().length !== 0);
      }
    
      function labelBlurHandler() {
        setIsLabelTouched(true);
      }
  
    function submitHandler(event) {
      event.preventDefault();
      props.onSubmit({
        name: nameRef.current.value,
        label: labelRef.current.value,
        manufacturer: manufacturerRef.current.value,
      });
    }
  
    return (
      <Card className={classes['new-bus']}>
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
          <Input
            ref={labelRef}
            type="text"
            id="label"
            label="Label:"
            isValid={isLabelValid}
            touched={isLabelTouched}
            onChange={labelChangeHandler}
            onBlur={labelBlurHandler}
          />
          <Select
            ref={manufacturerRef}
            id="manufacturer"
            label="Manufacturer:"
            items={props.items}
          />
          <Button type="submit" disabled={!isFormValid}>
            Submit
          </Button>
        </form>
      </Card>
    );
}

export default AddBusForm;