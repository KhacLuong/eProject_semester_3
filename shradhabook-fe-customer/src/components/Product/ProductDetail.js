import React, {useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import book14 from "../../assets/image/books/book3.png"
import {getProductById, updateViewCountProductById} from "../../services/apiService";
import {AiOutlineArrowRight} from 'react-icons/ai'
import parse from "html-react-parser";
import {FiEye, FiPackage, FiHeart, FiPlus} from "react-icons/fi";
import {TbShoppingCart} from "react-icons/tb";
import {HiOutlineMinusSm} from "react-icons/hi";
import {BiCommentDetail} from "react-icons/bi";
import {RxDotFilled} from "react-icons/rx";
import {BsFillCartFill, BsFillSuitHeartFill} from "react-icons/bs";
import './Product.scss'
import {Data} from "./Data";
import {renderStar} from "../../ultis/renderStar";
import {toast} from "react-toastify";

const ProductDetail = () => {
    const {id, slug} = useParams();
    const navigate = useNavigate();
    const [imageProduct, setImageProduct] = useState(book14);
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
    const tags = ['Action and Adventure', 'American Historical Romance', 'Humor', 'True Crime', 'Business', 'Bestsellers', 'Christian Fiction', 'Fantasy', 'Erotic Romance', 'Light Novel', 'Dark Romance & Erotica']
    const [count, setCount] = useState(0);

    useEffect(() => {
        fetchDetailProduct();
    }, []);

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
        console.log(count)
        e.preventDefault();
        let plus = count + 1
        handleOnChangeQuantity(plus)
        if (plus > 10) {
            plus = 10
            handleOnChangeQuantity(plus)
            toast.error('You can only buy up to 10 products')
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
        if (minus > 10) {
            minus = 10
            handleOnChangeQuantity(minus)
            toast.error('You can only buy up to 10 products')
        }
    }

    const handleOnKeyUp = (e) => {
        const number = parseInt(e.target.value)
        if (number > 10 ) {
            toast.error('You can only buy up to 10 products')
            setCount(10)
        } else if(number < 1) {
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
                                    className={`${status === 'Active' ? 'text-[#82d175] bg-[rgba(130,209,117,0.2)] ' : 'text-[#F65D4E] bg-[rgba(223,44,44,0.2)]'} font-extralight text-sm leading-normal uppercase  py-1 px-2`}>{status === 'Active' ? 'in stock' : 'Out of stock'}</span>
                            </div>
                            <h1 className={`text-[36px] leading-tight font-semiBold mb-[10px] clear-none`}>{productName}</h1>
                            <div
                                className={`flex items-center flex-wrap pb-[10px] mb-[23px] border-b-[1px] leading-loose text-[12px] relative`}>
                                <div
                                    className={`text-[12px] text-lightColor flex items-center mr-[20px] pr-[20px] relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%]`}>
                                    Author: <div
                                    className={`text-blackColor ml-1 cursor-pointer hover:text-dangerColor-hover_2 hover:underline`}>{author}</div>
                                </div>
                                <div
                                    className={`flex items-center mr-[20px] pr-[20px] font-semiBold relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                    {parse(renderStar(4))}
                                    <svg aria-hidden="true"
                                         className="w-4 h-4 text-gray-300 dark:text-gray-500 "
                                         fill="currentColor" viewBox="0 0 20 20"
                                         xmlns="http://www.w3.org/2000/svg"><title>Fifth star</title>
                                        <path
                                            d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                    </svg>
                                    <span className={`ml-1`}>4</span>
                                </div>
                                <div
                                    className={`flex items-center mr-[20px] pr-[20px] font-semiBold relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                    <BiCommentDetail className={`text-successColor text-[15px]`}/>
                                    <span className={`ml-1`}>50</span>
                                </div>
                                <div
                                    className={`flex text-blue-600 text-center items-center mr-[20px] pr-[20px] font-semiBold relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2`}>
                                    <FiEye className={`text-blue-600 text-[15px]`}/>
                                    <span className={`ml-1`}>{view}</span>
                                </div>
                                <div
                                    className={`flex text-center items-center mr-[20px] pr-[20px] font-semiBold relative mb-[6px] after:content-[''] after:h-[11px] after:w-[1px] after:bg-borderColor after:absolute after:right-0 after:top-1/2 after:translate-y-[-50%] cursor-pointer hover:underline hover:text-dangerColor-hover_2 `}>
                                    <FiPackage className={`text-yellow-600 text-[15px]`}/>
                                    <span className={`ml-1`}>{quantity}</span>
                                </div>
                                <div className={`flex items-center text-lightColor mb-[6px] uppercase`}>sku: <span
                                    className={`text-blackColor ml-1`}>{codeProduct}</span></div>
                            </div>
                            <p className={`flex text-[30px] font-semiBold leading-[1.4] items-center text-dangerColor-default_2 mb-[15px]`}>
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
                            </div>
                        </div>
                    </div>
                    <div className={`product_tabs`}>
                    </div>
                    <div className={`product_related mb-[60px] pt-[60px] `}>
                        <h2 className={`flex items-center text-[26px] leading-tight mb-[40px] text-left text-blackColor font-semiBold clear-both after:content-[''] after:inline-block after:h-[1px] after:ml-[55px] after:bg-borderColor after:flex-1`}>
                            Related products
                        </h2>
                        <ul className={`mx-[-15px] mb-0 clear-both grid grid-cols-6 shadow-lg rounded-[15px]`}>
                            {
                                Data.DT.products.map((item, index) => {
                                    if (item.id >= 17) {
                                        return <li className={``} title={item.name} key={index}>
                                            <div
                                                className={`px-[15px] mb-[60px] w-full  transition duration-300 ease ${item.id === 22 ? 'border-0' : 'border-r-[1px]'} cursor-pointer`}>
                                                <div className={`flex relative items-center flex-col`}>
                                                    <div onMouseOver={(e) => handleOnMouseOver(e, index)} state={item}
                                                         className={`overflow-hidden rounded-[15px] w-56`}>
                                                        <div>
                                                            <img width={`600`} height={`840`}
                                                                 className={`max-w-full h-auto`}
                                                                 src={item.imageProductName}/>
                                                        </div>
                                                        <div
                                                            className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                                            <div
                                                                className={`shop_action flex flex-col items-start relative`}>
                                                                <button
                                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                actionBtn text-dangerColor-default_3 transition duration-300 ease-in-out`}>
                                                                    <FiHeart/>
                                                                </button>
                                                                <div
                                                                    onClick={() => handleClickGoProductDetail(item.id, item.slug)}
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
                                                </div>
                                                <div className={`relative pt-[20px]`}>
                                                    <h3 className={`text-[16px] mb-[8px] font-semiBold leading-normal overflow-hidden truncate `}>{item.name}</h3>
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
                                                        className={`text-[20px] leading-normal font-semiBold text-dangerColor-default_2`}>
                                                        {item.price}
                                                    </span>
                                                </div>
                                            </div>
                                        </li>
                                    }
                                })
                            }
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ProductDetail;