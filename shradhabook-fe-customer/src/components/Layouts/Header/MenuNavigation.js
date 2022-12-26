import React from 'react';
import {NavLink} from "react-router-dom";
import {menuItems} from "./MenuItem";

const MenuNavigation = () => {
    const defaultClass = `block py-2 pl-3 pr-4 text-blackColor rounded hover:text-dangerColor-default_2 md:p-0 ease-in duration-100 text-base font-medium `
    const renderMenuItem = () => {

    }
    return (
        <div className="items-center justify-between flex py-3">
            <ul className="flex flex-col p-4 mt-4 md:flex-row md:space-x-8 md:mt-0 md:text-sm md:border-0">
                <NavLink to={'/'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Home
                </NavLink>
                <button id="dropdownNavbarLink" data-dropdown-toggle="dropdownNavbar"
                        className="hover:text-dangerColor-default_2 block py-2 pl-3 pr-4 rounded md:bg-transparent md:p-0 text-base font-medium flex items-center justify-between">
                    Categories
                    <svg
                        className="w-4 h-4 ml-1" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd"
                              d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                              clipRule="evenodd">
                        </path>
                    </svg>
                </button>
                <div id="dropdownNavbar"
                     className="z-10 hidden font-normal bg-white divide-y divide-gray-100 rounded shadow w-44 dark:bg-gray-700 dark:divide-gray-600">
                    <ul className="py-1 text-sm text-gray-700 dark:text-gray-400"
                        aria-labelledby="dropdownLargeButton">
                        <li>
                            <a href="src/components/Layout/Header#"
                               className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 hover:text-dangerColor-default_2">Dashboard</a>
                        </li>
                        <li aria-labelledby="dropdownNavbarLink">
                            <button id="doubleDropdownButton" data-dropdown-toggle="doubleDropdown"
                                    data-dropdown-placement="right-start" type="button"
                                    className="flex items-center justify-between w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 hover:text-dangerColor-default_2">Dropdown
                                <svg aria-hidden="true" className="w-5 h-5" fill="currentColor"
                                     viewBox="0 0 20 20"
                                     xmlns="http://www.w3.org/2000/svg">
                                    <path fillRule="evenodd"
                                          d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                                          clipRule="evenodd"></path>
                                </svg>
                            </button>
                            <div id="doubleDropdown"
                                 className="z-10 hidden bg-white divide-y divide-gray-100 rounded shadow w-44 dark:bg-gray-700">
                                <ul className="py-1 text-sm text-gray-700 dark:text-gray-200"
                                    aria-labelledby="doubleDropdownButton">
                                    <li>
                                        <a href="src/components/Layout/Header#"
                                           className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 hover:text-dangerColor-default_2">Overview</a>
                                    </li>
                                    <li>
                                        <a href="src/components/Layout/Header#"
                                           className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 hover:text-dangerColor-default_2">My
                                            downloads</a>
                                    </li>
                                    <li>
                                        <a href="src/components/Layout/Header#"
                                           className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 hover:text-dangerColor-default_2">Billing</a>
                                    </li>
                                    <li>
                                        <a href="src/components/Layout/Header#"
                                           className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 hover:text-dangerColor-default_2">Rewards</a>
                                    </li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <a href="src/components/Layout/Header#"
                               className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 hover:text-dangerColor-default_2">Earnings</a>
                        </li>
                    </ul>
                </div>
                <NavLink to={'/products'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Products
                </NavLink>
                <NavLink to={'/blogs'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Blogs
                </NavLink>
                <NavLink to={'/contact'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Contact
                </NavLink>
            </ul>
        </div>
    );
};

export default MenuNavigation;