import React, {useEffect, useRef, useState} from 'react';
import {useNavigate} from "react-router-dom";
import {GiBookAura} from "react-icons/gi"
import {FiHeart} from "react-icons/fi"
import {TbShoppingCart} from "react-icons/tb"
import Search from "./Search";
import {connect, useDispatch, useSelector} from "react-redux";
import {deleteLogout, getCountProductInWishList, getMyInfo} from "../../../services/apiService";
import {toast} from "react-toastify";
import {doLogout} from "../../../redux/action/userAction";
import jwt_decode from "jwt-decode";
import {removeAllProdcut} from "../../../redux/action/cartAction";
import * as moment from "moment/moment";

const TopHeader = (props) => {

    const [showNavUser, setShowNavUser] = useState(false);
    const [avatar, setAvatar] = useState(null)
    const [cartCount, setCartCount] = useState(0)
    const isAuthenticated = useSelector(state => state.user.isAuthenticated);
    const account = useSelector(state => state.user.account);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    let ref = useRef();

    let userId = ''

    if(account.accessToken) {
        userId = jwt_decode(account.accessToken).Id
    }
    const [totalWishList, setTotalWishList] = useState(0)

    useEffect(() => {
        const fetchData = async () => {
            await fetchCountProduct()
            await fetchMyInfo()
        }
        fetchData();
    })

    useEffect(() => {
        let count = 0;
        props.cart.forEach(item => {
            count += item.qty
        })
        setCartCount(count)
    }, [props.cart, cartCount])

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

    const fetchMyInfo = async () => {
        if(userId !== '') {
            let res = await getMyInfo(userId)
            if (res.status === true) {
                setAvatar(res.data.userInfo.avatar)
            }
        }
    }
    const fetchCountProduct = async () =>  {
        if(userId !== '') {
            let res = await getCountProductInWishList(userId)
            if(res && res.status === true) {
                setTotalWishList(res.data.totalWishlist)
            }
        }
    }
    const handleClickUser = () => {
        setShowNavUser(!showNavUser)
    }
    const handLogout = async () => {
        let res = await deleteLogout();
        if (res.status === true) {
            dispatch(doLogout());
            dispatch(removeAllProdcut());
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
                    <span className="self-center text-2xl font-semibold whitespace-nowrap ">Shradha</span>
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
                                 className="relative cursor-pointer text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-lg text-center inline-flex items-center mr-1"
                                 onClick={() => handleClickUser()}
                                 onMouseLeave={() => handleLeaveNavUser()}>
                                <img style={{
                                    backgroundSize: "cover",
                                    backgroundPosition: "center",
                                    objectFit: "cover"
                                }} className="w-10 h-10 rounded" src={avatar} alt=""/>
                                <div
                                    className={`${showNavUser ? 'block' : 'hidden'} rounded-[10px] border-[1px]  absolute left-0 top-[50px] w-44 bg-white divide-y divide-gray-100 shadow dark:bg-gray-700 dark:divide-gray-600 before:absolute before:content[''] before:w-[10px] before:h-[10px] before:bg-whiteColor before:top-[-5px] before:left-[13%] before:rotate-45 z-[9999] before:border-[#e4e4e4]-[1px] before:border-b-0 before:border-r-0 before:hover:bg-gray-100`}>
                                    <div onClick={() => {userId !== '' && navigate(`/user/my-profile/${userId}`)} } className="py-3 px-4 text-sm text-gray-900 dark:text-white hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white rounded-t-[10px]">
                                        <div className="font-medium truncate">{account.username}</div>
                                        <div className={`font-light`}>({account.email})</div>
                                    </div>
                                    <ul className="py-1 text-sm text-gray-700 dark:text-gray-200"
                                        aria-labelledby="dropdownInformationButton">
                                        <li>
                                            <div onClick={() => {userId !== '' && navigate(`user/wishlist/${userId}`)}}
                                                 className="relative cursor-pointer block py-2 px-4 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
                                                Wish list <span className={`${totalWishList !== 0 ? 'block' : 'hidden'} absolute right-[25px] top-[22%] bg-dangerColor-default_2 text-whiteColor rounded-full text-xs font-semiBold w-[20px] h-[20px] flex items-center justify-center text-center`}>{totalWishList}</span>
                                            </div>
                                        </li>
                                        <li>
                                        <div onClick={() => {userId !== '' && navigate(`user/my-history/${userId}`)} }
                                             className="cursor-pointer block py-2 px-4 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
                                            History
                                        </div>
                                    </li>
                                    </ul>
                                    <div className="py-1">
                                        <div onClick={() => handLogout()}
                                             className="block py-2 px-4 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Logout
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </>
                    }
                    <div onClick={() => props.setOpen(true)}
                         className={`cursor-pointer text-darkColor bg-whiteColor hover:bg-bgWhiteColor hover:text-blackColor outline-0 border-0 font-medium rounded-md text-lg w-10 h-10 text-center inline-flex items-center ml-2 relative justify-center`}>
                        <TbShoppingCart className={`hover:text-blackColor`}/>
                        {
                            cartCount !== 0
                                ? <span className={`flex justify-center items-center absolute -right-2 -top-2 border-2 border-whiteColor bg-dangerColor-default_2 text-whiteColor rounded-full text-xs font-semiBold w-6 h-6`}>{cartCount}</span>
                                : <span></span>
                        }

                    </div>
                </div>
            </div>
        </nav>
    );
}
const mapStateToProps = state => {
    return {
        cart: state.cart.cart
    }
}
export default connect(mapStateToProps)(TopHeader);