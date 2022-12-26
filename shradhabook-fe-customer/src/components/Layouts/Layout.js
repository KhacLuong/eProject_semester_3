import React from 'react';
import TopHeader from "./Header/TopHeader";
import BottomHeader from "./Header/BottomHeader";
import {Outlet} from "react-router-dom";
import Footer from "./Footer/Footer";

const Layout = () => {
    return (
        <div className={`app-container`}>
            <div className={`header-container w-full`}>
                <TopHeader/>
                <BottomHeader/>
            </div>
            <div className={`main-container w-full`}>
                <div className={`sidenav-container`}>

                </div>
                <div className={`app-content`}>
                    <Outlet/>
                </div>
            </div>
            <div className={`footer-container w-full`}>
                <Footer/>
            </div>
        </div>
    );
};

export default Layout;