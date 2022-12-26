import React from 'react';
import {useNavigate} from "react-router-dom";
import {GiBookAura} from "react-icons/gi"
import {FiHeart} from "react-icons/fi"
import {TbShoppingCart} from "react-icons/tb"
import {RiUserLine} from "react-icons/ri"
import Search from "./Search";
import {useSelector} from "react-redux";

const TopHeader = () => {
    const isAuthenticated = useSelector(state => state.user.isAuthenticated);
    const account = useSelector(state => state.user.account);
    const navigate = useNavigate();
    const icons = [
        {
            icon: RiUserLine,
            url: '/login',
            target: ""
        },
        {
            icon: FiHeart,
            url: '/wishlist/:id',
            target: ""
        },
        {
            icon: TbShoppingCart,
            url: '/shopping-cart/:id',
            target: ""
        }
    ]
    return (
        <nav className={'bg-dangerColor-default_2'}>
            <div className="container flex flex-wrap items-center justify-between mx-auto xl:px-30">
                <div onClick={(e) => navigate('/')} className="cursor-pointer flex items-center text-white">
                    <div className={`text-3xl mr-2`}>
                        <GiBookAura/>
                    </div>
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                </div>
                <Search/>
                <div className="items-center text-white text-xl justify-between w-full md:flex md:w-auto md:order-1">
                    {isAuthenticated === false
                        ?
                        <>
                            {icons.map((item, key) => {
                                const Icon = item.icon
                                return icons.lastIndexOf(item) === (icons.length - 1) ?
                                    <div onClick={() => navigate(item.url)} key={key}
                                         className={`text-white hover:text-blackColor cursor-pointer px-3 border-borderColor`}>
                                        <Icon className={`hover:text-blackColor`}/>
                                    </div>
                                    :
                                    <div onClick={() => navigate(item.url)} key={key}
                                         className={`text-white cursor-pointer border-r-[1px] px-3 border-borderColor`}>
                                        <Icon className={`hover:text-blackColor`}/>
                                    </div>
                            })}
                        </>
                        :
                        <>
                            <button id="dropdownInformationButton" data-dropdown-toggle="dropdownInformation"
                                    className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2.5 text-center inline-flex items-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
                                    type="button">Dropdown header
                            </button>
                            <div id="dropdownInformation"
                                 className="hidden z-10 w-44 bg-white rounded divide-y divide-gray-100 shadow dark:bg-gray-700 dark:divide-gray-600">
                                <div className="py-3 px-4 text-sm text-gray-900 dark:text-white">
                                    <div>Bonnie Green</div>
                                    <div className="font-medium truncate">name@flowbite.com</div>
                                </div>
                                <ul className="py-1 text-sm text-gray-700 dark:text-gray-200"
                                    aria-labelledby="dropdownInformationButton">
                                    <li>
                                        <div onClick={()=> navigate('/my-info')} className="cursor-pointer block py-2 px-4 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
                                            Profile
                                        </div>
                                    </li>
                                </ul>
                                <div className="py-1">
                                    <a href="#"
                                       className="block py-2 px-4 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Sign
                                        out</a>
                                </div>
                            </div>
                        </>
                    }
                </div>
            </div>
        </nav>
    );
}

export default TopHeader;