import Card from "../UI/Card/Card";
import Input from "../UI/Inputs/Input";
import Select from "../UI/Inputs/Select";
import Button from "../UI/Button/Button";
import {useRef, useState} from 'react';
import classes from './EditBusLineForm.module.css';

const BUSLINE_TYPES = [
    {
      id: 1,
      name: "GRADSKA",
    },
    {
      id: 2,
      name: "MEDJUGRADSKA",
    },
    {
      id: 3,
      name: "INTERNACIONALNA",
    }
  ];

function EditBusLineForm(props) {
    const labelRef = useRef();
    const [isLabelValid, setIsLabelValid] = useState(false);
    const [isLabelTouched, setIsLabelTouched] = useState(false);
    const busLineTypeRef = useRef();

    function labelChangeHandler() {
        setIsLabelValid(labelRef.current.value.trim().length !== 0);
    }

    function labelBlurHandler() {
        setIsLabelTouched(true);
    }

    function submitHandler(event) {
        event.preventDefault();

        props.onSubmit({
            label: labelRef.current.value,
            busLineType : busLineTypeRef.current.value
        });
    }

    return(
        <Card className={classes.edit}>
        <section className={classes.title}>
          <h1>{props.title}</h1>
        </section>
        <form onSubmit={submitHandler}>
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
            ref={busLineTypeRef}
            id="busLineType"
            label="Type:"
            items={BUSLINE_TYPES}
          />
          <Button type="submit" disabled={!isLabelValid}>
            Submit
          </Button>
        </form>
      </Card>
    );
}

export default EditBusLineForm;