import React,{useRef} from 'react';
import Button from '../UI/Button/Button';
import Input from '../UI/Inputs/Input';


function BusLineFilterForm(props) {
    const busLineLabelRef = useRef();
    const busLabelRef = useRef();
    const busNameRef = useRef();
    const cityRef = useRef();

    function applyFilterHandler() {
        props.onApply({
            busLineLabel : busLineLabelRef.current.value.trim().toLowerCase(),
            busName : busNameRef.current.value.trim().toLowerCase(),
            busLabel : busLabelRef.current.value.trim().toLowerCase(),
            city : cityRef.current.value.trim().toLowerCase()
        });
    }

    return(
        <React.Fragment>
           <Input
            ref={busLineLabelRef}
            type="text"
            id="busLineLabel"
            label="Label"
            isValid={true}
            touched={true}
          />
          <Input
            ref={busNameRef}
            type="text"
            id="busName"
            label="Bus Name"
            isValid={true}
            touched={true}
          />
          <Input
            ref={busLabelRef}
            type="text"
            id="busLabel"
            label="Bus Label"
            isValid={true}
            touched={true}
          />
          <Input
            ref={cityRef}
            type="text"
            id="city"
            label="City"
            isValid={true}
            touched={true}
          />
          <Button onClick={applyFilterHandler}>
            Apply
          </Button> 
          <Button onClick={props.onRemove}>
            Remove
          </Button> 
        </React.Fragment>
    );

}

export default BusLineFilterForm;