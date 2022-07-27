import Button from "../UI/Button/Button";
import classes from './BusLineBusItem.module.css';

function BusLineBusItem(props) {
    function removeBusHandler() {
        props.onRemove({busId:props.id});
      }
    
      return (
        <li className={classes.item}>
          <figure>
            <blockquote>
              <p>{props.name}</p>
            </blockquote>
            <figcaption>{props.manufacturerName} [{props.label}]</figcaption>
          </figure>
          <Button className={classes.button} onClick={removeBusHandler}>
            Remove
          </Button>
        </li>
      );
}

export default BusLineBusItem;