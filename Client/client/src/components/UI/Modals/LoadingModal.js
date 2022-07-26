import ReactDOM from 'react-dom';
import Card from '../Card/Card';
import React from 'react';
import classes from './LoadingModal.module.css';
import {useHistory} from 'react-router-dom';

function Backdrop(props) {
    return <div className={classes.backdrop} onClick={props.onClick} />;
  }
  
  function ModalOverlay(props) {
    return (
      <Card className={classes.modal}>
        <div className={classes.spinner}></div>
      </Card>
    );
  }
  
  const LoadingModal = (props) => {
    const history = useHistory();
    function clickHandler() {
      history.push('/');
    }
    return (
      <React.Fragment>
        {ReactDOM.createPortal(<Backdrop  onClick={clickHandler}/>, document.getElementById('backdrop-root'))}
        {ReactDOM.createPortal(<ModalOverlay/>, document.getElementById('overlay-root'))}
      </React.Fragment>
    );
  };

export default LoadingModal;