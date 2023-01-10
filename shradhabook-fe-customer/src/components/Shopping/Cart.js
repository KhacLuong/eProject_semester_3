import React, {useEffect, useState} from 'react';
import Banner from "../Layouts/Banner/Banner";
import {useSelector} from "react-redux";
import CartItem from "./CartItem";
import {useNavigate} from "react-router-dom";

const Cart = () => {
    const [listProduct, setListProduct] = useState([])
    const [total, setTotal] = useState(0)
    const [totalItem, setTotalItem] = useState(0)
    const shop = useSelector(state => state.cart)
    const navigate = useNavigate();

    useEffect(() => {
        let price = 0;
        let items = 0;
        shop.cart.forEach(item => {
            items += item.qty
            price += item.price * item.qty
        })

        setTotal(price)
        setTotalItem(items)
        setListProduct(shop.cart)
    }, [shop.cart, total, totalItem, setTotal, setTotalItem])

    const handleViewCheckout = (id) => {
        navigate(`/user/checkout/${id}`)
    }
    return (
        <div className={`cart_page`}>
            <Banner bannerTitle={`cart`}/>
            <div className={`container mx-auto xl:px-30 py-8`}>
                <div className="relative mb-1">
                    <div className={`grid grid-cols-3 gap-10 h-[600px]`}>
                        <div className={`col-span-2 sm:rounded-lg shadow-md overflow-y-auto h-full`}>
                            <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                                <thead
                                    className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                                <tr>
                                    <th scope="col" className="px-2 py-2 w-40">
                                        <span className="sr-only">Image</span>
                                    </th>
                                    <th scope="col" className="px-6 py-3 w-60">
                                        Product
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Price
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Quantity
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Subtotal
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        <span className="sr-only">Edit</span>
                                    </th>
                                </tr>
                                </thead>
                                <tbody>
                                {
                                    listProduct.map((item, index) => (
                                        <CartItem key={index} item={item} />
                                    ))
                                }
                                </tbody>
                            </table>
                        </div>
                        <div className={`col-span-1`}>
                            <div
                                className={`pb-[40px] pt-[30px] pl-[30px] pr-[30px] border-[1px] border-borderColor relative sm:rounded-lg shadow-md h-[400px] flex flex-col items-start justify-around`}>
                                <h2 className={`text-[28px] mb-[15px] font-semiBold`}>Cart totals</h2>
                                <div className={`w-full`}>
                                    <table className={`border-collapse w-full`}>
                                        <tbody className={`table-row-group align-middle`}>
                                        <tr className={`cart_subtotal table-row border-b-[1px] border-borderColor `}>
                                            <th className={`text-[18px] font-medium text-left break-all align-top py-4`}>Subtotal ({totalItem})</th>
                                            <td className={`table-cell text-right clear-both py-4 break-all align-top font-medium`}>
                                                $ {total}
                                            </td>
                                        </tr>
                                        <tr className={`cart_subtotal table-row `}>
                                            <th className={`text-[18px] font-medium text-left break-all align-top py-4`}>Total ({totalItem})</th>
                                            <td className={`table-cell text-right clear-both py-4 break-all align-top`}>
                                                <strong><span
                                                    className={`text-[26px] font-medium text-lime-600`}>$ {total}</span></strong>
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                    <div onClick={() => handleViewCheckout()}
                                        className={`bg-dangerColor-default_2 text-whiteColor py-3 rounded-full text-center font-medium hover:bg-dangerColor-hover_2 duration-300 mt-6 cursor-pointer`}>
                                        Procees to checkout
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


export default Cart;