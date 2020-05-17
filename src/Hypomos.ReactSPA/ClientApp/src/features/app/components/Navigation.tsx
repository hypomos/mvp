import React from 'react';
import { connect } from 'react-redux';
import { NavLink } from 'react-router-dom';

import { getPath } from '../../../router-paths';

class Navigation extends React.Component {
    render() {
        const activeNavLinkClasses = "border-blue-600";
        const inactiveNavLinkClasses = "block py-1 md:py-3 pl-1 align-middle text-white no-underline hover:text-white border-b-2 border-gray-800 hover:border-blue-500";

        return (
            <div className="shadow-lg h-16 fixed bottom-0 mt-12 md:relative md:h-screen z-10 w-full md:w-48">
                <div className="md:mt-12 md:w-48 md:fixed md:left-0 md:top-0 content-center md:content-start text-left justify-between">
                    <ul className="list-reset flex flex-row md:flex-col py-0 md:py-3 px-1 md:px-2 text-center md:text-left">
                        <li className="mr-3 flex-1">
                            <NavLink exact to={getPath('app')} className={inactiveNavLinkClasses} activeClassName={activeNavLinkClasses}>
                                <i className="fas fa-home pr-0 md:pr-3"></i>
                                <span className="pb-1 md:pb-0 text-xs md:text-base text-gray-600 md:text-gray-400 block md:inline-block">
                                    Home
                                </span>
                            </NavLink>
                        </li>
                        <li className="mr-3 flex-1">
                            <NavLink to={getPath('items')} className={inactiveNavLinkClasses} activeClassName={activeNavLinkClasses}>
                                <i className="fas fa-photo-video pr-0 md:pr-3"></i>
                                <span className="pb-1 md:pb-0 text-xs md:text-base text-gray-600 md:text-gray-400 block md:inline-block">
                                    Items
                                </span>
                            </NavLink>
                        </li>
                        <li className="mr-3 flex-1">
                            <NavLink to={getPath('collections')} className={inactiveNavLinkClasses} activeClassName={activeNavLinkClasses}>
                                <i className="fas fa-book-open pr-0 md:pr-3"></i>
                                <span className="pb-1 md:pb-0 text-xs md:text-base text-white md:text-white block md:inline-block">
                                    Collections
                                </span>
                            </NavLink>
                        </li>
                        <li className="mr-3 flex-1">
                            <NavLink to={getPath('cleaning')} className={inactiveNavLinkClasses} activeClassName={activeNavLinkClasses}>
                                <i className="fa fa-recycle pr-0 md:pr-3"></i>
                                <span className="pb-1 md:pb-0 text-xs md:text-base text-gray-600 md:text-gray-400 block md:inline-block">
                                    Cleaning
                                </span>
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        );
    }
}

export default connect()(Navigation);