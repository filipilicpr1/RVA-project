import React from "react";
import ReactDOM from 'react-dom';
import classes from "./InfoModal.module.css";
import Card from "../Card/Card";
import Button from "../Button/Button";

function Backdrop(props) {
    return <div className={classes.backdrop} onClick={props.onClick} />;
  }
  
  function ModalOverlay(props) {
    return (
      <Card className={classes.modal}>
        <header className={classes.header}>
          <h2>{props.title}</h2>
        </header>
        <div className={classes.content}>
          <p>{props.message}</p>
        </div>
        <footer className={classes.actions}>
          <Button onClick={props.onConfirm} className={classes.button}>Okay</Button>
          {props.onClose !== undefined && <Button onClick={props.onClose} className={classes.button}>Close</Button>}
        </footer>
      </Card>
    );
  }
  
  const InfoModal = (props) => {
    return (
      <React.Fragment>
        {ReactDOM.createPortal(<Backdrop onClick={props.onClose !== undefined ? props.onClose : props.onConfirm} />, document.getElementById('backdrop-root'))}
        {ReactDOM.createPortal(<ModalOverlay title={props.title} message={props.message} onConfirm={props.onConfirm} onClose={props.onClose}/>, document.getElementById('overlay-root'))}
      </React.Fragment>
    );
  };

export default InfoModal;