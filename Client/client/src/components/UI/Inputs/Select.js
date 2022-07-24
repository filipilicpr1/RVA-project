import React from "react";

const Select = React.forwardRef((props, ref) => {
  return (
    <div>
      <select ref={ref}>
        {props.items.map(item => <option key={Math.random().toString()}>{item}</option>)}
      </select>
    </div>
  );
});

export default Select;