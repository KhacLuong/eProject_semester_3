import React, {useEffect, useState} from 'react';
import Banner from "../Layouts/Banner/Banner";
import {getMyInfo} from "../../services/apiService";
import jwt_decode from "jwt-decode";
import {IoCloseSharp} from 'react-icons/io5'
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";
import {removeAllProdcut} from "../../redux/action/cartAction";
import {connect, useSelector} from "react-redux";

const Checkout = (props) => {
    const {removeAllProdcut} = props
    const account = useSelector(state => state.user.account);
    const [userId, setUserId] = useState('')
    const [userInfo, setUserInfo] = useState({})
    const [listProduct, setListProduct] = useState([])
    let decoded = jwt_decode(account.accessToken).Id
    const [total, setTotal] = useState(0)
    const [totalItem, setTotalItem] = useState(0)
    const shop = useSelector(state => state.cart)

    const [name, setName] = useState('')
    const [streetAddressOne, setStreetAddressOne] = useState('')
    const [streetAddressTwo, setStreetAddressTwo] = useState('')
    const [city, setCity] = useState('')
    const [state, setState] = useState('')
    const [zipCode, setZipCode] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [userAddress, setUserAddress] = useState([])
    const [district, setDistrict] = useState('')
    const navigate = useNavigate();

    useEffect(() => {
        setUserId(decoded)
        const fetchData = async () => {
            let res = await getMyInfo(decoded)
            if (res.status === true) {
                setEmail(res.data.email)
                setName(res.data.name)
                setPhone(res.data.userInfo.phone)
                setDistrict(res.data.userInfo.addresses[0].district)
                setStreetAddressOne(res.data.userInfo.addresses[0].addressLine1)
                setStreetAddressTwo(res.data.userInfo.addresses[0].addressLine2)
            }
        }
        fetchData();
    }, [userId, setUserId])

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
    const validatePhone = (phone) => {
        return String(phone)
            .toLowerCase()
            .match(/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/)
    }
    const onChangeName = () => {

    }
    const onChangeEmail = () => {

    }
    const onChangePhone = () => {

    }
    const onChangeAddress = (e) => {
        console.log(e.target)
    }
    const onChangeDistrict = (e) => {
        console.log(e.target)
    }
    const handlePlaceOrder = () => {
        removeAllProdcut();
        navigate('/')
        toast.success('Order Success')
    }
    return (
        <div className={`checkout_page`}>
            <Banner bannerTitle={`checkout`}/>
            <div className={`container mx-auto xl:px-30 py-8`}>
                <div className={`grid-cols-3 grid gap-6`}>
                    <div className={`col-span-2`}>
                        <form>
                            <h2 className={`text-[28px] mb-[15px] font-semiBold`}>Billing details</h2>
                            <div className={`mb-10`}>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Your name<span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input onChange={onChangeName} value={name} className={`py-4 pl-6 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor mt-2`} type={`text`}/>
                                </div>
                                {/*<div className={`flex flex-col mb-4`}>*/}
                                {/*    <label className={`text-sm`}>Country / Region <span className={`text-dangerColor-default_2`}>*</span></label>*/}
                                {/*    <select className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`}>*/}
                                {/*        <option></option>*/}
                                {/*    </select>*/}
                                {/*</div>*/}
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Street address 1<span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input value={streetAddressOne} onChange={onChangeAddress} className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Street address 2<span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input value={streetAddressTwo} onChange={onChangeAddress} className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Town / City <span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input value={district} onChange={onChangeDistrict} className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>State <span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>ZIP Code <span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Phone <span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input onChange={onChangePhone} value={phone} onKeyPress={(e) => !/[0-9]/.test(e.key) && e.preventDefault()} className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                                <div className={`flex flex-col mb-4`}>
                                    <label className={`text-sm`}>Email address<span className={`text-dangerColor-default_2`}>*</span></label>
                                    <input onChange={onChangeEmail} value={email} className={`py-4 pl-6 mt-2 text-sm text-darkColor rounded-full border-borderColor focus:border-blackColor hover:border-blackColor`} type={`text`}/>
                                </div>
                            </div>
                            <h2 className={`text-[28px] mb-1 font-semiBold`}>Additional information</h2>
                            <span className={`text-sm`}>Order notes (optional)</span>
                            <div className={`mt-4`}>
                                <textarea placeholder={`Notes about your order, e.g. special notes for delivery.`} className={`text-sm resize-none w-full rounded-xl p-4 border-borderColor focus:border-blackColor hover:border-blackColor`} cols={12} rows={6}></textarea>
                            </div>
                        </form>
                    </div>
                    <div className={`col-span-1`}>
                        <div
                            className={`pb-[40px] pt-[30px] pl-[30px] pr-[30px] border-[1px] border-borderColor relative sm:rounded-lg shadow-md flex flex-col items-start justify-around`}>
                                <h2 className={`text-[28px] mb-[15px] font-semiBold`}>Your order</h2>
                            <div className={`w-full`}>
                                <table className={`border-collapse w-full`}>
                                    <tbody className={`table-row-group align-middle`}>
                                    <tr className={`cart_subtotal table-row border-b-[1px] border-borderColor `}>
                                        <th className={`text-[18px] font-medium text-left break-all align-top py-4`}>Product</th>
                                        <td className={`table-cell text-right clear-both py-4 break-all align-top font-medium`}>
                                            Subtotal
                                        </td>
                                    </tr>
                                    {
                                        listProduct.map((item, index) => {
                                            return <tr key={index} className={`cart_subtotal table-row border-b-[1px] border-borderColor`}>
                                                <th className={`text-[18px] font-light text-left break-words align-top py-4 w-10/12`}>
                                                    <div>
                                                        <h3 className={`flex items-center mb-1 uppercase text-lg font-medium`}>{item.name} <IoCloseSharp className={`mx-2 text-sm`}/> {item.qty}</h3>
                                                        <code className={`text-xs`}>
                                                            <span>Code: </span>{item.code}
                                                        </code>
                                                        <div className={`text-xs`}>
                                                            <span className={`font-medium`}>Category: </span>{item.category}
                                                        </div>
                                                        <div className={`text-xs`}>
                                                            <span className={`font-medium`}>Author: </span>{item.author}
                                                        </div>
                                                        <div className={`text-xs`}>
                                                            <span className={`font-medium`}>Manufacturer: </span>{item.manufacturer}
                                                        </div>
                                                    </div>
                                                </th>
                                                <td className={`table-cell text-right clear-both py-4 break-all align-top font-light`}>
                                                    ${item.price}
                                                </td>
                                            </tr>
                                        })
                                    }
                                    <tr className={`cart_subtotal table-row border-b-[1px] border-borderColor `}>
                                        <th className={`text-[18px] font-medium text-left break-all align-top py-4`}>Subtotal ({totalItem})</th>
                                        <td className={`table-cell text-right clear-both py-4 break-all align-top font-medium`}>
                                            ${total}
                                        </td>
                                    </tr>
                                    <tr className={`cart_subtotal table-row`}>
                                        <th className={`text-[18px] font-medium text-left break-all align-top py-4`}>Total ({totalItem})</th>
                                        <td className={`table-cell text-right clear-both py-4 align-top`}>
                                            <strong>
                                                <span className={`text-[26px] font-medium text-lime-600`}>${total}</span>
                                            </strong>
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                                <div className={`mb-5`}>
                                    <div className="flex border-b-[1px] border-borderColor pb-4">
                                        <div className="flex items-center h-5">
                                            <input id="helper-radio" name={`default-radio`}  aria-describedby="helper-radio-text" type="radio" value="" className="w-4 h-4 text-dangerColor-default_2 bg-gray-100 border-gray-300 focus:ring-dangerColor-default_2 dark:focus:ring-dangerColor-default_2 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600" />
                                        </div>
                                        <div className="ml-2 text-sm">
                                            <label htmlFor="helper-radio"
                                                   className="font-medium text-gray-900 dark:text-gray-300">Direct bank transfer</label>
                                            <p id="helper-radio-text"
                                               className="pt-2 text-xs font-normal text-gray-500 dark:text-gray-300">Make your payment directly into our bank account. Please use your Order ID as the payment reference. Your order will not be shipped until the funds have cleared in our account.</p>
                                        </div>
                                    </div>
                                    <div className="flex border-b-[1px] border-borderColor py-4">
                                        <div className="flex items-center h-5">
                                            <input id="helper-radio" name={`default-radio`} aria-describedby="helper-radio-text" type="radio" value="" className="w-4 h-4 text-dangerColor-default_2 bg-gray-100 border-gray-300 focus:ring-dangerColor-default_2 dark:focus:ring-dangerColor-default_2 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600" />
                                        </div>
                                        <div className="ml-2 text-sm">
                                            <label htmlFor="helper-radio"
                                                   className="font-medium text-gray-900 dark:text-gray-300">Check payments</label>
                                            <p id="helper-radio-text"
                                               className="pt-2 text-xs font-normal text-gray-500 dark:text-gray-300">Please send a check to Store Name, Store Street, Store Town, Store State / County, Store Postcode.</p>
                                        </div>
                                    </div>
                                    <div className="flex border-b-[1px] border-borderColor py-4">
                                        <div className="flex items-center h-5">
                                            <input id="helper-radio" name={`default-radio`}  aria-describedby="helper-radio-text" type="radio" value="" className="w-4 h-4 text-dangerColor-default_2 bg-gray-100 border-gray-300 focus:ring-dangerColor-default_2 dark:focus:ring-dangerColor-default_2 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600" />
                                        </div>
                                        <div className="ml-2 text-sm">
                                            <label htmlFor="helper-radio"
                                                   className="font-medium text-gray-900 dark:text-gray-300">Cash on delivery</label>
                                            <p id="helper-radio-text"
                                               className="pt-2 text-xs font-normal text-gray-500 dark:text-gray-300">Pay with cash upon delivery.</p>
                                        </div>
                                    </div>
                                </div>
                                <span className={`text-xs text-lightColor`}>
                                    Your personal data will be used to process your order, support your experience throughout this website, and for other purposes described in our privacy policy.
                                </span>
                                <div onClick={handlePlaceOrder}
                                    className={`bg-dangerColor-default_2 text-whiteColor py-3 rounded-full text-center font-medium hover:bg-dangerColor-hover_2 duration-300 mt-6 cursor-pointer`}>
                                    Place order
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
        removeAllProdcut: () => dispatch(removeAllProdcut())
    }
}
export default connect(null, mapDispatchToProps)(Checkout);