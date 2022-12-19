import './App.css';
import {Outlet} from "react-router-dom";
import TopHeader from "./components/Layouts/Header/TopHeader";
import BottomHeader from "./components/Layouts/Header/BottomHeader";
import React from "react";
import Footer from "./components/Layouts/Footer/Footer";

const App = () => {
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
}

export default App;
