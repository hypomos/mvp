import React, { Component } from 'react';
import { connect } from 'react-redux';

import { ConnectedRouter } from 'connected-react-router';
import { Switch, Route } from 'react-router-dom';

import { RootState } from 'MyTypes';
import { history } from './store';
import { getPath } from './router-paths';
import Loading from './features/config/components/Loader';
import Home from './routes/Home';
import HypomosApp from './routes/App';
import Callback from './routes/Callback';


const mapStateToProps = (state: RootState) => (
  {
      config: state.config
  });

type Props = ReturnType<typeof mapStateToProps>

class App extends Component<Props> {
  render() {
    if (!this.props.config.ready) {
      return (<Loading />);
    }

    return (
      <ConnectedRouter history={history}>
        <Switch>
          <Route exact path={getPath('home')} render={Home} />
          <Route exact path={getPath('app')} render={HypomosApp} />
          <Route exact path={getPath('callback')} render={Callback} />

          <Route exact path={getPath('items')} render={HypomosApp} />

          <Route exact path={getPath('collections')} render={HypomosApp} />
          <Route exact path={getPath('addCollection')} render={HypomosApp} />
          <Route path={getPath('viewCollection', ':collectionId')} render={(props: any) => <HypomosApp {...props} />} />
          <Route path={getPath('editCollection', ':collectionId')} render={(props: any) => <HypomosApp {...props} />} />

          <Route exact path={getPath('cleaning')} render={HypomosApp} />

          {/* <Route exact path={getPath('addArticle')} render={AddArticle} />
            <Route
              exact
              path={getPath('editArticle', ':articleId')}
              render={(props: any) => <EditArticle {...props} />}
            />
            <Route
              exact
              path={getPath('viewArticle', ':articleId')}
              render={(props: any) => <ViewArticle {...props} />}
            /> */}
          <Route render={() => <div>Page not found!</div>} />
        </Switch>
      </ConnectedRouter>
    );
  }
}

export default connect(mapStateToProps)(App);