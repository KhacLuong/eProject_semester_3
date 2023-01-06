import React from 'react';
import Banner from "../Layouts/Banner/Banner";

const Cart = () => {
    return (
        <div className={`cart_page`}>
            <Banner bannerTitle={`cart`}/>
            <div className={`container mx-auto xl:px-30 py-8`}>
                <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
                    <div className={`grid grid-cols-3 gap-10`}>
                        <div className={`col-span-2`}>
                            <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                                <thead
                                    className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                                <tr>
                                    <th scope="col" className="px-6 py-3">
                                        <span className="sr-only">Image</span>
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Product
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Price
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        Quantity
                                    </th>
                                    <th scope="col" className="px-6 py-3">
                                        <span className="sr-only">Edit</span>
                                    </th>
                                </tr>
                                </thead>
                                <tbody>
                                <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <th scope="row"
                                        className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                        Apple MacBook Pro 17"
                                    </th>
                                    <td className="px-6 py-4">
                                        Sliver
                                    </td>
                                    <td className="px-6 py-4">
                                        Laptop
                                    </td>
                                    <td className="px-6 py-4">
                                        $2999
                                    </td>
                                    <td className="px-6 py-4 text-right">
                                        <a href="#"
                                           className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                    </td>
                                </tr>
                                <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <th scope="row"
                                        className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                        Microsoft Surface Pro
                                    </th>
                                    <td className="px-6 py-4">
                                        White
                                    </td>
                                    <td className="px-6 py-4">
                                        Laptop PC
                                    </td>
                                    <td className="px-6 py-4">
                                        $1999
                                    </td>
                                    <td className="px-6 py-4 text-right">
                                        <a href="#"
                                           className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                    </td>
                                </tr>
                                <tr className="bg-white dark:bg-gray-800 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <th scope="row"
                                        className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                        Magic Mouse 2
                                    </th>
                                    <td className="px-6 py-4">
                                        Black
                                    </td>
                                    <td className="px-6 py-4">
                                        Accessories
                                    </td>
                                    <td className="px-6 py-4">
                                        $99
                                    </td>
                                    <td className="px-6 py-4 text-right">
                                        <a href="#"
                                           className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                        <div className={`col-span-1`}>
                            <div className={`border-[1px] border-borderColor`}>
                                <h2>Cart totals</h2>
                                <table>
                                    <tbody>
                                    <tr className={`cart_subtotal`}>
                                        <th></th>
                                    </tr>
                                    <tr className={`order_total`}>
                                        <th></th>
                                    </tr>
                                    </tbody>
                                </table>
                                <div>
                                    <a>Procees to checkout</a>
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