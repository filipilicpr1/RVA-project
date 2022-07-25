import classes from "./LogItem.module.css";

function LogItem(props) {
  return (
    <li className={classes.log}>
      <div>
        <h3 className={classes.timestamp}>{props.timestamp}</h3>
        <div>
          <p
            className={`${classes.eventType} ${
              props.eventType === "INF" ? classes.info : classes.error
            }`}
          >
            [{props.eventType}]{" "}
          </p>
          {props.message}
        </div>
      </div>
    </li>
  );
}

export default LogItem;
