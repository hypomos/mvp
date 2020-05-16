import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { Switch, Route } from 'react-router';
import { OidcProvider } from 'redux-oidc';

import store, { history } from './store';
import { getPath } from './router-paths';
import userManager from './features/whoAmI/userManager';
import Loading from './routes/Loading';
import Home from './routes/Home';
import HypomosApp from './routes/App';
import Callback from './routes/Callback';

class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <OidcProvider store={store} userManager={userManager}>
          <ConnectedRouter history={history}>
            <Switch>
              <Route exact path={getPath('loading')} render={Loading} />
              <Route exact path={getPath('home')} render={Home} />
              <Route exact path={getPath('app')} render={HypomosApp} />
              <Route exact path={getPath('callback')} render={Callback} />
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
        </OidcProvider>
      </Provider>
    );
  }
}

export default App;