import React from 'react';
import {useNavigate} from "react-router-dom";
import TopHeader from "./Header/TopHeader";
import BottomHeader from "./Header/BottomHeader";
import Footer from "./Footer/Footer";
const NotFound = () => {
    const navigate = useNavigate();
    return (
        <div>
            <div className={`app-container`}>
                <div className={`header-container w-full`}>
                    <TopHeader/>
                    <BottomHeader/>
                </div>
                <div className={`main-container w-full`}>
                    <div className={`sidenav-container`}>

                    </div>
                    <div className={`app-content`}>
                        <main className="h-screen w-full flex flex-col justify-center items-center bg-whiteColor">
                            <div className={'relative'}>
                                <h1 className="text-9xl font-extrabold text-dangerColor-default_2 tracking-widest"><span className="sr-only">Error</span>404</h1>
                                <div className="bg-[#FF6A3D] text-white px-2 text-sm rounded rotate-12 top-12 left-20 absolute">
                                    Page Not Found
                                </div>
                            </div>
                            <div className="max-w-md text-center">
                                <p className="text-2xl font-semibold md:text-3xl text-darkColor">Sorry, we couldn't find this page.</p>
                                <p className="mt-4 mb-8 text-darkColor">t looks like nothing was found at this location. You can either go back to the last page or go to homepage.</p>
                            </div>
                            <button className="mt-5">
                                <a className="relative inline-block text-sm font-medium text-[#FF6A3D] group active:text-orange-500 focus:outline-none focus:ring">
                        <span
                            className="absolute inset-0 transition-transform translate-x-0.5 translate-y-0.5 bg-[#FF6A3D] group-hover:translate-y-0 group-hover:translate-x-0"></span>
                                    <span className="relative block px-8 py-3 bg-darkColor border border-current">
                            <div onClick={()=>navigate('/')}>Go Home</div>
                        </span>
                                </a>
                            </button>
                        </main>
                    </div>
                </div>
                <div className={`footer-container w-full`}>
                    <Footer/>
                </div>
            </div>

        </div>
    );
};

export default NotFound;