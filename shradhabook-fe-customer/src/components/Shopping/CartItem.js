import React, {useEffect, useState} from 'react';
import {connect} from "react-redux";
import {doRemoveFromCart, adJustQty} from "../../redux/action/cartAction";
import book14 from "../../assets/image/books/book14.png"
import {HiOutlineMinusSm} from "react-icons/hi";
import {FiPlus} from "react-icons/fi";
import {BsFillTrashFill} from "react-icons/bs";
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";

const CartItem = (props) => {
    const {item, doRemoveFromCart, adJustQty} = props
    const navigate = useNavigate();
    const [count, setCount] = useState(item.qty)

    useEffect(() => {
        setCount(item.qty)
    }, [item])
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
        adJustQty(item.id, plus)
        if (plus > 10) {
            plus = 10
            handleOnChangeQuantity(plus)
            adJustQty(item.id, plus)
            toast.error('You can only buy up to 10 products')
        }
    }
    const handleMinusQuantity = (e) => {
        e.preventDefault();
        let minus = count - 1
        handleOnChangeQuantity(minus)
        adJustQty(item.id, minus)
        if (minus < 1) {
            minus = 1
            handleOnChangeQuantity(minus)
            adJustQty(item.id, minus)
            toast.error('Must choose at least one product')
        }
    }
    const handleOnKeyUp = (e) => {
        const number = parseInt(e.target.value)
        if (number > 10) {
            toast.error('You can only buy up to 10 products')
            setCount(10)
            adJustQty(item.id, 10)
        } else if (number < 1) {
            toast.error('Must choose at least one product')
            setCount(1)
            adJustQty(item.id, 1)
        } else {
            setCount(number)
            adJustQty(item.id, number)
        }
    }

    return (
        <>
            <tr className="last:border-b-0 border-b-[1px] border-borderColor bg-white dark:bg-gray-800 hover:bg-gray-50 dark:hover:bg-gray-600">
                <td scope="row"
                    className="px-2 py-2 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                    <img onClick={() => navigate(`/products/product-detail/${item.id}/${item.slug}`)} className={`cursor-pointer rounded-xl`} src={book14}/>
                </td>
                <td className="px-6 py-4 w-60">
                    <div className={`w-60 font-semiBold text-blackColor break-words`}>
                        <h3 onClick={() => navigate(`/products/product-detail/${item.id}/${item.slug}`)} className={`cursor-pointer duration-300 hover:text-dangerColor-default_2 text-2xl mb-4 line-clamp-2`}>
                            {item.name}
                        </h3>
                        <p className={`text-xs`}>Author: <span
                            className={`hover:text-dangerColor-default_2 hover:underline duration-300 font-light cursor-pointer`}>{item.author}</span>
                        </p>
                        <p className={`text-xs`}>Category: <span
                            className={`hover:text-dangerColor-default_2 hover:underline duration-300 font-light cursor-pointer`}>{item.category}</span>
                        </p>
                        <p className={`text-xs`}>Description: <span
                            className={`hover:text-dangerColor-default_2 hover:underline duration-300 font-light cursor-pointer`}>{item.description}</span>
                        </p>
                    </div>
                </td>
                <td className="px-6 py-4 font-medium text-blackColor text-lg">
                    $ {item.price}
                </td>
                <td className="px-6 py-4">
                    <div
                        className="custom-number-input relative inline-flex overflow-hidden justify-center items-center w-[120px] h-[40px] rounded-md border-[1px] border-borderColor">
                        <button onClick={handleMinusQuantity} data-action="decrement"
                                className="flex justify-center items-center p-0 z-9 border-0 text-[12px] font-bold w-[30%] h-[52px] rounded-none text-blackColor bg-whiteColor">
                            <span className="m-auto font-thin"><HiOutlineMinusSm/></span>
                        </button>
                        <input
                            className="py-[10px] w-[50%] border-0 text-center text-[12px] font-bold text-blackColor flex-1"
                            value={count}
                            onChange={handleOnChangeQuantity}
                            onKeyUp={handleOnKeyUp}
                            type="number"
                        />
                        <button onClick={handlePlusQuantity} data-action="increment"
                                className="flex justify-center items-center p-0 z-9 border-0 text-[12px] font-bold w-[30%] h-[52px] text-blackColor bg-whiteColor">
                            <span className="m-auto font-thin"><FiPlus/></span>
                        </button>
                    </div>
                </td>
                <td className={`text-lime-600 font-semiBold text-lg`}>
                    $ {item.price * item.qty}
                </td>
                <td className="px-6 py-4 text-right">
                    <button onClick={()=>doRemoveFromCart(item.id)}
                            className={`p-2 border-0 rounded-md text-whiteColor duration-300 bg-dangerColor-default_2 hover:bg-dangerColor-hover_2`}>
                        <BsFillTrashFill className={` text-[14px] cursor-pointer`}/>
                    </button>
                </td>
            </tr>
        </>
    );
};
const mapDispatchToProps = dispatch => {
    return {
        doRemoveFromCart: (id) => dispatch(doRemoveFromCart(id)),
        adJustQty: (id, value) => dispatch(adJustQty(id, value))
    }
}
export default connect(null,mapDispatchToProps)(CartItem);