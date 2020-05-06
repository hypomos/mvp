import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';

class MainContent extends React.Component<Props> {
    renderContentList() {
        return <div>a list ...</div>;
    }

    render() {
        return (
            <div>
                <div className="bg-blue-800 p-2 shadow text-xl text-white">
                    <h3 className="font-bold pl-2">Hello World!</h3>
                </div>
                {this.renderContentList()}
            </div>
        );
    }
}

const mapStateToProps = (state: RootState) => ({
        // content: state.content,
        // user: state.userClaims
});

type Props = ReturnType<typeof mapStateToProps>

export default connect(mapStateToProps)(MainContent);