import Button from "../UI/Button/Button";
import classes from "./BusLineCityItem.module.css";

function BusLineCityItem(props) {
  function removeCityHandler() {
    props.onClick({cityId:props.id});
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
        {props.text}
      </Button>
    </li>
  );
}

export default BusLineCityItem;
