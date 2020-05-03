import React from 'react';
import { connect } from 'react-redux';

import { HypomosState } from '../reducers';
import { whoAmI } from '../actions';
import { ContentType, ClaimType } from '../models';

class MainContent extends React.Component<Props> {
    componentDidMount() {
        debugger;
        var payloadAction = this.props.whoAmIRequest();
        payloadAction.payload.then(this.props.whoAmISuccess);
        
        console.log(payloadAction);
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

type Props = {
    content: ContentType[],
    user: ClaimType[] | null,
    whoAmIRequest: typeof whoAmI.request,
    whoAmISuccess: typeof whoAmI.success
};

const mapStateToProps = (state: HypomosState) => ({
    content: state.content,
    user: state.userClaims
});

const mapDispatchToProps = {
    whoAmIRequest: whoAmI.request,
    whoAmISuccess: whoAmI.success
};

export default connect(mapStateToProps, mapDispatchToProps)(MainContent as any);