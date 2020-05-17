import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';

const mapStateToProps = (state: RootState) => (
    {
        oidc: state.whoAmI.oidc
    });

type Props = ReturnType<typeof mapStateToProps>;

class Authenticated extends React.Component<Props> {

    render() {
        const { oidc } = this.props;

        if (oidc && oidc.user) {
            return this.props.children;
        }

        return (null);
    }
}

export default connect(mapStateToProps)(Authenticated);
