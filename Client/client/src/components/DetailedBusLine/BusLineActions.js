import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import { Link } from "react-router-dom";
import classes from "./BusLineActions.module.css";

function BusLineActions(props) {
  return (
    <Card className={classes.item}>
      <Link to={`/bus-lines/${props.id}/add-city`} className={classes.button}>
        Add City
      </Link>
      <Link to={`/bus-lines/${props.id}/add-bus`} className={classes.button}>
        Add Bus
      </Link>
      <Button className={classes.button}>Duplicate</Button>
      <Button className={classes.button}>Delete</Button>
    </Card>
  );
}

export default BusLineActions;
