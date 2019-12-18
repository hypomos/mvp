import React from "react";
import ReactDOM from "react-dom";
import './styles.css';

const Index = () => {
    return <div id="plex" className="flex flex-col absolute inset-0">
        <div id="nav-bar" className="flex justify-between p-2 h-16 bg-gray-800 text-gray-200">
            <div className="flex items-center">
                <button className="w-12 h-12 group hover:bg-gray-700">
                    <svg className="m-auto svg-defaults" >
                        <path d="M4 5h16a1 1 0 010 2H4a1 1 0 110-2zm0 6h16a1 1 0 010 2H4a1 1 0 010-2zm0 6h16a1 1 0 010 2H4a1 1 0 010-2z" />
                    </svg>
                </button>

                <h1 className="tracking-wider font-semibold">Hypomos</h1>

                <div className="flex w-88 mx-8 my-1 bg-gray-700 rounded-lg font-sans focus-within:bg-gray-200 focus-within:text-gray-700">
                    <svg className="w-5 h-5 m-2 fill-current stroke-current">
                        <path d="M16.32 14.9l5.39 5.4a1 1 0 01-1.42 1.4l-5.38-5.38a8 8 0 111.41-1.41zM10 16a6 6 0 100-12 6 6 0 000 12z" />
                    </svg>
                    <input className="bg-transparent flex-1 m-1 outline-none"></input>
                </div>
            </div>

            <div className="flex items-center">
                <button className="w-12 h-12 group hover:bg-gray-700">
                    <svg className="m-auto svg-defaults" >
                        <path className="heroicon-ui" d="M15 19a3 3 0 0 1-6 0H4a1 1 0 0 1 0-2h1v-6a7 7 0 0 1 4.02-6.34 3 3 0 0 1 5.96 0A7 7 0 0 1 19 11v6h1a1 1 0 0 1 0 2h-5zm-4 0a1 1 0 0 0 2 0h-2zm0-12.9A5 5 0 0 0 7 11v6h10v-6a5 5 0 0 0-4-4.9V5a1 1 0 0 0-2 0v1.1z" />
                    </svg>
                </button>

                <button className="w-12 h-12 group hover:bg-gray-700">
                    <svg className="m-auto svg-defaults" >
                        <path d="M9 4.58V4c0-1.1.9-2 2-2h2a2 2 0 012 2v.58a8 8 0 011.92 1.11l.5-.29a2 2 0 012.74.73l1 1.74a2 2 0 01-.73 2.73l-.5.29a8.06 8.06 0 010 2.22l.5.3a2 2 0 01.73 2.72l-1 1.74a2 2 0 01-2.73.73l-.5-.3A8 8 0 0115 19.43V20a2 2 0 01-2 2h-2a2 2 0 01-2-2v-.58a8 8 0 01-1.92-1.11l-.5.29a2 2 0 01-2.74-.73l-1-1.74a2 2 0 01.73-2.73l.5-.29a8.06 8.06 0 010-2.22l-.5-.3a2 2 0 01-.73-2.72l1-1.74a2 2 0 012.73-.73l.5.3A8 8 0 019 4.57zM7.88 7.64l-.54.51-1.77-1.02-1 1.74 1.76 1.01-.17.73a6.02 6.02 0 000 2.78l.17.73-1.76 1.01 1 1.74 1.77-1.02.54.51a6 6 0 002.4 1.4l.72.2V20h2v-2.04l.71-.2a6 6 0 002.41-1.4l.54-.51 1.77 1.02 1-1.74-1.76-1.01.17-.73a6.02 6.02 0 000-2.78l-.17-.73 1.76-1.01-1-1.74-1.77 1.02-.54-.51a6 6 0 00-2.4-1.4l-.72-.2V4h-2v2.04l-.71.2a6 6 0 00-2.41 1.4zM12 16a4 4 0 110-8 4 4 0 010 8zm0-2a2 2 0 100-4 2 2 0 000 4z" />
                    </svg>
                </button>

                <button className="h-12 px-2 w-auto group hover:bg-gray-700 text-left">
                    <svg className="inline mr-2 svg-defaults" >
                        <path d="M12 12a5 5 0 110-10 5 5 0 010 10zm0-2a3 3 0 100-6 3 3 0 000 6zm9 11a1 1 0 01-2 0v-2a3 3 0 00-3-3H8a3 3 0 00-3 3v2a1 1 0 01-2 0v-2a5 5 0 015-5h8a5 5 0 015 5v2z" />
                    </svg>
                    <span>Beat Walti</span>
                </button>
            </div>
        </div>

        <div id="content" className="flex-grow relative overflow-x-hidden overflow-y-auto">
            <div className="flex flex-row absolute inset-0 overflow-hidden bg-gray-800 text-gray-300">
                <div className="flex flex-col">
                    <div className="flex-grow w-64 overflow-y-auto dark-scrollbar" >
                        <nav>
                            <div className="flex h-12 selected group hover:bg-gray-700 hover:text-gray-100">
                                <a className="flex flex-grow flex-shrink pl-4 items-center">
                                    <div className="">
                                        <svg className="svg-defaults inline mr-2">
                                            <path class="heroicon-ui" d="M4 4h16a2 2 0 012 2v12a2 2 0 01-2 2H4a2 2 0 01-2-2V6c0-1.1.9-2 2-2zm16 8.59V6H4v6.59l4.3-4.3a1 1 0 011.4 0l5.3 5.3 2.3-2.3a1 1 0 011.4 0l1.3 1.3zm0 2.82l-2-2-2.3 2.3a1 1 0 01-1.4 0L9 10.4l-5 5V18h16v-2.59zM15 10a1 1 0 110-2 1 1 0 010 2z" />
                                        </svg>
                                    </div>
                                    <div className="pl-2">
                                        <span>Fotos</span>
                                    </div>
                                </a>
                            </div>
                            <div className="h-12">Alben</div>
                            <div className="h-12">Cleaning</div>
                        </nav>
                    </div>
                </div>

                <div className="flex-1 main-content-background text-gray-100">
                    Main Content
                </div>
            </div>
        </div>
    </div>;
};

ReactDOM.render(<Index />, document.getElementById("index"));