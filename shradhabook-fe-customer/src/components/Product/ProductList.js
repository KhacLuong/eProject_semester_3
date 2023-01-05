import {useNavigate} from 'react-router-dom';
import {BsFillCartFill, BsFillGridFill, BsFillSuitHeartFill} from 'react-icons/bs'
import './Product.scss'
import ReactPaginate from 'react-paginate';
import parse from "html-react-parser";
import {FiHeart, FiEye, FiPackage} from "react-icons/fi";
import {FaThList} from "react-icons/fa";
import {TbShoppingCart} from "react-icons/tb"
import React, {useState} from "react";
import book14 from "../../assets/image/books/book14.png"
import {updateViewCountProductById} from "../../services/apiService";
import {BiCommentDetail} from "react-icons/bi";

const ProductList = (props) => {
    const navigate = useNavigate();
    const {
        totalPage,
        optionSort,
        optionQuantity,
        selectedPerPage,
        selectedSort,
        listProducts,
        handleChangeSort,
        handleChangeQuantity,
        handleSearchProduct,
        fetchListProducts,
        renderStar
    } = props
    const [hover, setHover] = useState(false);
    const [idProduct, setIdProduct] = useState(0);
    const [turnOffPrevNextBtn, setTurnOffPrevNextBtn] = useState(true)
    const [imageProduct, setImageProduct] = useState(book14);
    const [activeLayout, setActiveLayout] = useState(true);
    const handlePageClick = (event) => {
        fetchListProducts(+event.selected + 1)
        console.log(`User requested page number ${+event.selected + 1}`)
        if (+event.selected + 1 === 1) {
            setTurnOffPrevNextBtn(true)
        }
        if (+event.selected + 1 === totalPage) {
            setTurnOffPrevNextBtn(true)
        }
    };
    const handleOnMouseOver = (event, index) => {
        setHover(true);
        setIdProduct(index + 1);
    }
    const handleChangeLayout = () => {
        setActiveLayout(!activeLayout)
    }
    const handleClickGoProductDetail = async (id, slug) => {
        let res = await updateViewCountProductById(id)
        if (res === 'Success') {
            navigate(`product-detail/${id}/${slug}`)
        }
    }

    return (<div className={`product_list col-span-3`}>
        <div className={`shadow-md flex items-start justify-between border-b-[1px] py-2 pl-2`}>
            <div className={`flex items-center text-xl`} onClick={handleChangeLayout}>
                {
                    activeLayout
                        ? <BsFillGridFill title={`Grid View`}
                                          className={`cursor-pointer mr-2 text-dangerColor-default_2 hover:text-dangerColor-hover_2`}/>
                        : <FaThList title={`List View`}
                                    className={`cursor-pointer text-dangerColor-default_2 hover:text-dangerColor-hover_2`}/>
                }
            </div>
            <div className={`grid grid-cols-2`}>
                <div
                    className={`col-span-1 flex items-center py-0 text-sm leading-6 text-gray-500 bg-transparent border-0 dark:text-gray-400 focus:outline-none focus:ring-0 min-w-full`}>
                    <div className={`mr-2 font-medium text-blackColor`}>
                        Search:
                    </div>
                    <input onChange={handleSearchProduct} className={`w-full border-none outline-none`}
                           placeholder={`products...`}/>
                </div>
                <div className={`col-span-1 flex items-center`}>
                    <div
                        className={`relative after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-12 after:top-[10%]`}>
                        <select value={selectedSort} onChange={handleChangeSort}
                                className="cursor-pointer text-right block py-0 pr-[30px] text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer ">
                            {optionSort.map((option, index) => (<option key={index} value={option.value}>
                                {option.text}
                            </option>))}
                        </select>
                    </div>
                    <div
                        className={`relative flex items-center pl-[19px] ml-[14px] after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-0 after:top-[10%] `}>
                        <label htmlFor={`per_page`} className={`text-lightColor font-normal text-sm`}>Show</label>
                        <select name={`per_page`} id={`per_page`}
                                onChange={handleChangeQuantity}
                                value={selectedPerPage}
                                className="cursor-pointer py-0 text-left block text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer">
                            {optionQuantity.map((option, index) => (<option key={index} value={option.value}>
                                {option.text}
                            </option>))}
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div className={`show_product_list`}>
            {
                activeLayout
                    ? <div className={`render_product grid grid-cols-3 gap-4 py-3`}>
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
                                                    actionBtn text-dangerColor-default_3 transition duration-300 ease-in-out`}>
                                                    <FiHeart/></button>
                                                <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                                     state={item.id}
                                                     className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible' + ' translate-x-0' : 'opacity-0 translate-x-8' + ' invisible'} actionBtn delay-100 transition duration-300 ease-in-out`}>
                                                    <FiEye/>
                                                </div>
                                                <button
                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8 invisible'} actionBtn delay-200 transition duration-300 ease-in-out`}>
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
                                            className={`detail_product_price flex items-center justify-between text-center  leading-normal mt-2`}>
                                            <div className={`text-2xl font-medium text-lime-600`}>
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
                    : <div className={`render_product grid grid-cols-1 gap-4 py-3`}>
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
                                                 src={imageProduct}/>
                                        </div>
                                        <div className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                            <div className={`shop_action flex flex-col items-start relative`}>
                                                <button
                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                    actionBtn text-dangerColor-default_3 transition duration-300 ease-in-out`}>
                                                    <FiHeart/></button>
                                                <div onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                                     state={item.id}
                                                     className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible' + ' translate-x-0' : 'opacity-0 translate-x-8' + ' invisible'} actionBtn delay-100 transition duration-300 ease-in-out`}>
                                                    <FiEye/>
                                                </div>
                                                <button
                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8 invisible'} actionBtn delay-200 transition duration-300 ease-in-out`}>
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
                                                <div className={`text-[28px] font-medium text-lime-600`}>
                                                    ${item.price}
                                                </div>
                                            </div>
                                            <div className={`flex items-center`}>
                                                <div
                                                    className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor transition duration-300 ease bg-lime-600 hover:bg-lime-700 cursor-pointer`}>
                                                    <BsFillCartFill className={`mr-2`}/>
                                                    Add to cart
                                                </div>
                                                <div
                                                    className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 transition duration-300 ease cursor-pointer`}>
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
            }
            <div className={`pagination flex items-center justify-center mt-10`}>
                <ReactPaginate
                    nextLabel="next"
                    previousLabel="previous"
                    onPageChange={handlePageClick}
                    pageRangeDisplayed={3}
                    marginPagesDisplayed={2}
                    pageCount={totalPage}
                    pageClassName="pageClassName"
                    pageLinkClassName="pageLinkClassName font-semiBold"
                    previousClassName="previousClassName"
                    previousLinkClassName="previousLinkClassName font-semiBold"
                    nextClassName="nextClassName"
                    nextLinkClassName="nextLinkClassName font-semiBold"
                    breakLabel="..."
                    breakClassName="breakLinkClassName text-xl font-medium"
                    containerClassName="flex items-center justify-center inline-flex -space-x-px"
                    activeLinkClassName="text-whiteColor bg-dangerColor-default_2"
                    renderOnZeroPageCount={null}
                    disabledClassName={turnOffPrevNextBtn ? 'hidden' : ''}
                    disabledLinkClassName={turnOffPrevNextBtn ? 'hidden' : ''}
                />
            </div>
        </div>
    </div>);
};

export default ProductList;