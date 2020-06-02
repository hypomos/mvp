import React from 'react';

const Loader = () =>
(
    <div className="flex flex-col h-full justify-center items-center">
        <div className="h-10 w-10">
            <div className="spinner h-full w-full" />
        </div>
        <div className="mt-2">
            <span className="text-gray-800 font-semibold">Loading...</span>
        </div>
    </div>
);

export default Loader;