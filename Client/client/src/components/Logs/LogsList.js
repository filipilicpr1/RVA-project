import Card from "../UI/Card/Card";
import LogItem from "./LogItem";
import classes from "./LogsList.module.css";

function LogsList(props) {
  const logsList = props.items.map((item) => {
    return (
      <LogItem
        key={item.timestamp}
        timestamp={item.timestamp}
        eventType={item.eventType}
        message={item.message}
      />
    );
  });

  return (
    <section className={classes.logs}>
      <Card>
        <section className={classes.title}>
          <h1>Logs</h1>
        </section>
        <ul>{logsList}</ul>
      </Card>
    </section>
  );
}

export default LogsList;
