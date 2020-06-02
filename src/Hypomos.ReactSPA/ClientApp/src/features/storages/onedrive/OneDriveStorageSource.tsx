import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import withAuthProvider, { AuthComponentProps } from '../onedrive/AuthProvider';
import { getUserDetails } from './GraphService';

const mapStateToProps = (state: RootState) => ({
    config: state.config
});

const dispatchProps = {

};

// type Props = AuthComponentProps & ReturnType<typeof mapStateToProps> & typeof dispatchProps;

class OneDriveStorageSource extends React.Component<AuthComponentProps> {

    async getUserProfile() {
        try {
            var accessToken = await this.props.getAccessToken(this.props.config.oneDrive.scopes);

            if (accessToken) {
                // Get the user's profile from Graph
                var user = await getUserDetails(accessToken);
                this.setState({
                    isAuthenticated: true,
                    user: {
                        displayName: user.displayName,
                        email: user.mail || user.userPrincipalName
                    },
                    error: null
                });
            }
        }
        catch (err) {
            this.setState({
                isAuthenticated: false,
                user: {},
                error: err
            });
        }
    }

    render() {
        if (this.props.isAuthenticated) {
            return (
                <div>
                    Hello {this.props.user}
                </div>
            );
        }

        return (
            <div>
                Login!
            </div>
        );
    }
}

export default connect(mapStateToProps, dispatchProps)(withAuthProvider(OneDriveStorageSource));