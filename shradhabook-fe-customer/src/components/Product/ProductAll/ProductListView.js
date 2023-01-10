import React from 'react';
import {FiEye, FiHeart, FiPackage} from "react-icons/fi";
import {TbShoppingCart} from "react-icons/tb";
import parse from "html-react-parser";
import {BiCommentDetail} from "react-icons/bi";
import {BsFillCartFill, BsFillSuitHeartFill} from "react-icons/bs";
import {useNavigate} from "react-router-dom";
import {AddProductToWishList} from "../../../ultis/AddProductToWishList";

const ProductListView = (props) => {
    const navigate = useNavigate();
    const {
        listProducts,
        setHover,
        handleClickGoProductDetail,
        handleOnMouseOver,
        hover,
        idProduct,
        renderStar,
        setOpen,
        userId,
        doAddToCart
    } = props
    const handleAddToCart = (data) => {
        doAddToCart(data)
        setOpen(true)
    }
    return (
        <div className={`render_product grid grid-cols-1 gap-4 py-3`}>
            {listProducts.map((item, index) => {
                return <div className={`w-full py-5 px-2 shadow-lg rounded-2xl`}
                            key={index}>
                    <div className={`grid grid-cols-4 gap-40`}>
                        <div className={`product-transition relative w-[300px] h-[420px] col-span-1`}
                             onMouseLeave={() => setHover(false)}>
                            <div title={`${item.description}`}
                                 onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                 onMouseOver={(e) => handleOnMouseOver(e, index)} state={item}
                                 className={`overflow-hidden rounded-2xl cursor-pointer`}>
                                <img className={`w-full rounded-2xl block my-0 mx-auto`}
                                     src={item.imageProductPath}/>
                            </div>
                            <div className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                <div className={`shop_action flex flex-col items-start relative`}>
                                    <button onClick={() => AddProductToWishList(userId, item.id)}
                                            className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                    actionBtn text-dangerColor-default_3 duration-300`}>
                                        <FiHeart/></button>
                                    <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                         state={item.id}
                                         className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible' + ' translate-x-0' : 'opacity-0 translate-x-8' + ' invisible'} actionBtn delay-100  duration-300`}>
                                        <FiEye/>
                                    </div>
                                    <button onClick={() => handleAddToCart(item)}
                                            className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8 invisible'} actionBtn delay-200 duration-300`}>
                                        <TbShoppingCart/></button>
                                </div>
                            </div>
                        </div>
                        <div className={`col-span-3 product-caption flex flex-col justify-center`}>
                            <div className={` items relative`}>
                                <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                     className={`cursor-pointer text-[30px] font-semibold mb-4 overflow-hidden`}>
                                    <h3>{item.name}</h3>
                                </div>
                                <div
                                    className={`flex items-center flex-wrap pb-[10px] mb-[23px] border-b-[1px] leading-loose relative`}>
                                    <div
                                        className={`text-sm text-lightColor flex items-center mr-[20px] pr-[20px] relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%]`}>
                                        Author: <div
                                        className={`text-blackColor ml-1 text-sm cursor-pointer hover:text-dangerColor-hover_2 hover:underline`}>{item.author}</div>
                                    </div>
                                    <div
                                        className={`text-sm flex items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                        {parse(renderStar(4))}
                                        <svg aria-hidden="true"
                                             className="w-5 h-5 text-gray-300 dark:text-gray-500 "
                                             fill="currentColor" viewBox="0 0 20 20"
                                             xmlns="http://www.w3.org/2000/svg"><title>Fifth star</title>
                                            <path
                                                d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                        </svg>
                                        <span className={`ml-1`}>4</span>
                                    </div>
                                    <div
                                        className={`flex items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                        <BiCommentDetail
                                            className={`text-dangerColor-default_2 text-[15px]`}/>
                                        <span className={`ml-1 text-sm `}>50</span>
                                    </div>
                                    <div
                                        className={`flex text-center items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                        <FiEye className={`text-blue-600 text-[15px]`}/>
                                        <span className={`ml-1 text-sm`}>{item.viewCount}</span>
                                    </div>
                                    <div
                                        className={`flex text-center items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2 `}>
                                        <FiPackage className={`text-yellow-600 text-[15px]`}/>
                                        <span className={`ml-1 text-sm`}>{item.quantity}</span>
                                    </div>
                                    <div
                                        className={`flex items-center text-lightColor mb-[6px] uppercase text-sm`}>sku: <span
                                        className={`text-blackColor ml-1 text-sm`}>{item.code}</span></div>
                                </div>

                                <div
                                    className={`flex-wrap truncate italic pb-[20px] mb-[23px] border-b-[1px]`}>
                                    {item.description}
                                </div>
                                <div
                                    className={`detail_product_price leading-normal mb-[20px]`}>
                                    <div className={`text-[28px] font-medium text-dangerColor-default_2`}>
                                        ${item.price}
                                    </div>
                                </div>
                                <div className={`flex items-center`}>
                                    <div onClick={() => handleAddToCart(item)}
                                         className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-lime-600 hover:bg-lime-700 cursor-pointer`}>
                                        <BsFillCartFill className={`mr-2`}/>
                                        Add to cart
                                    </div>
                                    <div onClick={() => AddProductToWishList(userId, item.id)}
                                         className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 duration-300 cursor-pointer`}>
                                        <BsFillSuitHeartFill className={`mr-2`}/>
                                        Add to wishlist
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            })}
        </div>
    );
};

export default ProductListView;