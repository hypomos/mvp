import React from 'react';
import { NavLink } from 'react-router-dom';

import Authenticated from './Authenticated';
import NotAuthenticated from './NotAuthenticated';
import userManager from '../userManager';
import { getPath } from '../../../router-paths';

class LaunchOrSignIn extends React.Component {
    onLogin(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        event.preventDefault();
        userManager.signinRedirect();
    }

    render() {
        return (
            <div>
                <NotAuthenticated>
                    <button className='bg-yellow-400 px-2 py-1 mb-1 rounded font-semibold' onClick={this.onLogin}>Login</button>
                </NotAuthenticated>
                <Authenticated>
                    <NavLink to={getPath('app')} className='bg-yellow-400 px-2 py-1 mb-1 rounded font-semibold'>
                        Launch
                    </NavLink>
                </Authenticated>
            </div>);
    }
}

export default LaunchOrSignIn;