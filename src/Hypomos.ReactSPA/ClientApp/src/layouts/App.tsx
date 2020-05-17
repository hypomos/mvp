import React, { FC } from 'react';

import Header from '../components/Header';
import WhoAmI from '../features/whoAmI/components/WhoAmI';
import Navigation from '../features/app/components/Navigation';

type Props = {
  // something like: renderActionsMenu?: () => JSX.Element
}

const Main: FC<Props> = ({ children }) => {
  return (
    <div className="h-screen bg-gray-900 font-sans leading-normal tracking-normal" >
      <div className="fixed top-0 pt-0 w-full z-20 h-auto">
        <Header >
          <WhoAmI />
        </Header>
      </div>

      <div className="flex flex-col md:flex-row mt-8">
        <div className="bg-gray-900">
          <Navigation />
        </div>

        <main className="main-content flex-1 bg-gray-100 mt-12 md:mt-2 pb-24 md:pb-5">
          {children}
        </main>
      </div>
    </div>
  );
}

export default Main;
