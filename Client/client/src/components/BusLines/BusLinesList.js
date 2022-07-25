import classes from "./BusLinesList.module.css";
import Card from "../UI/Card/Card";
import BusLineItem from "./BusLineItem";

function BusLinesList(props) {
  const busLinesList = props.items.map((item) => {
    return (
      <BusLineItem
        key={item.id}
        id={item.id}
        label={item.label}
        busLineType={item.busLineType}
      />
    );
  });
  return (
    <section className={classes["bus-lines"]}>
      <Card>
        <section className={classes.title}>
          <h1>Bus Lines</h1>
        </section>
        <ul>{busLinesList}</ul>
      </Card>
    </section>
  );
}

export default BusLinesList;
