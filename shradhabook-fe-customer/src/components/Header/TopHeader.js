import React from 'react';
import {Link} from "react-router-dom";

const TopHeader = () => {
    return (
        <nav className={`py-3 bg-background_color`}>
            <div className={`container flex flex-wrap items-center justify-between mx-auto xl:px-10`}>
                <div className={`text-xs text-whiteColor`}>
                    By one, get one 50% off books for all ages.
                    <Link to={`/`} className={`underline ml-4`}>
                        See All Offers
                    </Link>
                </div>
                <div className={`text-xs text-whiteColor`}>icon</div>
            </div>
        </nav>
    );
};

export default TopHeader;