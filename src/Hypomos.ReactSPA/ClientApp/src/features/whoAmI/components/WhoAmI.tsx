import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import { loadWhoAmIAsync } from '../actions';

class WhoAmI extends React.Component<Props> {
    render() {
        const { whoAmI } = this.props;

        if (whoAmI.isLoadingUser) {
            return <div>loading...</div>;
        }

        if (whoAmI.user) {
            console.log(whoAmI.user);

            return <div>{whoAmI.user}</div>;
        }
    }
}

const mapStateToProps = (state: RootState) => ({
    whoAmI: state.whoAmI
});

const dispatchProps = {
    loadWhoAmIAsync: loadWhoAmIAsync.request,
};


type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps

export default connect(mapStateToProps, dispatchProps)(WhoAmI);