import React from 'react';
import Banner from "../Layouts/Banner/Banner";
import {MdKeyboardArrowRight} from 'react-icons/md'
const ContactPage = () => {
    return (
        <div>
            <Banner bannerTitle={`contact`}/>
            <div className={`container mx-auto xl:px-30 gap-8 py-14`}>
                <div className={`grid grid-cols-2 gap-14`}>
                    <div className={`col-span-1`}>
                        <iframe className={`rounded-xl w-full`}
                                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.0964841656846!2d105.78010801533206!3d21.02882509315103!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab86cece9ac1%3A0xa9bc04e04602dd85!2zRlBUIEFwdGVjaCBIw6AgTuG7mWkgLSBI4buHIFRo4buRbmcgxJDDoG8gVOG6oW8gTOG6rXAgVHLDrG5oIFZpw6puIFF14buRYyBU4bq_IChTaW5jZSAxOTk5KQ!5e0!3m2!1svi!2s!4v1672953307432!5m2!1svi!2s"
                                height="480" style={{border: 0}} allowFullScreen="" loading="lazy"
                                referrerPolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                    <div className={`col-span-1`}>
                        <h4 className={`text-blackColor text-[26px] font-semiBold capitalize leading-[32px]`}>we would
                            love to hear from you</h4>
                        <p className={`font-light text-lightColor my-2 text-sm`}>Your email address will not be
                            published. Required fields are marked *</p>
                        <form className={`mt-4`}>
                            <div className={`grid grid-rows-1 gap-4`}>
                                <div className={`row-span-1`}>
                                    <input className={`w-full h-full text-[14px] border-[1px] rounded-full border-borderColor py-[16px] px-[26px] focus:border-blackColor`} type={'text'} name={`your-name`} placeholder={`Name *`}/>
                                </div>
                                <div className={`row-span-1`}>
                                    <input className={`w-full h-full text-[14px] border-[1px] rounded-full border-borderColor py-[16px] px-[26px] focus:border-blackColor`} type={'email'} name={`your-email`} placeholder={`Email *`}/>
                                </div>
                                <div className={`row-span-1`}>
                                    <textarea className={`m-auto w-full h-full resize-none text-[14px] border-[1px] rounded-[20px] border-borderColor overflow-hidden py-[24px] px-[26px] focus:border-blackColor`} name={`your-message`} cols={60} rows={4} placeholder={`Message`}></textarea>
                                </div>
                                <div className={`row-span-1 my-1`}>
                                    <div className={`flex items-center`}>
                                        <input type={`checkbox`} className={`mr-3`}/>
                                        <span className={`text-sm font-light`}>Save my name, email, and website in this browser for the next time I comment.</span>
                                    </div>
                                </div>
                                <div className={`row-span-1`}>
                                    <button className={`flex items-center bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 text-whiteColor py-3 px-6 rounded-full`}>Submit <MdKeyboardArrowRight className={`ml-1`}/></button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ContactPage;