import React, {useState} from 'react';
import Banner from '../Layouts/Banner/Banner';
import {Link} from 'react-router-dom';
import Slider from '@mui/material/Slider';
import {AiOutlineMinus} from 'react-icons/ai'
import {BsFillGridFill} from 'react-icons/bs'
import {FaThList} from 'react-icons/fa'
import './Product.scss'
import parse from 'html-react-parser';

const ListProduct = () => {
    const optionSort = [
        {value: '', text: 'Default sorting'},
        {value: 'popularity', text: 'Sort by popularity'},
        {value: 'rating', text: 'Sort by average rating'},
        {value: 'date', text: 'Sort by latest'},
        {value: 'price', text: 'Sort by price: low to high'},
        {value: 'price-desc', text: 'Sort by price: high to low'},
    ]
    const optionQuantity = [
        {value: 'six', text: '6'},
        {value: 'nine', text: '9'},
        {value: 'twelve', text: '12'},
        {value: 'fifteen', text: '15'},
        {value: 'eighteen', text: '18'},
    ]
    const stars = [{
        star: 5, count: 20, path: ''
    }, {
        star: 4, count: 12, path: ''
    }, {
        star: 3, count: 16, path: ''
    }, {
        star: 2, count: 6, path: ''
    }, {
        star: 1, count: 2, path: ''
    },]
    const [price, setPrice] = useState([0, 1000]);
    const [selectedSort, setSelectedSort] = useState(optionSort[0].value);
    const [selectedQuantity, setSelectedQuantity] = useState(optionQuantity[2].value);
    const minDistance = 0;

    const listProducts = [
        {
            imgURL: '',
            name: 'Blood on the Snow',
            author: 'Rex Rios',
            price: '216.99'
        }
    ]

    const renderStar = (count_of_star) => {
        let data = []
        for (let i = 1; i <= count_of_star; i++) {
            data.push(`<svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor" viewBox="0 0 20 20"
                         xmlns="http://www.w3.org/2000/svg">
                    <title>Rating star</title>
                    <path
                            d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z">
                    </path>
                </svg>`)
        }
        return data.join('')

    }
    const handleUpdatePrice = (e, data, activeThumb) => {
        if (!Array.isArray(data)) {
            return;
        }
        if (activeThumb === 0) {
            setPrice([Math.min(data[0], data[1] - minDistance), data[1]]);
        } else {
            setPrice([price[0], Math.max(data[1], price[0] + minDistance)]);
        }
    }

    const handleFilterByPrice = () => {

    }

    const handleChangeSort = event => {
        setSelectedSort(event.target.value);
    };
    const handleChangeQuantity = event => {
        setSelectedQuantity(event.target.value);
    };
    return (<div className={`product_page`}>
        <Banner bannerTitle={`product`}/>
        <div className={`product_content container mx-auto xl:px-30 grid grid-cols-4 gap-8 py-14`}>
            <div className={`product_filter`}>
                <div className={`product_genre rounded-2xl border-[1px] mb-8`}>
                    <div
                        className={`text-lg title border-b-[1px] mx-auto px-10 py-4 text-[#000000] font-medium leading-normal`}>Genre
                    </div>
                    <div className={`mx-auto px-10 py-4`}>
                        <ul className={`text-[#444444]`}>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className={`product_author rounded-2xl border-[1px] mb-8`}>
                    <div
                        className={`text-lg title border-b-[1px] mx-auto px-10 py-4 text-[#000000] font-medium  leading-normal`}>Authors
                    </div>
                    <div className={`mx-auto px-10 py-4`}>
                        <ul className={`text-[#444444]`}>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                            <li className={`transition duration-300 mb-1 text-sm leading-normal flex items-center justify-between font-light hover:text-dangerColor-default_2`}>
                                <Link to={'/'}
                                      className={`flex items-center before:text-xl before:content-['☐'] before:mr-[8px]`}>
                                    Action &
                                    Adventure
                                </Link>
                                <div>(1)</div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className={`product_filter_price rounded-2xl border-[1px] mb-8`}>
                    <div
                        className={`text-lg title border-b-[1px] mx-auto px-10 py-4 text-[#000000] font-medium leading-normal`}>Filter
                        By Price
                    </div>
                    <div className={`mx-auto px-10 pt-4`}>
                        <Slider
                            value={price}
                            onChange={(e, data, activeThumb) => handleUpdatePrice(e, data, activeThumb)}
                            getAriaLabel={() => 'Minimum distance'}
                            disableSwap
                            min={0}
                            max={1000}
                            className={`text-black`}
                            valueLabelDisplay="off"
                            sx={{
                                color: '#000', '& .MuiSlider-rail': {
                                    background: '#999', height: '2px'
                                }, '& .MuiSlider-track': {
                                    borderRadius: '16px', background: '#000', height: '2px'
                                }, '& .MuiSlider-thumb': {
                                    background: '#fff', width: '13px', height: '13px', border: '2px solid #000'
                                },
                            }}
                        />
                        <div className={`text-[#999999] leading-normal text-xs font-light flex items-center`}>
                            Price:
                            <div className={`flex items-center text-black text-sm`}>
                                <span className={`mx-1`}>${price[0]}</span><AiOutlineMinus/> <span
                                className={`mx-1`}>${price[1]}</span>
                            </div>
                        </div>

                    </div>
                    <button onClick={handleFilterByPrice}
                            className={`uppercase mx-auto px-10 pb-4 pt-4 underline text-xs font-semibold hover:text-dangerColor-default_2 transition duration-300 tracking-wider`}>filter
                    </button>
                </div>
                <div className={`product_rating rounded-2xl border-[1px] mb-8`}>
                    <div
                        className={`text-lg title border-b-[1px] mx-auto px-10 py-4 text-[#000000] font-medium leading-normal`}>Review
                        ratings
                    </div>
                    <div className={`mx-auto px-10 py-4`}>
                        {stars.map((item, index) => {
                            return <div className="flex items-center" key={index}>
                                {parse(renderStar(item.star))}
                                <span className="w-1 h-1 mx-3 bg-gray-500 rounded-full dark:bg-gray-400"></span>
                                <a href="#"
                                   className="text-sm font-medium text-gray-900 underline hover:no-underline hover:text-dangerColor-default_2 dark:text-white transition duration-300">
                                    {item.count} products
                                </a>
                            </div>
                        })}
                    </div>
                </div>
            </div>
            <div className={`product_list col-span-3`}>
                <div className={`flex items-start justify-between border-b-[1px] py-2`}>
                    <div className={`flex items-center`}>
                        <BsFillGridFill title={`Grid View`} className={`cursor-pointer mr-2 hover:text-black`}/>
                        <FaThList title={`List View`} className={`cursor-pointer text-lightColor hover:text-black`}/>
                    </div>
                    <div className={`flex items-center`}>
                        <div>
                            <select value={selectedSort} onChange={handleChangeSort}
                                    className="text-right block py-0 pr-[30px] text-sm leading-6 text-gray-500 bg-transparent border-0  appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer ">
                                {optionSort.map((option, index) => (
                                    <option key={index} value={option.value}>
                                        {option.text}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className={`relative flex items-center pl-[19px] ml-[14px] after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-0 after:top-[10%] `}>
                            <label htmlFor={`per_page`} className={`text-lightColor font-normal text-sm`}>Show</label>
                            <select name={`per_page`} id={`per_page`} value={selectedQuantity}
                                    onChange={handleChangeQuantity}
                                    className="py-0 text-right block text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer">
                                {optionQuantity.map((option, index) => (
                                    <option key={index} value={option.value}>
                                        {option.text}
                                    </option>
                                ))}
                            </select>
                        </div>
                    </div>
                </div>
                <div className={``}>
                    <div className={`render_product grid grid-cols-3 gap-4`}>
                        <div className={`row-span-2`}>sdasd</div>
                        <div className={``}>sdasd</div>
                        <div className={``}>sdasd</div>
                        <div className={``}>sdasd</div>
                        <div className={``}>sdasd</div>
                        <div className={``}>sdasd</div>
                    </div>
                    <div className={`paginate`}>

                    </div>
                </div>
            </div>
        </div>
    </div>);
};

export default ListProduct;