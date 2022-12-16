import React from 'react';
import {Link} from "react-router-dom";
import {BiPhoneCall} from "react-icons/bi";
import {AiOutlineUser, AiOutlineHeart, AiOutlineShoppingCart} from "react-icons/ai";
import {GiBookAura} from "react-icons/gi"
import MenuNavigation from "./MenuNavigation";
import Search from "./Search";

const MiddleHeader = () => {
    return (
        <nav className={'bg-white py-5'}>
            <div className="container flex flex-wrap items-center justify-between mx-auto xl:px-40">
                <Link to={'/'} className="flex items-center">
                    <div className={`text-3xl text-dangerColor-default_2 mr-2`}>
                        <GiBookAura/>
                    </div>
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                </Link>
                <MenuNavigation/>
                <div className="items-center text-xl justify-between w-full md:flex md:w-auto md:order-1">
                    <div className={`flex items-center justify-between mr-3`}>
                        <div className={`mr-1 rounded-full bg-bgWhiteColor p-2.5`}>
                            <BiPhoneCall className={``}/>
                        </div>
                        <div>
                            <div className={`text-lg text-dangerColor-default_2 font-semibold`}>
                                +1  840-841 25 69
                            </div>
                            <div className={`text-xs text-darkColor`}>
                                24/7 Support Center
                            </div>
                        </div>
                    </div>
                    <div className={`border-r-2 px-3 border-borderColor`}>
                        <AiOutlineUser/>
                    </div>
                    <div className={`border-r-2 px-3 border-borderColor`}>
                        <AiOutlineHeart/>
                    </div>
                    <div className={`px-3`}>
                        <AiOutlineShoppingCart/>
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
                                <a href="src/components/Layouts/Header#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Dashboard</a>
                            </li>
                            <li>
                                <a href="src/components/Layouts/Header#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Settings</a>
                            </li>
                            <li>
                                <a href="src/components/Layouts/Header#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Earnings</a>
                            </li>
                            <li>
                                <a href="src/components/Layouts/Header#"
                                   className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Sign
                                    out</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default MiddleHeader;