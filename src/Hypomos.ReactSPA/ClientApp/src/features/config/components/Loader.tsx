import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';

class Loader extends React.Component<Props> {
    render() {
        const { config } = this.props;
        
        return (
            <div className="flex flex-col h-full justify-center items-center">
                <div className="h-10 w-10">
                    <div className="spinner h-full w-full" />
                </div>
                <div className="mt-2">
                    <span className="text-gray-800 font-semibold">Loading... { config.ready }</span>
                </div>
            </div>
        );
    }
}


const mapStateToProps = (state: RootState) => (
    {
        config: state.config
    });

type Props = ReturnType<typeof mapStateToProps>

export default connect(mapStateToProps)(Loader);