import React, {Component} from 'react';
import {Link} from "react-router-dom";
import {BiPhoneCall} from "react-icons/bi";
import {HiOutlineLocationMarker} from "react-icons/hi";
import {AiFillFacebook, AiOutlineTwitter, AiOutlineInstagram} from "react-icons/ai";
import {RiPinterestFill} from "react-icons/ri"

const IconMedia = [AiFillFacebook, AiOutlineTwitter, AiOutlineInstagram, RiPinterestFill]
const TopHeader = () => {
    return (
        <nav className={`py-3 bg-background_color`}>
            <div className={`container flex flex-wrap items-center justify-between mx-auto xl:px-40`}>
                <div className={`text-xs text-whiteColor flex items-center justify-center`}>
                    <div className={`flex items-center justify-center mr-6`}>
                        <HiOutlineLocationMarker className={`text-lg text-dangerColor-default_2 mr-1`}/>
                        <Link to={`/`} className={`underline text-white font-medium`}>
                            Find a Book Store
                        </Link>
                    </div>
                    <div className={`flex items-center justify-center `}>
                        <BiPhoneCall className={`text-lg text-dangerColor-default_2 mr-1`}/>
                        <div className={`text-xs text-white font-medium`}>
                            +1 840-841 25 69
                        </div>
                    </div>
                </div>
                <div className={`text-xs text-whiteColor flex items-center justify-center `}>
                    {
                        IconMedia.map((Icon, idx) => {
                            return <Icon key={idx} size={'15px'} className={`mr-3`}/>
                        })
                    }
                </div>
            </div>
        </nav>
    );
};

export default TopHeader;