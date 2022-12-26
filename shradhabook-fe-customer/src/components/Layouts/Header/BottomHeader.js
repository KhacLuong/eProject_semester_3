import React from 'react';
import {Link, useNavigate} from "react-router-dom";
import {HiOutlineLocationMarker} from "react-icons/hi"
import MenuNavigation from "./MenuNavigation";
import {BiPhoneCall} from "react-icons/bi";

const BottomHeader = () => {
    const navigate = useNavigate();
    return (
        <nav className={`bg-white border-gray-200 bg-white border-b-[2px] border-dark `}>
            <div className={`container flex items-center justify-between mx-auto xl:px-30`}>
                <div className={`text-sm flex items-center justify-center mr-6`}>
                    <HiOutlineLocationMarker className={` text-black mr-1`}/>
                    <div onClick={()=>navigate('/')} className={`underline text-black font-medium cursor-pointer`}>Find a Book Store</div>
                </div>
                    <MenuNavigation/>
                <div className={`flex items-center justify-between mr-3`}>
                    <div className={`mr-1 rounded-full bg-bgWhiteColor p-2.5`}>
                        <BiPhoneCall className={``}/>
                    </div>
                    <div>
                        <div className={`text-base text-dangerColor-default_2 font-semibold`}>
                            +1  840-841 25 69
                        </div>
                        <div className={`text-xs text-darkColor`}>
                            24/7 Support Center
                        </div>
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default BottomHeader;