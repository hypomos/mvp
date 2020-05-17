import React, { FC } from 'react';

import Header from '../components/Header';
import LaunchOrSignIn from '../features/whoAmI/components/LaunchOrSignIn';


type Props = {
  // something like: renderActionsMenu?: () => JSX.Element
}

const Main: FC<Props> = ({ children }) => {
  return (
    <div className="h-screen bg-gray-900 font-sans leading-normal tracking-normal" >
      <div className="fixed top-0 pt-0 w-full z-20 h-auto">
        <Header >
            <LaunchOrSignIn />
        </Header>
      </div>

      <div className="flex flex-col md:flex-row mt-12">
        <div className="main-content flex-1 bg-gray-100">
          {children}
        </div>
      </div>
    </div>
  );
}

export default Main;
