import React from 'react';
import { connect } from 'react-redux';

import { HypomosState } from '../reducers';
import { whoAmI } from '../actions';
import { bindActionCreators } from 'redux';

class MainContent extends React.Component<Props> {
    componentDidMount() {
        this.props.whoAmIRequest()
        .payload
        .then(this.props.whoAmISuccess);

        // console.log(payloadAction);
    }

    renderContentList() {
        return this.props.content.map(c => (
            <div key={c.title}>{c.title}</div>
        ));
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

const mapStateToProps = (state: HypomosState) => ({
        content: state.content,
        user: state.userClaims
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