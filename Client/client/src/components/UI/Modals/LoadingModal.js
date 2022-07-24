import ReactDOM from 'react-dom';
import Card from '../Card/Card';
import React from 'react';
import classes from './LoadingModal.module.css';

function Backdrop(props) {
    return <div className={classes.backdrop} />;
  }
  
  function ModalOverlay(props) {
    return (
      <Card className={classes.modal}>
        <div className={classes.spinner}></div>
      </Card>
    );
  }
  
  const LoadingModal = (props) => {
    return (
      <React.Fragment>
        {ReactDOM.createPortal(<Backdrop />, document.getElementById('backdrop-root'))}
        {ReactDOM.createPortal(<ModalOverlay/>, document.getElementById('overlay-root'))}
      </React.Fragment>
    );
  };

export default LoadingModal;