import React from "react";
import classes from './Select.module.css';

const Select = React.forwardRef((props, ref) => {
  return (
    <div className={classes.control}>
      <label htmlFor={props.id}>{props.label}</label>
      <select ref={ref} id={props.id}>
        {props.items.map(item => <option key={item.id}>{item.content}</option>)}
      </select>
    </div>
  );
});

export default Select;