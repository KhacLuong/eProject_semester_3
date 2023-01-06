import {Fragment, useState} from 'react'
import {Dialog, Transition} from '@headlessui/react'
import {XMarkIcon} from '@heroicons/react/24/outline'
import {useNavigate} from "react-router-dom";
import {IoCloseSharp} from 'react-icons/io5'
import {FaMinus} from 'react-icons/fa'
import Book20 from '../assets/image/books/book20.png'
import {useSelector} from "react-redux";

export default function ModalCart(props) {
    const {open, setOpen} = props
    const [hoverImg, setHoverImg] = useState(false);
    const navigate = useNavigate();

    const handleViewCart = (id) => {
        navigate(`/user/shopping-cart/${id}`)
    }
    const handleViewCheckout = (id) => {
        navigate(`/user/checkout/${id}`)
    }

    return (
        <Transition.Root show={open} as={Fragment}>
            <Dialog as="div" className="relative z-10" onClose={setOpen}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-in-out duration-500"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in-out duration-500"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    <div className="fixed inset-0 bg-zinc-900 bg-opacity-75 transition-opacity"/>
                </Transition.Child>

                <div className="fixed inset-0 overflow-hidden">
                    <div className="absolute inset-0 overflow-hidden">
                        <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
                            <Transition.Child
                                as={Fragment}
                                enter="transform transition ease-in-out duration-500 sm:duration-700"
                                enterFrom="translate-x-full"
                                enterTo="translate-x-0"
                                leave="transform transition ease-in-out duration-500 sm:duration-700"
                                leaveFrom="translate-x-0"
                                leaveTo="translate-x-full"
                            >
                                <Dialog.Panel className="pointer-events-auto relative w-screen max-w-md">
                                    <Transition.Child
                                        as={Fragment}
                                        enter="ease-in-out duration-500"
                                        enterFrom="opacity-0"
                                        enterTo="opacity-100"
                                        leave="ease-in-out duration-500"
                                        leaveFrom="opacity-100"
                                        leaveTo="opacity-0"
                                    >
                                        <div className="absolute top-3 right-6 -ml-8 flex pt-4 pr-2 sm:-ml-10 sm:pr-4">
                                            <button
                                                type="button"
                                                className="flex items-center rounded-md text-gray-300 hover:text-blackColor focus:outline-none focus:ring-2 focus:ring-white"
                                                onClick={() => setOpen(false)}
                                            >
                                                <XMarkIcon className="h-6 w-6" aria-hidden="true"/>
                                            </button>
                                        </div>
                                    </Transition.Child>
                                    <div className="flex h-full flex-col bg-white py-6 shadow-xl">
                                        <div className="px-4 sm:px-6">
                                            <Dialog.Title className="text-lg font-medium text-gray-900">Shopping
                                                cart</Dialog.Title>
                                        </div>
                                        <div className="relative mt-6 flex-1 px-4 sm:px-6 overflow-y-auto h-full">
                                            <div className={`flex items-start flex-col h-full`}>
                                                <ul className={`w-full px-4 sm:px-6 border-2 border-dashed border-gray-200`}>
                                                    <li className={`last:border-b-0 py-4 border-b-[1px] border-borderColor relative`}>
                                                        <div className={`w-full flex items-center`}>
                                                            <a onMouseOver={() => setHoverImg(true)}
                                                               onMouseLeave={() => setHoverImg(false)}
                                                               className={`cursor-pointer group/image max-w-[80px] mr-3`}>
                                                                <img src={Book20}
                                                                     className={`rounded-[10px] max-w-[80px]`}/>
                                                            </a>
                                                            <div className={`w-full`}>
                                                                <h3 className={`${hoverImg ? 'text-dangerColor-default_2 ' : ''} duration-300 hover:text-dangerColor-default_2 line-clamp-3 cursor-pointer w-full`}>Annie
                                                                    Leibovitz: Wonderss</h3>
                                                                <p className={`text-xs`}>Author: <span
                                                                    className={`hover:text-dangerColor-default_2 hover:underline duration-300 font-light cursor-pointer`}>Duc Anh</span>
                                                                </p>
                                                                <p className={`text-xs mb-4`}>Category: <span
                                                                    className={`hover:text-dangerColor-default_2 hover:underline duration-300 font-light cursor-pointer`}>Action</span>
                                                                </p>
                                                                <p className={`price flex items-center`}><span
                                                                    className={`font-medium`}>$ 316.15</span><IoCloseSharp
                                                                    className={`mx-2`}/><span
                                                                    className={`font-semiBold`}>2</span></p>
                                                            </div>
                                                            <button
                                                                className={`bg-dangerColor-default_2 hover:bg-dangerColor-hover_2 duration-300 text-whiteColor text-lg rounded-md py-1 px-1 mx-2`}>
                                                                <FaMinus className={``}/>
                                                            </button>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div className={`relative mt-6 px-4 sm:px-6`}>
                                            <div>
                                                <div className={`flex justify-between items-center capitalize text-xl`}>
                                                    <div>subtotal:</div>
                                                    <div className={`font-semiBold`}>$ 486.99</div>
                                                </div>
                                                <div className={`uppercase text-white flex flex-col text-center`}>
                                                    <div onClick={handleViewCart}
                                                         className={`duration-300 cursor-pointer bg-lime-600 my-3 py-3 rounded-full hover:bg-lime-700`}>view
                                                        cart
                                                    </div>
                                                    <div
                                                        className={`duration-300 cursor-pointer bg-dangerColor-default_2 py-3 rounded-full hover:bg-dangerColor-hover_2`}>checkout
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </Dialog.Panel>
                            </Transition.Child>
                        </div>
                    </div>
                </div>

            </Dialog>
        </Transition.Root>
    )
}
