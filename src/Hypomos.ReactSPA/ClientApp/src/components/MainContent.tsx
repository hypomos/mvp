import React from 'react';
import { connect } from 'react-redux';


class MainContent extends React.Component {
    renderContentList(){
        return this.props.content.map(c => (
            <div key={c.title}>{c.title}</div>
        ));
    }

    render() {
        console.log(this.props);

        return (
            <div>
                <div className="bg-blue-800 p-2 shadow text-xl text-white">
                    <h3 className="font-bold pl-2">Hello World!</h3>
                </div>
                { this.renderContentList() }
            </div>
        );
    }
}

const mapStateToProps = (state: any) => {
    return { content: state.content };
}

export default connect(mapStateToProps)(MainContent);