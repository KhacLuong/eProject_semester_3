import React from 'react';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {NavLink, Link} from "react-router-dom";
import * as Icon from 'react-bootstrap-icons';

const defaultClass = `block py-2 pl-3 pr-4 text-blackColor rounded hover:text-dangerColor-default_2 md:p-0 ease-in duration-100 text-base font-medium`

const MiddleHeader = () => {
    return (
        <nav className={'bg-white py-5'}>
            <div className="container flex flex-wrap items-center justify-between mx-auto xl:px-10">
                <Link to={'/'} className="flex items-center">
                    <div className={`text-3xl text-dangerColor-default_2 mr-2`}>
                        <Icon.Book/>
                    </div>
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                </Link>
                <div className=" items-center justify-between w-full md:flex md:w-auto md:order-1">
                    <ul className="flex flex-col p-4 mt-4 md:flex-row md:space-x-8 md:mt-0 md:text-sm md:border-0">
                        <NavLink to={'/'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Home
                        </NavLink>
                        <NavLink to={'/categories'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Category
                        </NavLink>
                        <NavLink to={'/products'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Product
                        </NavLink>
                        <NavLink to={'/blog'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Blog
                        </NavLink>
                        <NavLink to={'/contact'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Contact
                        </NavLink>
                        <NavLink to={'/pages'}
                                 className={({isActive}) => isActive ? 'active' : defaultClass}>
                            Pages
                        </NavLink>
                    </ul>
                </div>
                <div className="items-center text-lg justify-between w-full md:flex md:w-auto md:order-1">
                    <div>
                        <Icon.Telephone/>
                    </div>
                    <div>
                        <FontAwesomeIcon icon="fa-regular fa-user"/>
                    </div>
                    <div>
                        <FontAwesomeIcon icon="fa-regular fa-heart"/>
                    </div>
                    <div>
                        <Icon.Basket3/>
                    </div>
                    <div
                        className="z-50 hidden my-4 text-base list-none bg-white divide-y divide-gray-100 rounded shadow dark:bg-gray-700 dark:divide-gray-600"
                        id="user-dropdown">
                        <div className="px-4 py-3">
                            <span className="block text-sm text-gray-900 dark:text-white">Bonnie Green</span>
                            <span
                                className="block text-sm font-medium text-gray-500 truncate dark:text-gray-400">name@flowbite.com</span>
                        </div>
                        <ul className="py-1" aria-labelledby="user-menu-button">
                            <li>
                                <a href="#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Dashboard</a>
                            </li>
                            <li>
                                <a href="#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Settings</a>
                            </li>
                            <li>
                                <a href="#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Earnings</a>
                            </li>
                            <li>
                                <a href="#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Sign
                                    out</a>
                            </li>
                        </ul>
                    </div>
                    <button data-collapse-toggle="mobile-menu-2" type="button"
                            className="inline-flex items-center p-2 ml-1 text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
                            aria-controls="mobile-menu-2" aria-expanded="false">
                        <span className="sr-only">Open main menu</span>
                        <svg className="w-6 h-6" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20"
                             xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd"
                                  d="M3 5a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 10a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 15a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1z"
                                  clip-rule="evenodd"></path>
                        </svg>
                    </button>
                </div>
            </div>
        </nav>
    );
};

export default MiddleHeader;