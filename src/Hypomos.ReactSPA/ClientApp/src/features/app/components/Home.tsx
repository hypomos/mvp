import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import * as storageSelectors from '../../storages/selectors';
import { loadStoragesAsync } from '../../storages/actions'

import AddStorageSource from '../../storages/components/AddStorageSource';

const mapStateToProps = (state: RootState) => ({
    isLoading: state.storages.isLoadingStorages,
    storages: storageSelectors.getStorages(state),
});

const dispatchProps = {
    loadStoragesAsync: loadStoragesAsync.request
};

type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps;

class Home extends React.Component<Props> {
    componentDidMount() {
        this.props.loadStoragesAsync();
    }

    render() {
        if (this.props.storages.length === 0) {
            return (
                <React.Fragment>
                    <div>please add a storage source!</div>
                    <AddStorageSource />
                </React.Fragment>
            );
        }

        // if storage locations have been defined, but no content found, then we might display something similar as above

        return (
            <div>Hello!</div>
        );
    }
}

export default connect(mapStateToProps, dispatchProps)(Home);