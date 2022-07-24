import React from 'react';
import classes from './Input.module.css';


const Input = React.forwardRef((props, ref) => {

    return(
        <div className={`${classes.control} ${
            !props.isValid && props.touched ? classes.invalid : ""
          }`}>
            <label htmlFor={props.id}>{props.label}</label>
            <input ref={ref} type={props.type} id={props.id} onChange={props.onChange} onBlur={props.onBlur} />
        </div>
    );
});

export default Input;