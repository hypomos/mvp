import React from 'react';
import { NavLink } from 'react-router-dom';

import { getPath } from '../router-paths';

type Props = {
    // something like: renderActionsMenu?: () => JSX.Element
}

class Header extends React.Component<Props> {
    renderChildren() {
        const { children } = this.props;

        if (!children)
            return (null);

        const childrenCount = React.Children.count(children);

        return React.Children.map(children, (c, i) => {
            const marginRight = (i + 1 == childrenCount) ? '' : 'md:mr-3';
            const classes = `flex-1 md:flex-none ${marginRight}`;

            return (
                <li className={classes}>
                    <div className="relative inline-block">
                        {c}
                    </div>
                </li>
            )
        }
        );
    }

    render() {
        return (
            <nav className="w-full h-auto bg-gray-900 py-2 md:py-1 px-1 mt-0 z-20">
                <div className="flex flex-wrap items-center">
                    <div className="flex flex-shrink md:w-1/3 justify-center md:justify-start text-white">
                        <NavLink to={getPath('home')}>
                            <span className="text-xl pl-2">Hypomos</span>
                        </NavLink>
                    </div>

                    <div className="flex flex-1 md:w-1/3 justify-center md:justify-start text-white px-2">
                        {
                        // SEARCH BOX:
                    /* <span className="relative w-full">
                        <input type="search" placeholder="Search" className="w-full bg-gray-800 text-sm text-white transition border border-transparent focus:outline-none focus:border-gray-700 rounded py-1 px-2 pl-10 appearance-none leading-normal"></input>
                        <div className="absolute search-icon" style={{'top': '.5rem', 'left': '.8rem'}}>
                            <svg className="fill-current pointer-events-none text-white w-4 h-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                                <path d="M12.9 14.32a8 8 0 1 1 1.41-1.41l5.35 5.33-1.42 1.42-5.33-5.34zM8 14A6 6 0 1 0 8 2a6 6 0 0 0 0 12z"></path>
                            </svg>
                        </div>
                    </span> */}
                    </div>

                    <div className="flex w-full md:w-1/3 md:justify-end">
                        <ul className="list-reset flex justify-between content-center flex-1 md:flex-none items-center">
                            {
                            // SECONDARY NAV
                        /* <li className="flex-1 md:flex-none md:mr-3">
                            <a className="inline-block py-2 px-4 text-white no-underline" href="#">Active</a>
                        </li>
                        <li className="flex-1 md:flex-none md:mr-3">
                            <a className="inline-block text-gray-600 no-underline hover:text-gray-200 hover:text-underline py-2 px-4" href="/app">App</a>
                        </li> */}

                            {/* <li className="flex-1 md:flex-none md:mr-3">
                                <div className="relative inline-block">
                                    <button className="drop-button text-white focus:outline-none">
                                        <span className="pr-2"><i className="fas fa-bell"></i></span>
                                    </button>
                                    <div id="myDropdown" className="dropdownlist absolute bg-gray-900 text-white right-0 mt-3 p-3 overflow-auto z-30 invisible">
                                        <input type="text" className="drop-search p-2 text-gray-600" placeholder="Search.." id="myInput"></input>
                                        <a href="#" className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block"><i className="fa fa-user fa-fw"></i> Profile</a>
                                        <a href="#" className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block"><i className="fa fa-cog fa-fw"></i> Settings</a>
                                        <div className="border border-gray-800"></div>
                                        <a href="#" className="p-2 hover:bg-gray-800 text-white text-sm no-underline hover:no-underline block"><i className="fas fa-sign-out-alt fa-fw"></i> Log Out</a>
                                    </div>
                                </div>
                            </li> */}

                            {this.renderChildren()}

                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}

export default Header;