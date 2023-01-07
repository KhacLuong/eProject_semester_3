import React, {useEffect, useRef, useState} from 'react';
import {useParams} from "react-router-dom";
import {getMyInfo} from "../../services/apiService";
import * as moment from 'moment'
import AvatarImage1 from '../../assets/image/avatar/avatar1.png'
import lottie from "lottie-web";
import legoLoader from "../../assets/loader/lego-loader.json";
import './auth.scss'
const Me = () => {
    const {id} = useParams()
    const animationWindow = useRef();
    const [userName, setUserName] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [gender, setGender] = useState('')
    const [birthDay, setBirthday] = useState('')
    const [active, setActive] = useState(1);
    const [isLoadingData, setIsLoadingData] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            await fetchMyInfo();
        }
        fetchData()
    }, [])
    useEffect(() => {
        lottie.loadAnimation({
            container: animationWindow.current,
            loop: true,
            autoplay: true,
            animationData: legoLoader
        })
        lottie.setSpeed(3);
    }, [])
    const fetchMyInfo = async () => {
        let res = await getMyInfo(id)
        if(res.status === true) {
            setEmail(res.data.email)
            setUserName(res.data.name)
            setPhone(res.data.userInfo.phone)
            setGender(res.data.userInfo.gender)
            const date = moment(res.data.userInfo.dateofBirth).format("YYYY-MM-DD");
            setBirthday(date)
        }
    }
    const optionGenders = [
        {value: 'female', lable: 'Female'},
        {value: 'male', lable: 'Male'},
        {value: 'other', lable: 'Other'},
    ]
    const handleFormInfo = () => {
        setActive(1)
        setIsLoadingData(true);
    }
    useEffect(() => {
        setTimeout(() => {
            setIsLoadingData(false);
        }, 3000)
    }, [setActive, active])
    const handleFormPassword = () => {
        setActive(2)
        setIsLoadingData(true);
    }
    return (
        <div className={`me`}>
            <div className={`container flex items-center justify-between py-16 mx-auto xl:px-30`}>
                <div className={`profile_container grid grid-cols-4 w-full`}>
                    <div className={`p-6 min-h-screen profile_navbar col-span-1 bg-gray-200 border-r-[1px] border-darkColor items-center flex flex-col mb-4`}>
                        <div className={`avatar border-b-[1px] border-darkColor w-full flex items-center text-center flex-col justify-center pb-6`}>
                            <img className="rounded w-36 h-36" src={AvatarImage1}
                                 alt="Extra large avatar"/>
                            <div className={`my-2`}>{email}</div>
                        </div>
                        <div className={`navbar_action mt-4 w-full`}>
                            <div onClick={()=>handleFormInfo()} className={`${active === 1 ? 'hover:bg-whiteColor' +
                                ' hover:border-[2px] hover:border-blackColor hover:text-blackColor text-white' +
                                ' bg-dangerColor-default_2' : 'bg-whiteColor hover:border-[2px] hover:border-blackColor text-blackColor'} w-full text-center py-3 mb-2 cursor-pointer text-white font-medium text-lg duration-300 border-[2px] border-gray-20`}>Account</div>
                            <div onClick={()=>handleFormPassword()} className={`${active === 2 ? 'hover:bg-whiteColor' +
                                ' hover:border-[2px] hover:border-blackColor hover:text-blackColor text-white' +
                                ' bg-dangerColor-default_2' : 'bg-whiteColor hover:border-[2px] hover:border-blackColor text-blackColor'} w-full text-center py-3 cursor-pointer text-white font-medium text-lg duration-300 border-[2px] border-gray-20`}>Password</div>
                        </div>
                    </div>
                    <div className={`profile_content col-span-3`}>
                        <div className="bg-gray-200 min-h-screen relative  font-mono">
                            <div ref={animationWindow} className={`z-20 absolute animationWindow ${isLoadingData ? 'block' : 'hidden'}`}></div>
                            <div className="container mx-auto ">
                                <div className={`inputs w-full p-6 mx-auto ${active === 1 ? 'block' : 'hidden' }`}>
                                    <h2 className="text-2xl text-gray-900">My Info:</h2>
                                    <form className="mt-6 border-t border-gray-400 pt-4">
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='phoneNumber'>phone number</label>
                                                    <input
                                                        className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                        id='phoneNumber' type='text' placeholder='Enter your phone' defaultValue={phone} />
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='username'>user name</label>
                                                    <input
                                                        className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                        id='username' type='text' placeholder='Enter user name' defaultValue={userName}/>
                                                </div>
                                            </div>
                                        </div>
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='birthDay'>Birthday</label>
                                                    <input type="date" id='birthDay'
                                                           className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner py-2 rounded"
                                                           placeholder="Select date" defaultValue={birthDay}/>
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'>Gender</label>
                                                    <div className="flex-shrink w-full inline-block relative">
                                                        <select defaultValue={gender} className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner px-4 py-2 pr-8 rounded">
                                                            {optionGenders.map((item, index) => {
                                                                return <option key={index} value={item.value}>{item.lable}</option>
                                                            })}
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div className={`inputs w-full p-6 mx-auto ${active === 2 ? 'block' : 'hidden' }`}>
                                    <h2 className="text-2xl text-gray-900">My Password:</h2>
                                    <form className="mt-6 border-t border-gray-400 pt-4">
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='phoneNumber'>phone number</label>
                                                    <input
                                                        className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                        id='phoneNumber' type='text' placeholder='Enter your phone' defaultValue={phone} />
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='username'>user name</label>
                                                    <input
                                                        className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                        id='username' type='text' placeholder='Enter user name' defaultValue={userName}/>
                                                </div>
                                            </div>
                                        </div>
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='birthDay'>Birthday</label>
                                                    <input type="date" id='birthDay'
                                                           className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner py-2 rounded"
                                                           placeholder="Select date" defaultValue={birthDay}/>
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'>Gender</label>
                                                    <div className="flex-shrink w-full inline-block relative">
                                                        <select defaultValue={gender} className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner px-4 py-2 pr-8 rounded">
                                                            {optionGenders.map((item, index) => {
                                                                return <option key={index} value={item.value}>{item.lable}</option>
                                                            })}
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
);
};

export default Me;