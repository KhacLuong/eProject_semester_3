import React, {useEffect, useState} from 'react';
import {Swiper, SwiperSlide} from 'swiper/react';
import SwiperCore, {Pagination, Autoplay, Navigation} from "swiper";
import 'swiper/css';
import "swiper/css/pagination";
import "swiper/css/autoplay";
import parse from "html-react-parser";
import {renderStar} from "../../ultis/renderStar";
import './home.scss'
import {
    getListCategory,
    getListProductLatestReleases, getListProductLowestPrice,
    getListProductMostView,
    updateViewCountProductById
} from "../../services/apiService";
import {MdAttractions, MdKeyboardArrowRight} from 'react-icons/md'
import {HiOutlineStar, HiOutlineUsers, HiOutlineEmojiHappy} from 'react-icons/hi'
import {BsPencil, BsBook} from 'react-icons/bs'
import {GiVerticalBanner, GiChessQueen} from 'react-icons/gi'
import {TiWeatherPartlySunny} from 'react-icons/ti'
import {FiEye, FiHeart, FiShoppingBag} from "react-icons/fi";
import {BiBookAlt} from "react-icons/bi";
import {AiOutlineShoppingCart} from "react-icons/ai";

import ImageBanner3 from '../../assets/image/banner/revslider_1.png'
import ImageBanner4 from '../../assets/image/banner/revslider_book-4.png'
import ImageBanner5 from '../../assets/image/banner/revslider_book-5.png'
import ImageBanner6 from '../../assets/image/banner/h7_bn-2.png'
import ImageBanner7 from '../../assets/image/banner/h7-bn-3.png'
import ImageBanner8 from '../../assets/image/banner/h7_bn-4-1.png'
import ImageBanner9 from '../../assets/image/banner/h7_bn-5.png'
import ImageSale from '../../assets/image/sale.png'
import Book1 from '../../assets/image/books/book10.png'
import Book2 from '../../assets/image/books/book14.png'
import Book3 from '../../assets/image/books/book12.png'
import Book4 from '../../assets/image/books/book16.png'
import Book5 from '../../assets/image/books/book22.png'

import {useNavigate} from "react-router-dom";
import {TbShoppingCart} from "react-icons/tb";
import {Data} from '../Product/Data'
import {connect, useDispatch, useSelector} from "react-redux";
import {doAddToCart} from "../../redux/action/cartAction";
import {AddProductToWishList} from "../../ultis/AddProductToWishList";
import jwt_decode from "jwt-decode";

const HomePage = (props) => {
    const navigate = useNavigate();
    const [listCategory, setListCategory] = useState([])
    const [activeTitle, setActiveTitle] = useState(1)
    const {setOpen, doAddToCart} = props
    const account = useSelector(state => state.user.account);
    let userId = ''
    const [productMostView, setProductMostView] = useState([])
    const [productLatestReleases, setProductLatestReleases] = useState([])
    const [productLowPrice, setProductLowPrice] = useState([])
    const [allProduct, setAllProduct] = useState([])
    const listIconCategory = [MdAttractions, HiOutlineStar, BsPencil, BsBook, GiVerticalBanner, GiChessQueen, TiWeatherPartlySunny]
    const listTopInWeek = [
        {id: 1, img: Book1, name: 'life flight', author: 'misty figueroa', color: '#facc15'},
        {id: 2, img: Book2, name: 'goodbye again', author: 'gilberto mills', color: '#9ca3af'},
        {id: 3, img: Book3, name: 'Annie Leibovitz: Wonderland', author: 'Rita James', color: '#d97706'},
        {id: 4, img: Book4, name: 'the good egg', author: 'Arthur Gonzalez', color: '#a3e635'},
        {id: 5, img: Book5, name: 'The Bear of Byzantium', author: 'misty figueroa', color: '#60a5fa'},
    ]
    const listTotal = [
        {id: 1, name: 'total products', total: '15,254', icon: BiBookAlt, color: '#7261d4', background: '#edebfc'},
        {id: 2, name: 'authors', total: '1,254', icon: HiOutlineUsers, color: '#4dc1db', background: '#e9f9fd'},
        {
            id: 3,
            name: 'books sold',
            total: '7,589',
            icon: AiOutlineShoppingCart,
            color: '#72c949',
            background: '#e8f6e1'
        },
        {
            id: 4,
            name: 'happy customer',
            total: '97%',
            icon: HiOutlineEmojiHappy,
            color: '#fa8c17',
            background: '#fbf1e6'
        },
    ]
    const listTitle = [
        {id: 1, title: 'popular books'},
        {id: 2, title: 'on sale'},
        {id: 3, title: 'top rated'},
    ]
    const classActiveHeaderTitle = `text-blackColor after:opacity-100 after:scale-100 after:content[''] after:inline-block after:absolute after:w-full after:h-[4px] after:left-0 after:bottom-[-5px] after:bg-dangerColor-default_2 after:duration-500`;
    const classHoverHeaderTitle = `hover:after:opacity-100 hover:after:scale-100 hover:after:content[''] hover:after:inline-block hover:after:absolute hover:after:w-full hover:after:h-[4px] hover:after:left-0 hover:after:bottom-[-5px] hover:after:bg-dangerColor-default_2 hover:text-blackColor hover:after:duration-500`
    if (account.accessToken) {
        userId = jwt_decode(account.accessToken).Id;
    }

    useEffect(() => {

        const fetchData = async () => {
            await fetchListCategories()
            await fetchListProductMostView()
            await fetchListProductLatestReleases()
            await fetchListProductLowPrice()
        }
        fetchData();
    }, [])

    const fetchListCategories = async () => {
        let res = await getListCategory()
        setListCategory(res.data)
    }
    const fetchListProductMostView = async () => {
        let res = await getListProductMostView(10)
        if (res.status === true && res) {
            setProductMostView(res.data)
            setAllProduct(res.data)
        }
    }
    const fetchListProductLatestReleases = async () => {
        let res = await getListProductLatestReleases(10)
        if (res.status === true && res) {
            setProductLatestReleases(res.data)
        }
    }
    const fetchListProductLowPrice = async () => {
        let res = await getListProductLowestPrice(10)
        if (res.status === true && res) {
            setProductLowPrice(res.data)
        }
    }
    const BtnShopNow = () => (
        <button onClick={() => navigate('/products')}
                className={`text-sm bg-whiteColor text-blackColor py-4 px-6 rounded-full capitalize mt-6 hover:bg-blackColor hover:text-whiteColor duration-300 flex items-center`}>
            shop now
            <MdKeyboardArrowRight className={`ml-2 text-dangerColor-default_2`}/>
        </button>
    )
    const [hover, setHover] = useState(false);
    const [idProduct, setIdProduct] = useState(0);
    const handleOnMouseOver = (event, index) => {
        setHover(true);
        setIdProduct(index + 1);
    }
    const handleClickGoProductDetail = async (id, slug) => {
        let res = await updateViewCountProductById(id)
        if (res === 'Success') {
            navigate(`products/product-detail/${id}/${slug}`)
        }
    }
    const handleAddProductToCart = (data) => {
        doAddToCart(data)
        setOpen(true);
    }
    const handleActiveTitle = (id) => {
        setActiveTitle(id)
        if (id === 1) {
            setAllProduct(productMostView)
        } else if (id === 2) {
            setAllProduct(productLowPrice)
        } else {
            setAllProduct(productLatestReleases)
        }
    }
    return (
        <div className={`home_page`}>
            <div className={`container mx-auto xl:px-30 py-8`}>
                <div className={`grid grid-cols-5 gap-10`}>
                    <div className={`col-span-1 grid grid-rows-1 gap-10`}>
                        <div
                            className={`list_genre border-2 border-dangerColor-default_2 rounded-2xl flex flex-col items-start px-12 py-4`}>
                            <h3 className={`font-medium text-blackColor text-2xl text-left mb-4`}>Top 10 genre</h3>
                            <ul className={`w-full `}>
                                {
                                    listCategory.map((item, index) => {
                                        if (index < 10) {
                                            return <div key={index}
                                                        className={`${listCategory.length === index + 1 ? '' : 'border-b-[1px]'} py-3 text-lightColor text-sm hover:text-dangerColor-default_2 cursor-pointer duration-300 flex items-center`}>
                                                <li className={`ml-4`}>{item.name}</li>
                                            </div>
                                        }
                                    })
                                }
                            </ul>
                        </div>
                        <div className={`top_weak bg-[#f0f1f5] rounded-2xl flex flex-col items-start px-6 py-6`}>
                            <h3 className={`border-b-[1px] pb-4 font-medium text-blackColor text-xl text-left mb-4 w-full`}>Top
                                5 view this weak</h3>
                            <ul>
                                {
                                    listTopInWeek.map((item, index) => (
                                        <li title={item.name} key={index} className={`flex items-center mb-[30px]`}>
                                            <div className={`relative`}>
                                                <img alt={item.name}
                                                     className={`rounded-2xl max-w-[80px] mr-4 cursor-pointer`}
                                                     src={item.img}/>
                                                <div style={{background: item.color}}
                                                     className={`rounded-full border-2 border-whiteColor absolute top-[-8px] left-[-11px] w-[30px] h-[30px] flex items-center text-center justify-center text-xs font-semiBold`}>{item.id}</div>
                                            </div>
                                            <div className={`capitalize`}>
                                                <p className={`mb-2 line-clamp-2 text-clip text-sm font-medium cursor-pointer hover:text-dangerColor-default_2 duration-300`}>{item.name}</p>
                                                <div
                                                    className={`capitalize text-lightColor font-light text-xs cursor-pointer hover:text-dangerColor-default_2 duration-300`}>{item.author}</div>
                                                <div className={`flex items-center mt-2 ml-[-5px]`}>
                                                    {parse(renderStar(1))}
                                                    <span className={`text-xs ml-2 font-medium`}>5.0</span>
                                                    <span
                                                        className="w-1 h-1 mx-2 bg-gray-500 rounded-full dark:bg-gray-400"></span>
                                                    <a href="#"
                                                       className="text-xs font-medium text-gray-900 underline hover:no-underline duration-300 hover:text-dangerColor-default_2 dark:text-white">73
                                                        reviews</a>
                                                </div>
                                            </div>
                                        </li>
                                    ))
                                }
                            </ul>
                        </div>
                        <div className={`top_sold border-[1px] rounded-2xl flex flex-col items-start px-6 py-6`}>
                            <h3 className={`border-b-[1px] pb-4 font-medium text-blackColor text-xl text-left mb-4 w-full`}>Top
                                5 sold this weak</h3>
                            <ul>
                                {
                                    listTopInWeek.map((item, index) => (
                                        <li key={index}
                                            className={`flex items-center mb-[30px] pb-6 ${listTopInWeek.length === index + 1 ? '' : 'border-b-[1px]'}`}>
                                            <div className={`relative`}>
                                                <img alt={item.name}
                                                     className={`rounded-2xl max-w-[80px] mr-4 cursor-pointer`}
                                                     src={item.img}/>
                                                <div style={{background: item.color}}
                                                     className={`rounded-full border-2 border-grayColor absolute top-[-8px] left-[-11px] w-[30px] h-[30px] flex items-center text-center justify-center text-xs font-semiBold`}>{item.id}</div>
                                            </div>
                                            <div className={`capitalize`}>
                                                <p className={`line-clamp-2 text-clip text-sm font-medium cursor-pointer hover:text-dangerColor-default_2 duration-300`}>{item.name}</p>
                                                <div
                                                    className={`capitalize text-lightColor font-light text-xs cursor-pointer hover:text-dangerColor-default_2 duration-300`}>{item.author}</div>
                                                <div className={`flex items-start mt-2`}>
                                                    <div className={`flex items-start mr-2`}>
                                                        <FiEye className={`text-blue-600 text-[15px]`}/>
                                                        <span className={`text-xs ml-1`}>2032</span>
                                                    </div>
                                                    <div className={`flex items-start `}>
                                                        <FiShoppingBag
                                                            className={`text-dangerColor-default_2 text-[15px]`}/>
                                                        <span className={`text-xs ml-1`}>239</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    ))
                                }
                            </ul>
                        </div>
                        <div className={`w-full`}>
                            <a onClick={() => navigate('/products')}
                               className={`group/image relative flex overflow-hidden duration-300 rounded-[20px] cursor-pointer w-full`}>
                                <div className={`absolute top-0 left-0 right-0 bottom-0 duration-300 rounded-[20px]`}>
                                    <div
                                        className={`z-0 group-hover/image:scale-110 duration-300 absolute top-0 left-0 right-0 bottom-0 w-full h-full`}
                                        style={{
                                            backgroundImage: `url(${ImageSale})`,
                                            backgroundSize: 'cover',
                                            backgroundPosition: 'center',
                                            backgroundRepeat: 'no-repeat'
                                        }}>
                                    </div>
                                </div>
                                <div
                                    className={`z-10 min-h-[400px] w-full text-center pb-[40px] justify-center flex items-end`}>
                                    <div className={`text-center block`}>
                                        <div
                                            className={`mb-[10px] uppercase text-blackColor font-semiBold leading-loose text-[12px]`}>
                                            <span
                                                className={`relative duration before:content[''] before:inline-block before:absolute before:w-0 before:bg-blackColor before:h-[2px] before:left-auto before:right-0 before:duration-300 before:bottom-[-2px] group-hover/image:before:w-full group-hover/image:before:duration-300 group-hover/image:before:left-0 group-hover/image:before:right-auto`}>best seller books</span>
                                        </div>
                                        <h3 className={`capitalize mb-[10px] font-semiBold  text-dangerColor-default_2 text-[36px] tracking-tighter`}>sale
                                            50%</h3>
                                        <div
                                            className={`hover:text-dangerColor-default_2 duration-300 text-[14px] capitalize font-semiBold flex items-center justify-center `}>
                                            shop now <MdKeyboardArrowRight className={`ml-2`}/>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div>
                            {
                                listTotal.map((item, index) => {
                                    let Icon = item.icon
                                    return <div key={index}
                                                className={`total_product border-[1px] rounded-2xl flex justify-start items-start px-6 py-6 mb-6`}>
                                        <div className={`flex items-center group/total `}>
                                            <div style={{color: item.color, backgroundColor: item.background}}
                                                 className={`group-hover/total:text-[36px] duration-300 text-[40px] mr-8 bg-[#e8f6e1] w-[80px] h-[80px] rounded-full flex items-center justify-center`}>
                                                <Icon/>
                                            </div>
                                            <div className={`uppercase`}>
                                                <h2 className={`font-semiBold text-2xl`}>{item.total}</h2>
                                                <span className={`text-lightColor text-xs`}>{item.name}</span>
                                            </div>
                                        </div>
                                    </div>
                                })
                            }
                        </div>
                    </div>
                    <div className={`col-span-4`}>
                        <div className={`top_banner pb-10`}>
                            <Swiper
                                autoplay={{
                                    delay: 10000,
                                    disableOnInteraction: false,
                                }}
                                pagination={{
                                    clickable: true,
                                    dynamicBullets: true,

                                }}
                                navigation={true}
                                modules={[Autoplay, Pagination, Navigation]}
                                className="mySwiper rounded-2xl duration-300"
                            >
                                <SwiperSlide>
                                    <div
                                        style={{
                                            backgroundImage: `url(${ImageBanner3})`,
                                            backgroundSize: 'cover',
                                            backgroundPosition: 'center'
                                        }}
                                        className={`h-full rounded-2xl flex px-20 py-36 flex items-center justify-end flex`}>
                                        <div className={`text-darkColor uppercase font-medium w-2/5`}>
                                            <div className={`text-sm mb-6`}>a sale for the page</div>
                                            <div className={`text-4xl font-semiBold mb-4`}>50% off hundreds of books
                                            </div>
                                            <div className={`w-2/3`}>
                                                <span className={`text-dangerColor-default_2 ml-2`}>
                                                     Online And In Stores Only
                                                </span>
                                            </div>
                                            <BtnShopNow/>
                                        </div>
                                    </div>
                                </SwiperSlide>
                                <SwiperSlide>
                                    <div
                                        className={`rounded-2xl bg-[#223d4d] flex px-20 py-20 items-center justify-between`}>
                                        <div className={`text-whiteColor uppercase font-medium`}>
                                            <div className={`text-sm mb-6`}>special offer</div>
                                            <div className={`text-4xl font-semiBold mb-4`}>the best book of 2022</div>
                                            <div className={`w-2/3`}><span
                                                className={`text-dangerColor-default_2`}>
                                                10%</span> off when you spend
                                                $59 or more with code:
                                                <span className={`text-dangerColor-default_2 ml-2`}>
                                                     savemore
                                                </span>
                                            </div>
                                            <BtnShopNow/>
                                        </div>
                                        <div className={`flex items-end justify-end max-w-[446px] min-w-[446px]`}>
                                            <img className={`w-full`} src={ImageBanner5}/>
                                        </div>
                                    </div>
                                </SwiperSlide>
                                <SwiperSlide>
                                    <div
                                        className={`rounded-2xl bg-[#fcedec] flex px-20 py-20 items-center justify-center text-center justify-between`}>
                                        <div
                                            className={`text-center items-center justify-center flex-col flex text-whiteColor uppercase font-medium`}>
                                            <div className={`text-sm mb-6 text-lightColor`}>editor choice</div>
                                            <div className={`text-6xl text-blackColor font-semiBold mb-4`}>top 10 books
                                                of year
                                            </div>
                                            <div className={`w-2/3 text-darkColor`}><span
                                                className={`text-dangerColor-default_2`}>
                                                10%</span> off when you spend
                                                $59 or more with code:
                                                <span className={`text-dangerColor-default_2 ml-2`}>
                                                     savemore
                                                </span>
                                            </div>
                                            <BtnShopNow/>
                                        </div>
                                        <div className={`flex items-end justify-end max-w-[446px] min-w-[446px]`}>
                                            <img className={`w-full`} src={ImageBanner4}/>
                                        </div>
                                    </div>
                                </SwiperSlide>
                            </Swiper>
                        </div>
                        <div className={`feature_books pb-10`}>
                            <div className={`relative`}>
                                <div
                                    className={`right-0 top-[-12px] absolute uppercase text-sm text-whiteColor cursor-pointer`}>
                                    <a className={`leading-tight flex font-semiBold items-center bg-dangerColor-default_2 rounded-full py-4 px-8 hover:bg-dangerColor-hover_2`}>view
                                        all <MdKeyboardArrowRight className={`ml-2`}/></a>
                                </div>
                                <div className={`container`}>
                                    <div className={`tabs`}>
                                        <div
                                            className={`flex items-center justify-start after:content-[''] after:inline-block after:h-[1px] after:w-[35%] after:ml-[65px] after:bg-borderColor after:mr-[200px]`}>
                                            <div
                                                className={`text-center flex flex-wrap capitalize font-medium text-2xl text-lightColor`}>
                                                {
                                                    listTitle.map((item, index) => {
                                                        return <div
                                                            className={`${index + 1 === activeTitle ? classActiveHeaderTitle : 'after:scale-0 after:opacity-0'} ${classHoverHeaderTitle} duration-500 relative mr-[25px] cursor-pointer`}
                                                            onClick={() => handleActiveTitle(item.id)} key={index}>
                                                            {item.title}
                                                        </div>
                                                    })
                                                }
                                            </div>
                                        </div>
                                        <div className={`content`}>
                                            <div>
                                                <ul className={`mt-8 mb-0 clear-both grid grid-cols-5 shadow-md rounded-[15px]`}>
                                                    {
                                                        allProduct.map((item, index) => {
                                                            return <li key={index} className={`mx-2`}>
                                                                <div
                                                                    className={`px-[15px] mb-[60px] w-full rounded-t-[15px] duration-300 shadow-inner cursor-pointer`}>
                                                                    <div onMouseLeave={() => setHover(false)}
                                                                         className={`flex relative items-center flex-col`}>
                                                                        <div
                                                                            onMouseOver={(e) => handleOnMouseOver(e, index)}
                                                                            className={`overflow-hidden rounded-[15px] w-56`}>
                                                                            <div>
                                                                                <img width={`600`} height={`840`}
                                                                                     className={`max-w-full h-auto p-2 rounded-[15px]`}
                                                                                     src={item.imageProductPath}
                                                                                     onClick={() => navigate(`products/product-detail/${item.id}/${item.slug}`)}/>
                                                                            </div>
                                                                            <div
                                                                                className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                                                                <div
                                                                                    className={`shop_action flex flex-col items-start relative`}>
                                                                                    <button
                                                                                        onClick={() => AddProductToWishList(userId, item.id)}
                                                                                        className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8'} 
                                                    actionBtn text-dangerColor-default_3 duration-300`}>
                                                                                        <FiHeart/>
                                                                                    </button>
                                                                                    <div
                                                                                        onClick={() => handleClickGoProductDetail(item.id, item.slug)}
                                                                                        className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible' + ' translate-x-0' : 'opacity-0 translate-x-8' + ' invisible'} actionBtn delay-100 duration-300`}>
                                                                                        <FiEye/>
                                                                                    </div>
                                                                                    <button
                                                                                        onClick={() => handleAddProductToCart(item)}
                                                                                        className={`${hover && idProduct === index + 1 ? 'opacity-1' + ' visible translate-x-0' : 'opacity-0' + ' translate-x-8 invisible'} actionBtn delay-200 duration-300`}>
                                                                                        <TbShoppingCart/></button>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div className={`relative pt-[20px]`}>
                                                                        <h3 onClick={() => navigate(`products/product-detail/${item.id}/${item.slug}`)}
                                                                            className={`text-[16px] mb-[8px] font-semiBold leading-normal overflow-hidden truncate hover:text-dangerColor-default_2`}>{item.name}</h3>
                                                                        <div
                                                                            className={`flex items-center mb-[10px] text-[12px] font-semiBold`}>
                                                                            <div
                                                                                className={`flex items-center mr-[5px] overflow-hidden`}>
                                                                                {parse(renderStar(5))}
                                                                            </div>
                                                                            <span>5</span>
                                                                        </div>
                                                                        <div
                                                                            className={`text-[12px] leading-none text-lightColor font-semibold mb-[12px]`}>
                                                                            Author: <span
                                                                            className={`font-medium`}>{item.author}</span>
                                                                        </div>
                                                                        <div
                                                                            className={`text-[12px] leading-none text-lightColor font-semibold mb-[12px]`}>
                                                                            Category: <span
                                                                            className={`font-medium`}>{item.category}</span>
                                                                        </div>
                                                                        <span
                                                                            className={`text-2xl leading-normal font-medium text-dangerColor-default_2`}>
                                                                                ${item.price}
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
                            </div>
                        </div>
                        <div className={`two_advertisement pb-10`}>
                            <div className={`grid grid-cols-2 gap-8`}>
                                <div
                                    className={`col-span-1 rounded-[20px] relative overflow-hidden duration-300 min-h-[260px]`}>
                                    <div className={`group/image`}>
                                        <div
                                            className={`rounded-[20px] absolute top-0 left-0 right-0 bottom-0 duration-300 w-full h-full min-h-[220px] group-hover/image:scale-110`}
                                            style={{
                                                backgroundImage: `url(${ImageBanner7})`,
                                                backgroundSize: 'cover',
                                                backgroundRepeat: 'no-repeat',
                                                backgroundPosition: 'center',
                                                zIndex: 1
                                            }}>
                                        </div>
                                        <div
                                            className={`capitalize text-whiteColor z-10 absolute text-left py-[30px] px-[40px] top-6`}>
                                            <div>
                                                <span
                                                    className={`duration-300 relative font-semiBold mb-[5px] before:content[''] before:inline-block before:absolute before:w-0 before:bg-whiteColor before:h-[2px] before:left-auto before:right-0 before:duration-300 before:bottom-[-2px] group-hover/image:before:w-full group-hover/image:before:duration-300 group-hover/image:before:left-0 group-hover/image:before:right-auto`}>up to 20% off</span>
                                                <h3 className={`w-3/5 font-semiBold h-[100px] text-[30px]`}>enjoy the
                                                    weekend with good books</h3>
                                                <button onClick={() => navigate('/products')}
                                                        className={`capitalize cursor-pointer duration-300 font-medium hover:text-blackColor flex items-center`}>shop
                                                    now <MdKeyboardArrowRight className={`ml-1`}/></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div
                                    className={`col-span-1 rounded-[20px] relative overflow-hidden duration-300 min-h-[260px]`}>
                                    <div className={`group/image`}>
                                        <div
                                            className={`rounded-[20px] absolute top-0 left-0 right-0 bottom-0 duration-300 w-full h-full min-h-[220px] group-hover/image:scale-110`}
                                            style={{
                                                backgroundImage: `url(${ImageBanner6})`,
                                                backgroundSize: 'cover',
                                                backgroundRepeat: 'no-repeat',
                                                backgroundPosition: 'center',
                                                zIndex: 1
                                            }}>
                                        </div>
                                        <div
                                            className={`capitalize text-whiteColor z-10 absolute text-left py-[30px] px-[40px] top-6`}>
                                            <div>
                                                <span
                                                    className={`duration-300 relative font-semiBold mb-[5px] before:content[''] before:inline-block before:absolute before:w-0 before:bg-whiteColor before:h-[2px] before:left-auto before:right-0 before:duration-300 before:bottom-[-2px] group-hover/image:before:w-full group-hover/image:before:duration-300 group-hover/image:before:left-0 group-hover/image:before:right-auto`}>special offer</span>
                                                <div className={`h-[100px]`}>
                                                    <h3 className={`w-full font-semiBold mb-[5px] text-[35px]`}>20%
                                                        off</h3>
                                                    <div className={`text-sm`}>best bookshelf in town</div>
                                                </div>
                                                <button onClick={() => navigate('/products')}
                                                        className={`capitalize cursor-pointer duration-300 font-medium hover:text-blackColor flex items-center`}>shop
                                                    now <MdKeyboardArrowRight className={`ml-1`}/></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div
                            className={`one_advertisement rounded-[20px] mb-10 px-[60px] py-[60px] w-full min-h-[200px] `}
                            style={{
                                backgroundImage: `url(${ImageBanner8})`,
                                backgroundSize: 'cover',
                                backgroundRepeat: 'no-repeat',
                                backgroundPosition: 'center right',
                                zIndex: 1
                            }}>
                            <div className={`flex mr-auto ml-auto relative`}>
                                <div className={`flex w-[55%]`}></div>
                                <div className={`w-[35%]`}>
                                    <div
                                        className={`flex justify-center items-start flex-col content-center text-whiteColor capitalize`}>
                                        <div className={`font-semiBold text-xl mb-2`}>children's books</div>
                                        <div className={`font-semiBold text-4xl mb-2`}>sale 25% off</div>
                                        <div className={`normal-case text-sm`}>It all begins with a great book!</div>
                                    </div>
                                </div>
                                <div className={`w-[20%] text-whiteColor text-sm`}>
                                    <div className={`mb-8`}>Online Book's Store!</div>
                                    <button onClick={() => navigate('/products')}
                                            className={`bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 rounded-full px-8 py-4 font-semiBold flex items-center duration-300`}>Shop
                                        Now <MdKeyboardArrowRight className={`ml-1`}/></button>
                                </div>
                            </div>
                        </div>
                        <div className={`top_trending pb-10`}>
                            <div className={` grid grid-cols-3 gap-10`}>
                                <div className={`col-span-1`}>
                                    <div className={`w-full`}>
                                        <h5 className={`capitalize py-[10px] mb-[10px] font-semiBold text-xl clear-both border-b-[1px] border-borderColor`}>
                                            top selling
                                        </h5>
                                        <div className={``}>
                                            <ul className={`mb-0 clear-both flex flex-wrap grid grid-rows-1`}>
                                                {
                                                    productLowPrice.map((item, index) => {
                                                        if (index >= 0 && index < 4) {
                                                            return <li key={index} title={item.name}
                                                                       onClick={() => navigate(`products/product-detail/${item.id}/${item.slug}`)}
                                                                       className={`row-span-1 last:border-0 border-b-[1px]`}>
                                                                <div className={`py-[15px] flex items-center`}>
                                                                    <div className={`left max-w-[130px] mr-[30px]`}>
                                                                        <a className={`cursor-pointer`}>
                                                                            <img
                                                                                className={`rounded-[16px]`}
                                                                                src={item.imageProductPath}/>
                                                                        </a>
                                                                    </div>
                                                                    <div className={`right`}>
                                                                        <h3 className={`cursor-pointer text-[16px] mb-[8px] font-semiBold line-clamp-1 leading-normal hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a>{item.name}</a>
                                                                        </h3>
                                                                        <div className={`flex items-center mb-[10px] `}>
                                                                            {parse(renderStar(5))}
                                                                            <span
                                                                                className={`ml-2 text-[12px] font-semiBold`}>5</span>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Author: <span
                                                                                className={`font-normal`}>{item.author}</span>
                                                                            </a>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Category: <span
                                                                                className={`font-normal`}>{item.category}</span>
                                                                            </a>
                                                                        </div>
                                                                        <span
                                                                            className={`font-medium text-[20px] text-dangerColor-default_2`}>$ {item.price}</span>
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
                                <div className={`col-span-1`}>
                                    <div>
                                        <h5 className={`capitalize py-[10px] mb-[10px] font-semiBold text-xl clear-both border-b-[1px] border-borderColor`}>trending
                                            products</h5>
                                        <div className={``}>
                                            <ul className={`mb-0 clear-both flex flex-wrap grid grid-rows-1`}>
                                                {
                                                    productMostView.map((item, index) => {
                                                        if (index >= 0 && index < 4) {
                                                            return <li key={index} title={item.name}
                                                                       onClick={() => navigate(`products/product-detail/${item.id}/${item.slug}`)}>
                                                                <div
                                                                    className={`border-borderColor py-[15px] flex items-center`}>
                                                                    <div className={`left max-w-[130px] mr-[30px]`}>
                                                                        <a className={`cursor-pointer`}><img
                                                                            className={`rounded-[16px]`}
                                                                            src={item.imageProductPath}/></a>
                                                                    </div>
                                                                    <div className={`right`}>
                                                                        <h3 className={`cursor-pointer text-[16px] mb-[8px] font-semiBold line-clamp-1 leading-normal hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a>{item.name}</a>
                                                                        </h3>
                                                                        <div className={`flex items-center mb-[10px] `}>
                                                                            {parse(renderStar(5))}
                                                                            <span
                                                                                className={`ml-2 text-[12px] font-semiBold`}>5</span>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Author: <span
                                                                                className={`font-normal`}>{item.author}</span>
                                                                            </a>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Category: <span
                                                                                className={`font-normal`}>{item.category}</span>
                                                                            </a>
                                                                        </div>
                                                                        <span
                                                                            className={`font-medium text-[20px] text-dangerColor-default_2`}>$ {item.price}</span>
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
                                <div className={`col-span-1`}>
                                    <div>
                                        <h5 className={`capitalize py-[10px] mb-[10px] font-semiBold text-xl clear-both border-b-[1px] border-borderColor`}>recently
                                            added</h5>
                                        <div className={``}>
                                            <ul className={`mb-0 clear-both flex flex-wrap grid grid-rows-1`}>
                                                {
                                                    productLatestReleases.map((item, index) => {
                                                        if (index >= 0 && index < 4) {
                                                            return <li key={index} title={item.name}
                                                                       onClick={() => navigate(`products/product-detail/${item.id}/${item.slug}`)}>
                                                                <div className={`py-[15px] flex items-center`}>
                                                                    <div className={`left max-w-[130px] mr-[30px]`}>
                                                                        <a className={`cursor-pointer`}><img
                                                                            className={`rounded-[16px]`}
                                                                            src={item.imageProductPath}/></a>
                                                                    </div>
                                                                    <div className={`right`}>
                                                                        <h3 className={`cursor-pointer text-[16px] mb-[8px] font-semiBold line-clamp-1 leading-normal hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a>{item.name}</a>
                                                                        </h3>
                                                                        <div className={`flex items-center mb-[10px] `}>
                                                                            {parse(renderStar(5))}
                                                                            <span
                                                                                className={`ml-2 text-[12px] font-semiBold`}>5</span>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Author: <span
                                                                                className={`font-normal`}>{item.author}</span>
                                                                            </a>
                                                                        </div>
                                                                        <div
                                                                            className={`cursor-pointer text-[12px] leading-none mb-[12px] font-normal text-lightColor hover:text-dangerColor-default_2 duration-300`}>
                                                                            <a className={`font-medium`}>Category: <span
                                                                                className={`font-normal`}>{item.category}</span>
                                                                            </a>
                                                                        </div>
                                                                        <span
                                                                            className={`font-medium text-[20px] text-dangerColor-default_2`}>$ {item.price}</span>
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
                        </div>
                        <div className={`your_offer duration-300 px-[80px] py-[40px] rounded-[16px] flex items-center`}
                             style={{
                                 backgroundImage: `url(${ImageBanner9})`,
                                 backgroundSize: 'cover',
                                 backgroundRepeat: 'no-repeat',
                                 backgroundPosition: 'center',
                             }}>
                            <div className={`w-[40%] relative`}>
                                <div className={`flex flex-col`}>
                                    <div className={`w-full`}><h4
                                        className={`mb-[5px] capitalize text-blackColor font-semiBold clear-bold text-[26px]`}>get
                                        10% off your order</h4></div>
                                    <div className={`w-full`}>
                                        <div className={`text-[#444444] text-sm`}>
                                            Enter your email and receive a 10% discount on your next order!
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className={`w-[60%] relative`}>
                                <div className={`content-center items-center flex`}>
                                    <div className={`w-full`}>
                                        <form className={`relative`}>
                                            <div className={`flex items-center`}>
                                                <input
                                                    className={`py-[14px] pr-[140px] pl-[25px] w-full rounded-full text-[13px] leading-loose text-blackColor bg-whiteColor border-[1px] border-borderColor`}
                                                    type={`email`} name={`email`} placeholder={`Your email address`}
                                                    required={true}/>
                                                <button
                                                    className={`absolute right-0 flex items-center justify-center text-[14px] font-bold py-[19px] px-[20px] rounded-full border-0 bg-dangerColor-default_2 duration-300 text-whiteColor hover:bg-dangerColor-hover_2`}>
                                                    Subscribe <MdKeyboardArrowRight className={`ml-1`}/>
                                                </button>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
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
export default connect(null, mapDispatchToProps)(HomePage);