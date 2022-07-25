import Card from "../UI/Card/Card";
import Button from "../UI/Button/Button";
import {useState} from 'react';
import BusLineFilterForm from "./BusLineFilterForm";
import classes from './BusLineFilter.module.css';

function BusLineFilter(props) {
    const [showFilter, setShowFilter] = useState(false);

    function showFilterHandler() {
        setShowFilter(true);
    }

    function hideFilterHandler() {
        setShowFilter(false);
        props.onRemove();
    }

    return (
        <Card className={classes.filter}>
            {!showFilter && <Button onClick={showFilterHandler}>Filter</Button>}
            {showFilter && <BusLineFilterForm onRemove={hideFilterHandler} onApply={props.onApply} />}
        </Card>
    );

}

export default BusLineFilter;