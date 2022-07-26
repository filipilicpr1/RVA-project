import Button from "../UI/Button/Button";
import classes from "./BusLineCityItem.module.css";

function BusLineCityItem(props) {
  function removeCityHandler() {
    props.onRemove({cityId:props.id});
  }

  return (
    <li className={classes.item}>
      <figure>
        <blockquote>
          <p>{props.name}</p>
        </blockquote>
        <figcaption>{props.countryName}</figcaption>
      </figure>
      <Button className={classes.button} onClick={removeCityHandler}>
        Remove
      </Button>
    </li>
  );
}

export default BusLineCityItem;
