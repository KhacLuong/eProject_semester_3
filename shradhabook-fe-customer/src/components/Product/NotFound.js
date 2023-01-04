import React from 'react';
import {useNavigate} from "react-router-dom";
const NotFound = () => {
    const navigate = useNavigate();

    return (
        <div>

            <main className="h-screen w-full flex flex-col justify-center items-center bg-[#1A2238]">
                <div className={'relative'}>
                    <h1 className="text-9xl font-extrabold text-white tracking-widest"><span className="sr-only">Error</span>404</h1>
                    <div className="bg-[#FF6A3D] px-2 text-sm rounded rotate-12 top-12 left-20 absolute">
                        Page Not Found
                    </div>
                </div>
                <div className="max-w-md text-center">
                    <p className="text-2xl font-semibold md:text-3xl text-white">Sorry, we couldn't find this page.</p>
                    <p className="mt-4 mb-8 text-white ">But dont worry, you can find plenty of other things
                        on our homepage.</p>
                </div>
                <button className="mt-5">
                    <a className="relative inline-block text-sm font-medium text-[#FF6A3D] group active:text-orange-500 focus:outline-none focus:ring">
                        <span
                            className="absolute inset-0 transition-transform translate-x-0.5 translate-y-0.5 bg-[#FF6A3D] group-hover:translate-y-0 group-hover:translate-x-0"></span>
                        <span className="relative block px-8 py-3 bg-[#1A2238] border border-current">
                            <div onClick={()=>navigate('/')}>Go Home</div>
                        </span>
                    </a>
                </button>
            </main>
        </div>
    );
};

export default NotFound;