import React from 'react';
import {Link} from "react-router-dom";
import {AiOutlineUser, AiOutlineHeart, AiOutlineShoppingCart} from "react-icons/ai";
import {GiBookAura} from "react-icons/gi"
import Search from "./Search";

const TopHeader = () => {
    const icons = [
        {
            icon: AiOutlineUser,
            url: '/login'
        },
        {
            icon: AiOutlineHeart,
            url: '/wishlist'
        },
        {
            icon: AiOutlineShoppingCart,
            url: '/shopping-cart'
        }
    ]

    return (<nav className={'bg-dangerColor-default_2'}>
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
                        </Link> : <Link to={item.url} key={key}>
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