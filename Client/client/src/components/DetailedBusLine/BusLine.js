import classes from "./BusLine.module.css";
import Card from "../UI/Card/Card";
import { Link } from "react-router-dom";

function BusLine(props) {
  return (
    <Card className={classes["bus-line"]}>
      <section className={classes.title}>
        <h1>Bus Line</h1>
      </section>
      <div className={classes.item}>
        <figure>
          <blockquote>
            <p>{props.label}</p>
          </blockquote>
          <figcaption>{props.busLineType}</figcaption>
        </figure>

        <Link to={`/bus-lines/${props.id}/edit`} className={classes.link}>
          Edit
        </Link>
      </div>
    </Card>
  );
}

export default BusLine;
