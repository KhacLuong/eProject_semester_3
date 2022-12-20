import React from 'react';
import {Link} from "react-router-dom";
import {AiOutlineUser, AiOutlineHeart, AiOutlineShoppingCart} from "react-icons/ai";
import {GiBookAura} from "react-icons/gi"
import Search from "./Search";

const icons = [AiOutlineUser, AiOutlineHeart, AiOutlineShoppingCart]
const TopHeader = () => {
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
                {icons.map((Icon, key) => {
                    return icons.lastIndexOf(Icon) === (icons.length - 1) ?
                        <div key={key} className={` px-3 border-borderColor`}>
                            <Icon className={`text-white`}/>
                        </div> : <div key={key} className={`border-r-[1px] px-3 border-borderColor`}>
                            <Icon className={`text-white`}/>
                        </div>
                })}
            </div>
        </div>
    </nav>);
};

export default TopHeader;