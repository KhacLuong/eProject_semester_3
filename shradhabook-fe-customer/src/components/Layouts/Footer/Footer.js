import React from 'react';
import {GiBookAura} from "react-icons/gi";
import {AiFillFacebook, AiOutlineTwitter, AiOutlineInstagram} from "react-icons/ai";
import {RiPinterestFill} from "react-icons/ri"
import image from '../../../assets/image/footer_img.png'
import {Link, useNavigate} from "react-router-dom";

const Footer = () => {

    const mediaIcons = [AiFillFacebook, AiOutlineTwitter, AiOutlineInstagram, RiPinterestFill]
    const companyItems = ['About us', 'Delivery Information', 'Privacy Policy', 'Terms & Conditions', 'Contact Us', 'Support Center']
    const corporateItems = ['Become a Vendor', 'Affiliate Program', 'Our Suppliers', 'Accessibility', 'Promotions', 'Careers']
    const ourServiceItems = ['Help Center', 'Returns', 'Product', 'Recalls', 'Accessibility', 'Contact Us', 'Store Pickup']
    const textClassFooter = 'text-sm font-light hover:text-dangerColor-hover_2 cursor-pointer'
    const navigate = useNavigate();

    return (
        <div className={`bg-footerBgColor`}>
            <div className={`container border-solid border-lightColor py-20 mx-auto xl:px-30 text-lightColor`}>
                <div className={`flex items-start justify-between`}>
                    <div className={`h-[100px] mr-20 grid grid-row-4 gap-5`}>
                        <div className={``}>
                            <h2 className={`text-base text-white font-semibold capitalize`}>Contact Info</h2>
                        </div>
                        <div className={`text-lg`}>
                            <p className={textClassFooter}>FPT Aptech, Ha Noi, Viet Nam <br/>8A, Ton That Thuyet, My
                                Dinh
                            </p>
                        </div>
                        <div>
                            <p className={textClassFooter}>Monday - Friday: 9:00-20:00 <br/>Saturday: 11:00-15:00
                            </p>
                        </div>
                        <div className={`text-[18px] leading-6 text-white`}>
                            contact@example.com
                        </div>
                    </div>
                    <div className={`mr-20 grid gap-5`}>
                        <div className={``}>
                            <h2 className={`text-base text-white font-semibold capitalize`}>Company</h2>
                        </div>
                        <div className={`grid gap-2`}>
                            {companyItems.map((item, key) => {
                                return <p className={textClassFooter} key={key}>{item}</p>
                            })}
                        </div>
                    </div>
                    <div className={`h-56 px-10 mr-20 border-x-[1px] border-solid border-lightColor`}>
                        <div className={`text-center grid gap-6 `}>
                            <div className={`flex items-center justify-center text-dangerColor-default_2 flex`}>
                                <div className={`text-3xl mr-2`}>
                                    <GiBookAura/>
                                </div>
                                <span
                                    className="text-white self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                            </div>
                            <div>
                                <p>Got Questions ? Call us 24/7!</p>
                            </div>
                            <div>
                                <h2 className={`text-dangerColor-default_2 font-bold text-3xl leading-8 tracking-[1px]`}>+(84)
                                    - 1800 - 4635</h2>
                            </div>
                            <div className={`text-xs text-lightColor flex items-center justify-center `}>
                                {
                                    mediaIcons.map((Icon, idx) => {
                                        return <Icon key={idx} size={'20px'} className={`mr-3 hover:text-dangerColor-hover_2 cursor-pointer`}/>
                                    })
                                }
                            </div>
                        </div>
                    </div>
                    <div className={`mr-20 grid gap-5`}>
                        <div className={``}>
                            <h2 className={`text-base text-white font-semibold capitalize`}>Corporate</h2>
                        </div>
                        <div className={`grid gap-2`}>
                            {corporateItems.map((item, key) => {
                                return <p className={textClassFooter} key={key}>{item}</p>
                            })}
                        </div>
                    </div>
                    <div className={`grid gap-5`}>
                        <div className={``}>
                            <h2 className={`text-base text-white font-semibold capitalize`}>Our Service</h2>
                        </div>
                        <div className={`grid gap-2`}>
                            {ourServiceItems.map((item, key) => {
                                return <p className={textClassFooter} key={key}>{item}</p>
                            })}
                        </div>
                    </div>
                </div>
            </div>
            <hr/>
            <div className={`py-8 w-100 container mx-auto xl:px-30`}>
                <div className={`flex items-center justify-between`}>
                    <div className={`text-white text-sm flex-row flex`}>
                        Copyright © 2022
                        <div onClick={()=>navigate('/')} className={`text-dangerColor-default_2 mx-1`}> ShradhaBook.</div>
                        All rights reserved.
                    </div>
                    <div>
                        <img src={image} alt={`card`}/>
                    </div>
                </div>
            </div>
        </div>);
};

export default Footer;