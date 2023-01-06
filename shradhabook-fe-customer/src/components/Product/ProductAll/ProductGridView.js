import React from 'react';
import {FiEye, FiHeart, FiPackage} from "react-icons/fi";
import {TbShoppingCart} from "react-icons/tb";
import parse from "html-react-parser";
import {BiCommentDetail} from "react-icons/bi";
import {useNavigate} from "react-router-dom";

const ProductGridView = (props) => {
    const navigate = useNavigate();
    const {
        listProducts,
        setHover,
        handleClickGoProductDetail,
        handleOnMouseOver,
        imageProduct,
        hover,
        idProduct,
        renderStar,
        setOpen
    } = props
    return (
        <div className={`render_product grid grid-cols-3 gap-4 py-3`}>
            {listProducts.map((item, index) => {
                return <div
                    className={`w-full py-5 flex justify-center items-center shadow-lg rounded-2xl`}
                    key={index}>
                    <div>
                        <div className={`product-transition relative w-[300px] h-[420px]`}
                             onMouseLeave={() => setHover(false)}>
                            <div title={`${item.description}`}
                                 onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                 onMouseOver={(e) => handleOnMouseOver(e, index)} state={item}
                                 className={`overflow-hidden rounded-2xl cursor-pointer`}>
                                <img className={`w-full rounded-2xl block my-0 mx-auto`}
                                     src={imageProduct}/>
                            </div>
                            <div className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                <div className={`shop_action flex flex-col items-start relative`}>
                                    <button
                                        className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                    actionBtn text-dangerColor-default_3 duration-300`}>
                                        <FiHeart/></button>
                                    <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                         state={item.id}
                                         className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible' + ' translate-x-0' : 'opacity-0 translate-x-8' + ' invisible'} actionBtn delay-100 duration-300`}>
                                        <FiEye/>
                                    </div>
                                    <button onClick={() => setOpen(true)}
                                        className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8 invisible'} actionBtn delay-200 duration-300`}>
                                        <TbShoppingCart/></button>
                                </div>
                            </div>
                        </div>
                        <div onMouseOver={(e) => handleOnMouseOver(e, index)}
                             className={`product-caption relative pt-[20px] flex flex-col `}>
                            <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                 className={`cursor-pointer text-xl ml-1 font-semibold overflow-hidden mb-1`}>
                                <h2>{item.name}</h2>
                            </div>
                            <div
                                className={`ml-1 cursor-pointer detail_product_author text-xs text-lightColor leading-none font-normal hover:text-dangerColor-default_2`}>
                                <div onClick={() => navigate(``)}>{item.author}</div>
                            </div>
                            <div
                                className={`count_review text-sm my-[10px] flex items-center font-semibold`}>
                                <div className={`flex items-center`}>
                                    {parse(renderStar(4))}
                                    <svg aria-hidden="true"
                                         className="w-5 h-5 text-gray-300 dark:text-gray-500 "
                                         fill="currentColor" viewBox="0 0 20 20"
                                         xmlns="http://www.w3.org/2000/svg"><title>Fifth star</title>
                                        <path
                                            d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                    </svg>
                                </div>
                                <span className={`ml-2`}>5</span>
                            </div>
                            <div
                                className={`detail_product_price flex items-center justify-between text-center leading-normal mt-2`}>
                                <div className={`text-2xl font-medium text-dangerColor-default_2`}>
                                    ${item.price}
                                </div>
                                <div className={`flex items-center`}>
                                    <div
                                        className={`flex items-center mr-3 justify-center text-center`}>
                                        <BiCommentDetail
                                            className={`mr-1 text-sm text-dangerColor-default_2`}/>
                                        <span className={`font-medium`}>50</span>
                                    </div>
                                    <div
                                        className={`flex items-center mr-3 justify-center  text-center`}>
                                        <FiEye className={`mr-1 text-sm text-blue-600`}/>
                                        <div className={`font-medium`}>{item.viewCount}</div>
                                    </div>
                                    <div
                                        className={`flex items-center justify-center text-center`}>
                                        <FiPackage className={`mr-1 text-sm text-yellow-600`}/>
                                        <div className={`font-medium`}>{item.quantity}</div>
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

export default ProductGridView;