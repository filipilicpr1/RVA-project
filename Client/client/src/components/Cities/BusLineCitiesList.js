import BusLineCityItem from "./BusLineCityItem";
import Card from "../UI/Card/Card";
import classes from "./BusLineCitiesList.module.css";

function BusLineCitiesList(props) {
  const citiesList = props.items.map((item) => {
    return (
      <BusLineCityItem
        key={item.id}
        id={item.id}
        name={item.name}
        countryName={item.country.name}
        onClick={props.onClick}
        text={props.text}
      />
    );
  });
  return (
    <section className={classes.cities}>
      <Card>
        <section className={classes.title}>
          <h1>{props.items.length > 0 ? "Cities" : "No Cities"}</h1>
        </section>
        <ul>{citiesList}</ul>
      </Card>
    </section>
  );
}

export default BusLineCitiesList;
