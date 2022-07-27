import classes from "./BusLineBusesList.module.css";
import BusLineBusItem from "./BusLineBusItem";
import Card from "../UI/Card/Card";

function BusLineBusesList(props) {
  const citiesList = props.items.map((item) => {
    return (
      <BusLineBusItem
        key={item.id}
        id={item.id}
        name={item.name}
        label={item.label}
        manufacturerName={item.manufacturer.name}
        onRemove={props.onRemove}
      />
    );
  });
  return (
    <section className={classes.buses}>
      <Card>
        <section className={classes.title}>
          <h1>{props.items.length > 0 ? "Buses" : "No Buses"}</h1>
        </section>
        <ul>{citiesList}</ul>
      </Card>
    </section>
  );
}

export default BusLineBusesList;
