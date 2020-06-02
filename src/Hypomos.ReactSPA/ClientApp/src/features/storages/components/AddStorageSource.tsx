import React from 'react';
import { connect } from 'react-redux';

import OneDriveStorageSource from '../onedrive/OneDriveStorageSource';

class AddStorageSource extends React.Component {
    render() {
        return (
            <div>
                <OneDriveStorageSource />
            </div>
        );
    }
}

export default connect()(AddStorageSource);