import React, { FC } from 'react';

import Header from '../components/Header';
import Navigation from '../components/Navigation';

type Props = {
    // something like: renderActionsMenu?: () => JSX.Element
}

const Main: FC<Props> = ({children}) => {
  return (
    <div className="App bg-gray-900 font-sans leading-normal tracking-normal" >
      <Header />
      <div className="flex flex-col md:flex-row">
        <Navigation />

        <main className="main-content flex-1 bg-gray-100 mt-12 md:mt-2 pb-24 md:pb-5">
          { children }
        </main>
      </div>
    </div>
  );
}

export default Main;
