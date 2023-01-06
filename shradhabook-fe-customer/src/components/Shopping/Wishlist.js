import React from 'react';
import Banner from "../Layouts/Banner/Banner";
import Image1 from "../../assets/image/books/book12.png"
import Image2 from "../../assets/image/books/book8.png"
import {BsFillCartFill, BsFillTrashFill} from "react-icons/bs";
import {useNavigate} from "react-router-dom";
const Wishlist = () => {
    const navigate = useNavigate();

    const handleRemoveProduct = () => {
        console.log(1)
    }
    const handleGoToCategory = () => {
        const id = 1
        const slug = 'abcd'
        navigate(`/categories/${id}/${slug}`)
    }

    return (
        <div className={`wishlist_page`}>
            <Banner bannerTitle={`Wish list`}/>
            <div className={`wishlist_container container mx-auto xl:px-30 py-20`}>
                <div className={`wishlist_content`}>
                    <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                        <thead
                            className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" className="px-2 py-2 w-40">
                                Product Image
                            </th>
                            <th scope="col" className="px-6 py-3">
                                Info
                            </th>
                            <th scope="col" className="px-6 py-3">
                                <span className="sr-only">Action</span>
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                            <td scope="row"
                                className="px-2 py-2 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <img onClick={() => navigate('/products/product-detail/1/ad')} className={`cursor-pointer rounded-xl`} src={Image1}/>
                            </td>
                            <td scope="row"
                                className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <div>
                                    <h3 onClick={() => navigate('/products/product-detail/1/ad')} className={`font-semiBold cursor-pointer text-xl`}>Apple MacBook Pro 17"</h3>
                                    <div className={`flex items-center font-light text-xs mb-2`}>
                                        <div className={`mr-4`}>code: <span className={`hover:text-dangerColor-default_2 hover:underline cursor-pointer`}> BD8890123</span> </div>
                                        <div>category: <span onClick={() => handleGoToCategory()} className={`hover:text-dangerColor-default_2 hover:underline cursor-pointer`}> Book 1</span> </div>
                                    </div>
                                    <div className={`font-medium text-dangerColor-default_2 text-2xl`}>$ 9999</div>
                                    <div className={`font-light text-xs`}>January 2, 2023</div>
                                </div>

                            </td>
                            <td className="px-6 py-4 text-left">
                                <div className={`flex items-center`}>
                                    <div
                                        className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-lime-600 hover:bg-lime-700 cursor-pointer`}>
                                        <BsFillCartFill className={`mr-2`}/>
                                        Add to cart
                                    </div>
                                    <div onClick={() => handleRemoveProduct()}
                                        className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 cursor-pointer`}>
                                        <BsFillTrashFill className={`mr-2`}/>
                                        Remove
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                            <td scope="row"
                                className="px-2 py-2 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <img onClick={() => navigate('/products/product-detail/1/ad')} className={`cursor-pointer rounded-xl`} src={Image2}/>
                            </td>
                            <td scope="row"
                                className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <div>
                                    <h3 onClick={() => navigate('/products/product-detail/1/ad')} className={`font-semiBold cursor-pointer text-xl`}>Apple MacBook Pro 17"</h3>
                                    <div className={`flex items-center font-light text-xs mb-2`}>
                                        <div className={`mr-4`}>code: <span className={`hover:text-dangerColor-default_2 hover:underline cursor-pointer`}> BD8890123</span> </div>
                                        <div>category: <span className={`hover:text-dangerColor-default_2 hover:underline cursor-pointer`}> Book 1</span> </div>
                                    </div>
                                    <div className={`font-medium text-dangerColor-default_2 text-2xl`}>$ 9999</div>
                                    <div className={`font-light text-xs`}>January 2, 2023</div>
                                </div>

                            </td>
                            <td className="px-6 py-4 text-left">
                                <div className={`flex items-center`}>
                                    <div
                                        className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-lime-600 hover:bg-lime-700 cursor-pointer`}>
                                        <BsFillCartFill className={`mr-2`}/>
                                        Add to cart
                                    </div>
                                    <div
                                        className={`flex justify-center items-center text-[14px] leading-tight font-semiBold mt-[10px] mr-[15px] mb-[10px] py-[17px] px-[32px] border-0 rounded-full text-whiteColor duration-300 bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 cursor-pointer`}>
                                        <BsFillTrashFill className={`mr-2`}/>
                                        Remove
                                    </div>
                                </div>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default Wishlist;