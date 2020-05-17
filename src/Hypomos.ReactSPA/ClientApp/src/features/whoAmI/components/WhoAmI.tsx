import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import { toggleDropDown } from '../actions';
import userManager from "../userManager";

class WhoAmI extends React.Component<Props> {

    onLogin(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        event.preventDefault();
        userManager.signinRedirect();
    }

    onLogout(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        event.preventDefault();
        userManager.signoutRedirect();
    }

    toggleDropDown(){
        this.props.toggleDropDown();
    }

    showLoggedIn() {
        const { oidc } = this.props;

        if (!oidc || !oidc.user) {
            return <div></div>;
        }

        let dropDownClasses = "dropdownlist absolute bg-gray-900 text-white right-0 mt-3 p-3 overflow-auto z-30 w-64";
        if (!this.props.showDropDown) {
            dropDownClasses += " invisible";
        }

        return (
            <div className="relative inline-block">
                <button onClick={() => this.toggleDropDown()} className="drop-button text-white focus:outline-none">
                    <span className="pr-2"><i className="fas fa-user"></i></span>
                    {oidc.user.profile.given_name}
                    <svg className="h-3 fill-current inline" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                        <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                    </svg>
                </button>
                <div id="myDropdown" className={dropDownClasses} >
                    {/* <input type="text" className="drop-search p-2 text-gray-600" placeholder="Search.." id="myInput"></input>
                    <a href="#" className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block"><i className="fa fa-user fa-fw"></i> Profile</a>
                    <a href="#" className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block"><i className="fa fa-cog fa-fw"></i> Settings</a> */}
                    <div className="border border-gray-800"></div>
                    <button className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block" onClick={this.onLogout}><i className="fas fa-sign-out-alt fa-fw"></i> Log Out</button>
                </div>
            </div>
        );
    }

    render() {
        const { oidc } = this.props;

        if (oidc && oidc.isLoadingUser) {
            return <div></div>;
        }

        if (oidc && oidc.user) {
            return this.showLoggedIn();
        }

        return (
            <button className="bg-yellow-300" onClick={this.onLogin}>Login</button>
        );
    }
}

const mapStateToProps = (state: RootState) => (
    {
        showDropDown: state.whoAmI.showDropDown,
        oidc: state.whoAmI.oidc
    });

const dispatchProps = {
    toggleDropDown: toggleDropDown
};


type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps

export default connect(mapStateToProps, dispatchProps)(WhoAmI);