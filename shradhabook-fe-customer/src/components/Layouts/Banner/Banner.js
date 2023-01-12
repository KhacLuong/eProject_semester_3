import React from 'react';
import {useNavigate} from "react-router-dom";
import {HiOutlineArrowNarrowRight} from 'react-icons/hi'

const Banner = (props) => {
    const {bannerTitle} = props
    const navigate = useNavigate();

    return (
        <div className={`banner_${bannerTitle} w-full h-44 bg-[#f0f0f0] flex items-center`}>
            <div
                className={`container flex justify-between mx-auto xl:px-30`}>
                <div className={`text-6xl capitalize  font-bold text-black leading-tight`}>{bannerTitle}</div>
                <div
                    className={`text-dangerColor-default_2 uppercase text-xs flex items-center justify-between leading-relaxed font-medium`}>
                    <div onClick={()=>navigate('/')} className={`hover:text-black text-lightColor duration-300 cursor-pointer`}>home</div>
                    <HiOutlineArrowNarrowRight className={`text-lightColor mx-4`}/>
                    {bannerTitle}
                </div>
            </div>
        </div>

    );
};

export default Banner;