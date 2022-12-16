import './App.css';
import {Outlet} from "react-router-dom";
import TopHeader from "./components/Layouts/Header/TopHeader";
import MiddleHeader from "./components/Layouts/Header/MiddleHeader";
import BottomHeader from "./components/Layouts/Header/BottomHeader";
import React from "react";

const App = () => {
    return (
        <div className={`app-container`}>
            <div className={`header-container fixed w-full`}>
                <TopHeader />
                <MiddleHeader />
                <BottomHeader />
            </div>
            <div className={`main-container`}>
                <div className={`sidenav-container`}>

                </div>
                <div className={`app-content`}>
                    <Outlet />
                </div>
            </div>
        </div>
    );
}

export default App;
