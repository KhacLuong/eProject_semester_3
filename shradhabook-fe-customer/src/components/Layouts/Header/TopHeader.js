import React, {useEffect, useRef, useState} from 'react';
import {useNavigate} from "react-router-dom";
import {GiBookAura} from "react-icons/gi"
import {FiHeart} from "react-icons/fi"
import {TbShoppingCart} from "react-icons/tb"
import {RiUserLine} from "react-icons/ri"
import Search from "./Search";
import {useDispatch, useSelector} from "react-redux";
import {deleteLogout, postRefreshToken} from "../../../services/apiService";
import {toast} from "react-toastify";
import {doLogout} from "../../../redux/action/userAction";
import jwt_decode from "jwt-decode";

const TopHeader = () => {
    const isAuthenticated = useSelector(state => state.user.isAuthenticated);
    const account = useSelector(state => state.user.account);
    const navigate = useNavigate();
    const [showNavUser, setShowNavUser] = useState(false);
    const dispatch = useDispatch();
    let ref = useRef();

    let decoded = ''
    if (account.accessToken) {
        decoded = jwt_decode(account.accessToken);
    }

    useEffect(() => {
        refreshToken()
    }, [])

    useEffect(() => {
        const handler = (event) => {
            if (showNavUser && ref.current && !ref.current.contains(event.target)) {
                setShowNavUser(false)
            }
        };
        document.addEventListener("mousedown", handler);
        document.addEventListener("touchstart", handler);
        return () => {
            // cleanup the event listener
            document.removeEventListener("mousedown", handler);
            document.removeEventListener("touchstart", handler);
        }
    }, [showNavUser])
    const refreshToken = async () => {
        const refreshToken = account.refreshToken
        let res = await postRefreshToken(refreshToken);
        console.log(res)
    }
    const handleClickUser = () => {
        setShowNavUser(!showNavUser)
    }
    const handLogout = async () => {
        // setIsLoadingData(true);
        let res = await deleteLogout(account.accessToken);

        if (res.status === true) {
            dispatch(doLogout());
            toast.success(res.message);
            navigate('/')
        }
    }
    const handleLeaveNavUser = () => {
        window.innerWidth > 2060 && setShowNavUser(false);
    }

    return (
        <nav className={'bg-dangerColor-default_2'}>
            <div className="container flex flex-wrap items-center justify-between mx-auto xl:px-30">
                <div onClick={(e) => navigate('/')} className="cursor-pointer flex items-center text-white">
                    <div className={`text-3xl mr-2`}>
                        <GiBookAura/>
                    </div>
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">ShradhaBook</span>
                </div>
                <Search/>
                <div className="items-center text-white text-xl justify-between w-full md:flex md:w-auto md:order-1">
                    {isAuthenticated === false
                        ?
                        <>
                            <button
                                className="text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-sm px-4 py-2.5 text-center inline-flex items-center"
                                type="button" onClick={() => navigate('login')}>Login
                            </button>
                        </>
                        :
                        <>
                            <div ref={ref}
                                className="relative cursor-pointer text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-lg px-4 py-2.5 text-center inline-flex items-center mr-1"
                                onClick={() => handleClickUser()}
                                onMouseLeave={() => handleLeaveNavUser()}>
                                <RiUserLine></RiUserLine>
                                <div
                                    className={`${showNavUser ? 'block' : 'hidden'} rounded-[10px] absolute z-14 left-0 top-[50px] w-44 bg-white rounded divide-y divide-gray-100 shadow dark:bg-gray-700 dark:divide-gray-600 before:absolute before:content[''] before:w-[10px] before:h-[10px] before:bg-whiteColor before:top-[-5px] before:left-[13%] before:rotate-45 before:z-99999 before:border-[#e4e4e4]-[1px] before:border-b-0 before:border-r-0`}>
                                    <div className="py-3 px-4 text-sm text-gray-900 dark:text-white">
                                        <div onClick={() => navigate(`/user/my-profile/${decoded.Id}`)}
                                             className="font-medium truncate">{account.username}</div>
                                        <div className={`font-light`}>({account.email})</div>
                                    </div>
                                    <ul className="py-1 text-sm text-gray-700 dark:text-gray-200"
                                        aria-labelledby="dropdownInformationButton">
                                        <li>
                                            <div onClick={() => navigate(`user/my-history/${decoded.Id}`)}
                                                 className="cursor-pointer block py-2 px-4 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
                                                History
                                            </div>
                                        </li>
                                    </ul>
                                    <div className="py-1">
                                        <div onClick={handLogout}
                                             className="block py-2 px-4 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Logout
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div onClick={() => navigate(`user/wishlist/${decoded.Id}`)}
                                 className={`cursor-pointer text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-lg px-4 py-2.5 text-center inline-flex items-center mr-1`}>
                                <FiHeart className={`hover:text-blackColor`}/>
                            </div>
                            <div onClick={() => navigate(`user/shopping-cart/${decoded.Id}`)}
                                 className={`cursor-pointer text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-lg px-4 py-2.5 text-center inline-flex items-center`}>
                                <TbShoppingCart className={`hover:text-blackColor`}/>
                            </div>
                        </>
                    }
                </div>
            </div>
        </nav>
    );
}

export default TopHeader;