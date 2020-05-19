import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import * as storageSelectors from '../../storages/selectors';
import { loadStoragesAsync } from '../../storages/actions'

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
        debugger;
        this.props.loadStoragesAsync();
    }

    render() {
        return (
            <div>Hello!</div>
        );
    }
}

export default connect(mapStateToProps, dispatchProps)(Home);