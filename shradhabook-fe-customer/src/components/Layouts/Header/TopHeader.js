import React from 'react';
import {Link} from "react-router-dom";
import {GiBookAura} from "react-icons/gi"
import {FiHeart} from "react-icons/fi"
import {TbShoppingCart} from "react-icons/tb"
import {RiUserLine} from "react-icons/ri"
import Search from "./Search";

const TopHeader = () => {
    const icons = [
        {
            icon: RiUserLine,
            url: '/login',
            target: ""
        },
        {
            icon: FiHeart,
            url: '/wishlist',
            target: ""
        },
        {
            icon: TbShoppingCart,
            url: '/shopping-cart',
            target: ""
        }
    ]

    return (
        <nav className={'bg-dangerColor-default_2'}>
            <div className="container flex flex-wrap items-center justify-between mx-auto xl:px-30">
                <Link to={'/'} className="flex items-center text-white">
                    <div className={`text-3xl mr-2`}>
                        <GiBookAura/>
                    </div>
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                </Link>
                <Search/>
                <div className="items-center text-white text-xl justify-between w-full md:flex md:w-auto md:order-1">
                    {icons.map((item, key) => {
                        const Icon = item.icon
                        return icons.lastIndexOf(item) === (icons.length - 1) ?
                            <Link to={item.url} key={key}>
                                <div className={` px-3 border-borderColor`}>
                                    <Icon className={`text-white`}/>
                                </div>
                            </Link> : <Link to={item.url} target={item.target} key={key}>
                                <div key={key} className={`border-r-[1px] px-3 border-borderColor`}>
                                    <Icon className={`text-white`}/>
                                </div>
                            </Link>
                    })}
                </div>
            </div>
        </nav>);
};

export default TopHeader;