import React from 'react';
import { connect } from 'react-redux';

import { RootState } from 'MyTypes';
import { whoAmI } from './actions';
import { bindActionCreators } from 'redux';

class MainContent extends React.Component<Props> {
    componentDidMount() {
        // this.props.whoAmIRequest()
        // .payload
        // .then(this.props.whoAmISuccess);
    }

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

const mapDispatchToProps = (dispatch: any) =>
    bindActionCreators(
        {
            whoAmIRequest: whoAmI.request,
            whoAmISuccess: whoAmI.success
        },
        dispatch
    );

type Props = ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>

export default connect(mapStateToProps, mapDispatchToProps)(MainContent);