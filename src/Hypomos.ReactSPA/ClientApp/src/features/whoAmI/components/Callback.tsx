import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { push } from 'connected-react-router'
import userManager from "../userManager";
import { getPath } from '../../../router-paths';


var dispatchProps = { 
    push 
};

type Props = typeof dispatchProps;

class CallbackPage extends React.Component<Props> {
  render() {
    // just redirect to '/' in both cases
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={() => this.props.push(getPath('app'))}
        errorCallback={error => {
          this.props.push("/");
          console.error(error);
        }}
        >
        <div>Redirecting...</div>
      </CallbackComponent>
    );
  }
}


export default connect(null, dispatchProps)(CallbackPage);