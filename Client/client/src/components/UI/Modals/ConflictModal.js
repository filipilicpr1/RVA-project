import React from "react";
import InfoModal from "./InfoModal";
import LoadingModal from "./LoadingModal";

function ConflictModal(props) {
  return (
    <React.Fragment>
      {props.isLoading && <LoadingModal />}
      {props.infoData !== null && props.conflictData === null && (
        <InfoModal
          title={props.infoData.title}
          message={props.infoData.message}
          onConfirm={
            props.infoData.title === "Error"
              ? props.hideErrorModalHandler
              : props.hideSuccessModalHandler
          }
        />
      )}
      {props.infoData === null && props.conflictData !== null && (
        <InfoModal
          title={props.conflictData.title}
          message={props.conflictData.message}
          onConfirm={props.confirmConflictHandler}
          onClose={props.closeConflictHandler}
        />
      )}
    </React.Fragment>
  );
}

export default ConflictModal;
