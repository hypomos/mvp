import React from 'react';
import Header from './components/Header';
import Navigation from './components/Navigation';
import MainContent from './components/MainContent';

function App() {
  return (
    <div className="App bg-gray-900 font-sans leading-normal tracking-normal" >
      <Header />
      <div className="flex flex-col md:flex-row">
        <Navigation />

        <div className="main-content flex-1 bg-gray-100 mt-12 md:mt-2 pb-24 md:pb-5">
          <MainContent />
        </div>
      </div>
    </div>
  );
}

export default App;
