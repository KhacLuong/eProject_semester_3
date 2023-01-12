import React, {useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import avatar from "../../../assets/image/avatar/avatar1.png"
import {
    getListProductLatestReleases,
    getProduct,
    getProductById,
    updateViewCountProductById
} from "../../../services/apiService";
import {AiOutlineArrowRight} from 'react-icons/ai'
import parse from "html-react-parser";
import {FiEye, FiPackage, FiHeart, FiPlus} from "react-icons/fi";
import {TbShoppingCart} from "react-icons/tb";
import {HiOutlineMinusSm} from "react-icons/hi";
import {BiCommentDetail} from "react-icons/bi";
import {RxDotFilled} from "react-icons/rx";
import {BsFillCartFill, BsFillSuitHeartFill} from "react-icons/bs";
import '../Product.scss'
import {renderStar} from "../../../ultis/renderStar";
import {toast} from "react-toastify";
import {AddProductToWishList} from "../../../ultis/AddProductToWishList";
import jwt_decode from "jwt-decode";
import {useSelector} from "react-redux";
import {doAddToCart} from "../../../redux/action/cartAction";
import {connect} from "react-redux";

const ProductDetail = (props) => {
    const {setOpen, doAddToCart} = props
    const {id, slug} = useParams();
    const navigate = useNavigate();
    const [imageProduct, setImageProduct] = useState('');
    const [imageAvatar, setAvatar] = useState(avatar);
    const [productName, setProductName] = useState('');
    const [author, setAuthor] = useState('');
    const [codeProduct, setCodeProduct] = useState('');
    const [description, setDescription] = useState('')
    const [intro, setIntro] = useState('')
    const [price, setPrice] = useState(null)
    const [quantity, setQuantity] = useState(null)
    const [view, setView] = useState(null);
    const [status, setStatus] = useState(null)
    const [categoryName, setCategoryName] = useState('')
    const [hover, setHover] = useState(false);
    const [idProduct, setIdProduct] = useState(0);
    const [product, setProduct] = useState({})
    const tags = ['Action and Adventure', 'American Historical Romance', 'Humor', 'True Crime', 'Business', 'Bestsellers', 'Christian Fiction', 'Fantasy', 'Erotic Romance', 'Light Novel', 'Dark Romance & Erotica']

    const [count, setCount] = useState(1);
    const [userId, setUserId] = useState('')
    const account = useSelector(state => state.user.account);
    const [productLatestReleases, setProductLatestReleases] = useState([])

    let decoded = ''

    useEffect(() => {
        if (account.accessToken) {
            decoded = jwt_decode(account.accessToken);
            setUserId(decoded.Id)
        }
        const fetchData = async () => {
            await fetchDetailProduct();
            await fetchProductById();
        }
        fetchData();
    }, [id, slug]);

    const handleOnMouseOver = (event, index) => {
        setHover(true);
        setIdProduct(index + 1);
    }
    const handleClickGoProductDetail = async (id, slug) => {
        let res = await updateViewCountProductById(id)
        if (res.status === true) {
            navigate(`product-detail/${id}/${slug}`)
        }
    }
    const fetchDetailProduct = async () => {
        let res = await getProductById(id)
        if (res.status === true) {
            setProductName(res.data.name)
            setAuthor(res.data.author.name)
            setCodeProduct(res.data.code)
            setDescription(res.data.description)
            setIntro(res.data.intro)
            setPrice(res.data.price)
            setQuantity(res.data.quantity)
            setView(res.data.viewCount)
            setStatus(res.data.status)
            setCategoryName(res.data.category.name)
            setImageProduct(res.data.imageProductPath)
        }
    }
    const fetchProductById = async () => {
        let res = await getProduct(id);
        if (res && res.status === true) {
            setProduct(res.data)
        }
    }
    const handleOnChangeQuantity = (count) => {
        if (typeof (count) === "object") {
            setCount(parseInt(count.target.value));
        } else {
            setCount(count)
        }
    }
    const handlePlusQuantity = (e) => {
        e.preventDefault();
        let plus = count + 1
        handleOnChangeQuantity(plus)
        if (plus > 10) {
            plus = 10
            handleOnChangeQuantity(plus)
            toast.error('You can only buy up to 10 products')
        }
    }
    const handleAddToCart = (data, qty, count) => {
        if (!count) {
            setOpen(false);
            toast.error('Must choose at least one product')
            return
        }
        if (qty > 0) {
            for (let i = 0; i < count; i++) {
                doAddToCart(data)
            }
            setOpen(true);
        } else {
            setOpen(false);
            toast.error('The remaining quantity of products is not enough')
            return;
        }
    }
    useEffect(() => {
        const fetchData = async () => {
            await fetchListProductLatestReleases()
        }
        fetchData();
    }, [])
    const fetchListProductLatestReleases = async () => {
        let res = await getListProductLatestReleases(6)
        if (res.status === true && res) {
            setProductLatestReleases(res.data)
        }
    }
    const handleMinusQuantity = (e) => {
        e.preventDefault();
        let minus = count - 1
        handleOnChangeQuantity(minus)
        if (minus < 1) {
            minus = 1
            handleOnChangeQuantity(minus)
            toast.error('Must choose at least one product')
        }
    }

    const handleOnKeyUp = (e) => {
        const number = parseInt(e.target.value)
        if (number > 10) {
            toast.error('You can only buy up to 10 products')
            setCount(10)
        } else if (number < 1) {
            toast.error('Must choose at least one product')
            setCount(1)
        } else {
            setCount(number)
        }
    }
    return (
        <div className={`product_details`}>
            <div className={`container flex flex-col mx-auto xl:px-30`}>
                <div className={`breadcrumb_wrap py-7`}>
                    <nav className={`woocommerce_breadcrumb flex items-center text-lightColor`}>
                        <div onClick={() => navigate('/')}
                             className={`hover:text-darkColor font-medium text-sm cursor-pointer`}>Home
                        </div>
                        <AiOutlineArrowRight className={'text-sm mx-4'}/>
                        <div onClick={() => navigate('/products')}
                             className={`hover:text-blackColor font-medium text-sm cursor-pointer`}>List posts
                        </div>
                        <AiOutlineArrowRight className={'text-sm mx-4'}/>
                        <a className={`text-dangerColor-default_2 font-medium text-sm `}>{productName}</a>
                    </nav>
                </div>
                <div className={``}>
                    <div className={`grid grid-cols-5 gap-8`}>
                        <div className={`relative shadow-lg col-span-2 p-[30px] rounded-[15px] border-[1px]`}>
                            <div className={`absolute top-0 text-[30px] left-[50%]`}><RxDotFilled/></div>
                            <div className={`relative h-full overflow-hidden`}>
                                <img src={imageProduct} className={`w-full`}/>
                            </div>
                        </div>
                        <div className={`shadow-lg col-span-3 p-[30px] rounded-[15px] border-[1px] mb-auto`}>
                            <div className={`mb-[20px] flex items-center justify-between`}>
                                <span
                                    className={`${quantity > 0 ? 'text-[#82d175] bg-[rgba(130,209,117,0.2)] ' : 'text-[#F65D4E] bg-[rgba(223,44,44,0.2)]'} font-extralight text-sm leading-normal uppercase  py-1 px-2`}>{quantity > 0 ? 'in stock' : 'Out of stock'}</span>
                            </div>
                            <h1 className={`text-[36px] leading-tight font-semiBold mb-[10px] clear-none`}>{productName}</h1>
                            <div
                                className={`flex items-center flex-wrap pb-[10px] mb-[23px] border-b-[1px] leading-loose relative`}>
                                <div
                                    className={`text-sm text-lightColor flex items-center mr-[20px] pr-[20px] relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%]`}>
                                    Author: <div
                                    className={`text-blackColor ml-1 text-sm cursor-pointer hover:text-dangerColor-hover_2 hover:underline`}>{author}</div>
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
                                    <BiCommentDetail className={`text-dangerColor-default_2 text-[15px]`}/>
                                    <span className={`ml-1 text-sm `}>50</span>
                                </div>
                                <div
                                    className={`flex text-center items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                    <FiEye className={`text-blue-600 text-[15px]`}/>
                                    <span className={`ml-1 text-sm`}>{view}</span>
                                </div>
                                <div
                                    className={`flex text-center items-center mr-[20px] pr-[20px] font-medium relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2 `}>
                                    <FiPackage className={`text-yellow-600 text-[15px]`}/>
                                    <span className={`ml-1 text-sm`}>{quantity}</span>
                                </div>
                                <div
                                    className={`flex items-center text-lightColor mb-[6px] uppercase text-sm`}>sku: <span
                                    className={`text-blackColor ml-1 text-sm`}>{codeProduct}</span></div>
                            </div>
                            <p className={`flex text-[30px] font-medium leading-[1.4] items-center text-dangerColor-default_2 mb-[15px]`}>
                                <span>${price}</span>
                            </p>
                            <div className={`mb-[20px] font-light`}>
                                <p>{description}</p>
                            </div>
                            <div className={`py-[25px] flex items-center my-[25px] border-y-[1px]`}>
                                <div className={``}>
                                    <label htmlFor={`custom-input-number`}
                                           className={`w-full text-lightColor text-sm block mt-[-10px] mb-[5px]`}>Quantity</label>
                                    <div
                                        className="custom-number-input relative inline-flex overflow-hidden justify-center items-center w-[160px] h-[52px] mb-[10px] rounded-full mr-[15px] border-[1px] border-borderColor">
                                        <button onClick={handleMinusQuantity} data-action="decrement"
                                                className="flex justify-center items-center p-0 z-9 border-0 text-[12px] font-bold w-[30%] h-[52px] rounded-none text-blackColor bg-whiteColor">
                                            <span className="m-auto text-xl font-thin"><HiOutlineMinusSm/></span>
                                        </button>
                                        <input
                                            id={`custom-input-number`}
                                            className="py-[10px] w-[50%] border-0 text-center font-bold text-blackColor flex-1"
                                            value={count}
                                            onChange={handleOnChangeQuantity}
                                            onKeyUp={handleOnKeyUp}
                                            type="number"
                                        />
                                        <button onClick={handlePlusQuantity} data-action="increment"
                                                className="flex justify-center items-center p-0 z-9 border-0 text-[12px] font-bold w-[30%] h-[52px] rounded-none text-blackColor bg-whiteColor">
                                            <span className="m-auto text-xl font-thin"><FiPlus/></span>
                                        </button>
                                    </div>
                                </div>
                                <div onClick={() => handleAddToCart(product, quantity, count)}
                                     className={`${quantity > 0 ? 'block' : 'hidden'} flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-lime-600 hover:bg-lime-700 cursor-pointer`}>
                                    <BsFillCartFill className={`mr-2`}/>
                                    Add to cart
                                </div>
                                <div onClick={() => AddProductToWishList(userId, id)}
                                     className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 duration-300 cursor-pointer`}>
                                    <BsFillSuitHeartFill className={`mr-2`}/>
                                    Add to wishlist
                                </div>
                            </div>
                            <div className={`mt-[23px]`}>
                                <span
                                    className={`flex items-center text-lightColor text-[14px] font-normal leading-tight`}>
                                    Category: <div className={`text-blackColor ml-2`}>{categoryName}</div>
                                </span>
                                <span
                                    className={`flex items-center text-lightColor text-[14px] font-normal leading-tight mt-2 flex-wrap`}>
                                    Tags:

                                </span>
                                <div className={`mt-2`}>
                                    {
                                        tags.map((item, index) => (
                                            <div key={index}
                                                 className="mr-2 mb-2 leading-5  text-xs inline-flex items-center font-bold leading-sm uppercase px-3 py-1 bg-blue-600 hover:bg-blue-700 text-whiteColor italic cursor-pointer"
                                            >{item}</div>
                                        ))
                                    }
                                </div>
                                <div className={`mt-4`}>
                                    <p className="text-sm font-medium text-gray-500 dark:text-gray-400">1,745 global ratings</p>
                                    <div className="flex items-center mt-4">
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">5 star</span>
                                        <div className="w-2/4 h-5 mx-4 bg-gray-200 rounded dark:bg-gray-700">
                                            <div className="h-5 bg-yellow-400 rounded" style={{width: "70%"}} ></div>
                                        </div>
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">70%</span>
                                    </div>
                                    <div className="flex items-center mt-4">
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">4 star</span>
                                        <div className="w-2/4 h-5 mx-4 bg-gray-200 rounded dark:bg-gray-700">
                                            <div className="h-5 bg-yellow-400 rounded" style={{width: "17%"}}></div>
                                        </div>
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">17%</span>
                                    </div>
                                    <div className="flex items-center mt-4">
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">3 star</span>
                                        <div className="w-2/4 h-5 mx-4 bg-gray-200 rounded dark:bg-gray-700">
                                            <div className="h-5 bg-yellow-400 rounded" style={{width: "8&"}}></div>
                                        </div>
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">8%</span>
                                    </div>
                                    <div className="flex items-center mt-4">
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">2 star</span>
                                        <div className="w-2/4 h-5 mx-4 bg-gray-200 rounded dark:bg-gray-700">
                                            <div className="h-5 bg-yellow-400 rounded" style={{width: "4%"}}></div>
                                        </div>
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">4%</span>
                                    </div>
                                    <div className="flex items-center mt-4">
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">1 star</span>
                                        <div className="w-2/4 h-5 mx-4 bg-gray-200 rounded dark:bg-gray-700">
                                            <div className="h-5 bg-yellow-400 rounded" style={{width: "1%"}}></div>
                                        </div>
                                        <span className="text-sm font-medium text-blue-600 dark:text-blue-500">1%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className={`product_comment mt-10`}>
                        <form>
                            <div
                                className="w-full mb-4 border border-gray-200 rounded-lg bg-gray-50 dark:bg-gray-700 dark:border-gray-600">
                                <div className="px-4 py-2 bg-white rounded-t-lg dark:bg-gray-800">
                                    <label htmlFor="comment" className="sr-only">Your comment</label>
                                    <textarea id="comment" rows="4"
                                              className="w-full px-0 text-sm text-gray-900 bg-white border-0 dark:bg-gray-800 focus:ring-0 dark:text-white dark:placeholder-gray-400"
                                              placeholder="Write a comment..." required></textarea>
                                </div>
                                <div
                                    className="flex items-center justify-between px-3 py-2 border-t dark:border-gray-600">
                                    <button type="submit"
                                            className="inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-blue-700 rounded-lg focus:ring-4 focus:ring-blue-200 dark:focus:ring-blue-900 hover:bg-blue-800">
                                        Post comment
                                    </button>
                                    <div className="flex pl-0 space-x-1 sm:pl-2">
                                        <button type="button"
                                                className="inline-flex justify-center p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor"
                                                 viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                                <path fillRule="evenodd"
                                                      d="M8 4a3 3 0 00-3 3v4a5 5 0 0010 0V7a1 1 0 112 0v4a7 7 0 11-14 0V7a5 5 0 0110 0v4a3 3 0 11-6 0V7a1 1 0 012 0v4a1 1 0 102 0V7a3 3 0 00-3-3z"
                                                      clipRule="evenodd"></path>
                                            </svg>
                                            <span className="sr-only">Attach file</span>
                                        </button>
                                        <button type="button"
                                                className="inline-flex justify-center p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor"
                                                 viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                                <path fillRule="evenodd"
                                                      d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z"
                                                      clipRule="evenodd"></path>
                                            </svg>
                                            <span className="sr-only">Set location</span>
                                        </button>
                                        <button type="button"
                                                className="inline-flex justify-center p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor"
                                                 viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                                <path fillRule="evenodd"
                                                      d="M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z"
                                                      clipRule="evenodd"></path>
                                            </svg>
                                            <span className="sr-only">Upload image</span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div>
                        <article>
                            <div className="flex items-center mb-4 space-x-4">
                                <img className="w-10 h-10 rounded-full" src={imageAvatar}
                                     alt=""/>
                                <div className="space-y-1 font-medium dark:text-white">
                                    <p>Jese Leos <time dateTime="2014-08-16 19:00"
                                                       className="block text-sm text-gray-500 dark:text-gray-400">Joined
                                        on August 2014</time></p>
                                </div>
                            </div>
                            <div className="flex items-center mb-1">
                                <svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor"
                                     viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><title>First star</title>
                                    <path
                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                </svg>
                                <svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor"
                                     viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><title>Second star</title>
                                    <path
                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                </svg>
                                <svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor"
                                     viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><title>Third star</title>
                                    <path
                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                </svg>
                                <svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor"
                                     viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><title>Fourth star</title>
                                    <path
                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                </svg>
                                <svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor"
                                     viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><title>Fifth star</title>
                                    <path
                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                </svg>
                                <h3 className="ml-2 text-sm font-semibold text-gray-900 dark:text-white">Thinking to buy
                                    another one!</h3>
                            </div>
                            <footer className="mb-5 text-sm text-gray-500 dark:text-gray-400">
                                <p>Reviewed in the United Kingdom on <time dateTime="2017-03-03 19:00">March 3,
                                    2017</time></p>
                            </footer>
                            <p className="mb-2 font-light text-gray-500 dark:text-gray-400">This is my third Invicta Pro
                                Diver. They are just fantastic value for money. This one arrived yesterday and the first
                                thing I did was set the time, popped on an identical strap from another Invicta and went
                                in the shower with it to test the waterproofing.... No problems.</p>
                            <p className="mb-3 font-light text-gray-500 dark:text-gray-400">It is obviously not the same
                                build quality as those very expensive watches. But that is like comparing a Citroën to a
                                Ferrari. This watch was well under £100! An absolute bargain.</p>
                            <a href="#"
                               className="block mb-5 text-sm font-medium text-blue-600 hover:underline dark:text-blue-500">Read
                                more</a>
                            <aside>
                                <p className="mt-1 text-xs text-gray-500 dark:text-gray-400">19 people found this
                                    helpful</p>
                                <div
                                    className="flex items-center mt-3 space-x-3 divide-x divide-gray-200 dark:divide-gray-600">
                                    <a href="#"
                                       className="text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-xs px-2 py-1.5 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700">Helpful</a>
                                    <a href="#"
                                       className="pl-4 text-sm font-medium text-blue-600 hover:underline dark:text-blue-500">Report
                                        abuse</a>
                                </div>
                            </aside>
                        </article>
                    </div>
                    <div className={`product_related mb-[60px] pt-[60px] `}>
                        <h2 className={`flex items-center text-[26px] leading-tight mb-[40px] text-left text-blackColor font-semiBold clear-both after:content-[''] after:inline-block after:h-[1px] after:ml-[55px] after:bg-borderColor after:flex-1`}>
                            Related products
                        </h2>
                        <ul className={`mx-[-15px] mb-0 clear-both grid grid-cols-6 shadow-lg rounded-[15px]`}>
                            {
                                productLatestReleases.map((item, index) => {

                                    return <li className={``} title={item.name} key={index}>
                                        <div className={`px-[15px] mb-[60px] w-full duration-300 border-r-[1px] last:border-0 cursor-pointer`}>
                                            <div onMouseLeave={() => setHover(false)} className={`flex relative items-center flex-col`}>
                                                <div onMouseOver={(e) => handleOnMouseOver(e, index)} state={item}
                                                     className={`overflow-hidden rounded-[15px] w-56`}>
                                                    <div>
                                                        <img width={`600`} height={`840`}
                                                             className={`max-w-full h-auto`}
                                                             src={item.imageProductPath}/>
                                                    </div>
                                                    <div
                                                        className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                                        <div
                                                            className={`shop_action flex flex-col items-start relative`}>
                                                            <button
                                                                className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                actionBtn text-dangerColor-default_3 duration-300`}>
                                                                <FiHeart/>
                                                            </button>
                                                            <div
                                                                onClick={() => handleClickGoProductDetail(item.id, item.slug)}
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
                                            </div>
                                            <div className={`relative pt-[20px]`}>
                                                <h3 className={`hover:text-dangerColor-default_2 duration-300 text-[16px] mb-[8px] font-semiBold leading-normal overflow-hidden truncate `}>{item.name}</h3>
                                                <div
                                                    className={`flex items-center mb-[10px] text-[12px] font-semiBold`}>
                                                    <div className={`flex items-center mr-[5px] overflow-hidden`}>
                                                        {parse(renderStar(5))}
                                                    </div>
                                                    <span>5</span>
                                                </div>
                                                <div
                                                    className={`text-[12px] leading-none text-lightColor font-normal mb-[12px]`}>
                                                    {item.author}
                                                </div>
                                                <span
                                                    className={`text-[20px] leading-normal font-medium text-dangerColor-default_2`}>
                                                        {item.price}
                                                    </span>
                                            </div>
                                        </div>
                                    </li>
                                })
                            }
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        doAddToCart: (data) => dispatch(doAddToCart(data))
    }
}
export default connect(null, mapDispatchToProps)(ProductDetail);