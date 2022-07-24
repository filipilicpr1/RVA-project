import React from "react";
import ReactDOM from 'react-dom';
import classes from "./ErrorModal.module.css";
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
          <Button onClick={props.onClick}>Okay</Button>
        </footer>
      </Card>
    );
  }
  
  const ErrorModal = (props) => {
    return (
      <React.Fragment>
        {ReactDOM.createPortal(<Backdrop onClick={props.onConfirm} />, document.getElementById('backdrop-root'))}
        {ReactDOM.createPortal(<ModalOverlay title={props.title} message={props.message} onClick={props.onConfirm} />, document.getElementById('overlay-root'))}
      </React.Fragment>
    );
  };

export default ErrorModal;