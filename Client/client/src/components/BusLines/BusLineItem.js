import classes from "./BusLineItem.module.css";
import {Link} from 'react-router-dom';

function BusLineItem(props) {
  return (
    <li className={classes.item}>
      <figure>
        <blockquote>
          <p>{props.label}</p>
        </blockquote>
        <figcaption>{props.busLineType}</figcaption>
      </figure>
      <Link to={`/bus-lines/${props.id}`} className={classes.link}>
        View 
      </Link>
    </li>
  );
}

export default BusLineItem;
