import React from 'react';
import Search from "./Search";
import {Link} from "react-router-dom";
import {HiOutlineLocationMarker} from "react-icons/hi"
import {BiCategory} from "react-icons/bi"

const BottomHeader = () => {
    return (
        <nav className={`bg-white border-gray-200  bg-dangerColor-default_2`}>
            <div className={`container flex flex-wrap items-center justify-between mx-auto xl:px-40`}>
                <div className={`px-10 sm:px-5 py-3 categories-header text-white font-semibold flex items-center justify-between text-center bg-dangerColor-default_3`}>
                    <BiCategory className={`text-xl`}/>
                    Categories
                </div>
                <Search/>
                <Link to={`/contact`}
                      className={`underline flex items-center text-white justify-between flex-row font-bold text-xs `}>
                    <HiOutlineLocationMarker className={`mr-2 text-lg text-white`}/>
                    Find a Book Store
                    <div>
                        <div>

                        </div>
                        <div>

                        </div>
                    </div>
                </Link>
            </div>

        </nav>
    );
};

export default BottomHeader;