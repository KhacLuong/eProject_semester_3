import React, {useEffect, useRef, useState} from 'react';
import {useParams} from "react-router-dom";
import {getMyAddress, getMyInfo, postAvatarUser, putUserInfo} from "../../../services/apiService";
import * as moment from 'moment'
import AvatarImage1 from '../../../assets/image/avatar/avatar1.png'
import lottie from "lottie-web";
import legoLoader from "../../../assets/loader/lego-loader.json";
import '../auth.scss'
import {BsThreeDotsVertical} from 'react-icons/bs'
import ChangePassword from "./ChangePassword";
import Addresses from "./Addresses";
import {toast} from "react-toastify";

const Me = () => {
    const {id} = useParams()
    const animationWindow = useRef();
    const [userName, setUserName] = useState('')
    const [avatar, setAvatar] = useState(null)
    const [avatarName, setAvatarName] = useState(null)
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [gender, setGender] = useState('')
    const [birthDay, setBirthday] = useState('')
    const [active, setActive] = useState(1);
    const [isLoadingData, setIsLoadingData] = useState(false);
    const [userInfoId, setUserInfoId] = useState(null)
    const inputAvatarRef = useRef(null);
    const optionGenders = [
        {value: 'female', lable: 'Female'},
        {value: 'male', lable: 'Male'},
        {value: 'other', lable: 'Other'},
    ]
    const btnSystem = [
        {id: 1, title: 'Account details'},
        {id: 2, title: 'Addresses'},
        {id: 3, title: 'Change password'},
        {id: 4, title: 'Orders'},
        {id: 5, title: 'Settings'},
    ]
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
        if (res.status === true) {
            setUserInfoId(res.data.userInfo.id)
            setAvatar(res.data.userInfo.avatar)
            setEmail(res.data.email)
            setUserName(res.data.name)
            setPhone(res.data.userInfo.phone)
            setGender(res.data.userInfo.gender)
            const date = moment(res.data.userInfo.dateofBirth).format("YYYY-MM-DD");
            setBirthday(date)
        }
    }

    useEffect(() => {
        setTimeout(() => {
            setIsLoadingData(false);
        }, 2000)
    }, [setActive, active])

    const handleChangeGenDer = (e) => {
        setGender(e.target.value)
    }
    const handleChangeBirthDay = (e) => {
        setBirthday(e.target.value)
    }
    const handleChangeName = (e) => {
        setUserName(e.target.value)
    }
    const handleChangePhone = (e) => {
        setPhone(e.target.value)
    }
    const handleChangeAvatar = (e) => {
        setAvatar(window.URL.createObjectURL(e.target.files[0]))
        setAvatarName(e.target.files[0])
    }
    const handleUpdateAvatar = async (e) => {
        e.preventDefault();
        // const query = {
        //     phone: phone,
        //     avatar: avatarName,
        //     gender: gender,
        //     dateofBirth: birthDay
        // }
        // console.log(query)
        const data = new FormData()
        data.append('file', avatarName)
        let res = await postAvatarUser(email, data)
        if (res && res.status === true) {
            toast.success(res.message)
        } else {
            toast.error(res.message)
        }
    }
    return (
        <div className={`me`}>
            <div className={`container flex items-center justify-between py-16 mx-auto xl:px-30`}>
                <div className={`profile_container grid grid-cols-4 w-full`}>
                    <div
                        className={`p-6 h-[600px] profile_navbar col-span-1 bg-gray-200 border-r-[1px] border-gray-400 items-center flex flex-col mb-4 relative`}>
                        <div
                            className={` avatar border-b-[1px] border-gray-400 w-full flex items-center text-center flex-col justify-center pb-6`}>
                            <BsThreeDotsVertical onClick={() => (inputAvatarRef.current.click())}
                                          className={`text-darkColor absolute text-white top-2 right-2 cursor-pointer text-2xl`}/>
                            <div className={` w-36 h-36`}>
                                <img style={{
                                    backgroundSize: "cover",
                                    backgroundPosition: "center",
                                    objectFit: "cover"
                                }} className="rounded w-full h-full" src={avatar}
                                     alt="Extra large avatar"/>

                                <input onChange={(e) => handleChangeAvatar(e)} accept="image/png" ref={inputAvatarRef}
                                       type={`file`} hidden={true}/>
                            </div>
                            <div className={`my-2`}>{email}</div>
                        </div>
                        <div className={`navbar_action mt-4 w-full`}>
                            {
                                btnSystem.map((item, index) => {
                                    return <div key={index} onClick={() => setActive(item.id)}
                                                className={`${active === item.id ? 'hover:bg-whiteColor' +
                                                    ' hover:border-[2px] hover:border-blackColor hover:text-blackColor text-white' +
                                                    ' bg-dangerColor-default_2' : 'bg-whiteColor hover:border-[2px] hover:border-blackColor text-blackColor'} w-full text-center py-3 mb-2 cursor-pointer text-white font-medium text-lg duration-300 border-[2px] border-gray-20`}>
                                        {item.title}
                                    </div>
                                })
                            }
                        </div>
                    </div>
                    <div className={`profile_content col-span-3`}>
                        <div className="bg-gray-200 h-[600px]  relative  font-mono">
                            <div ref={animationWindow}
                                 className={`z-20 absolute animationWindow ${isLoadingData ? 'block' : 'hidden'}`}></div>
                            <div className="container mx-auto ">
                                <div className={`inputs w-full p-6 mx-auto ${active === 1 ? 'block' : 'hidden'}`}>
                                    <h2 className="text-2xl text-gray-900">My Info:</h2>
                                    <form className="mt-6 border-t border-gray-400 pt-4">
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='username'>user name <span
                                                        className={`text-dangerColor-default_2`}>*</span></label>
                                                    <input onChange={(e) => handleChangeName(e)}
                                                           className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                           id='username' type='text' placeholder='Enter user name'
                                                           value={userName}/>
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='phoneNumber'>phone number <span
                                                        className={`text-dangerColor-default_2`}>*</span></label>
                                                    <input onChange={(e) => handleChangePhone(e)}
                                                           onKeyPress={(e) => !/[0-9]/.test(e.key) && e.preventDefault()}
                                                           className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                           id='phoneNumber' type='text'
                                                           placeholder='Enter your phone'
                                                           value={phone}/>
                                                </div>
                                            </div>
                                        </div>
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='birthDay'>Birthday <span
                                                        className={`text-dangerColor-default_2`}>*</span></label>
                                                    <input onChange={(e) => handleChangeBirthDay(e)} type="date"
                                                           id='birthDay'
                                                           className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner py-2 rounded"
                                                           placeholder="Select date" value={birthDay}/>
                                                </div>
                                                <div className='w-full md:w-full px-3 mb-6'>
                                                    <label htmlFor={`gender`}
                                                           className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'>Gender <span
                                                        className={`text-dangerColor-default_2`}>*</span></label>
                                                    <div className="flex-shrink w-full inline-block relative">
                                                        <select id={`gender`}
                                                                onChange={(e) => handleChangeGenDer(e)}
                                                                value={gender}
                                                                className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner px-4 py-2 pr-8 rounded">
                                                            {optionGenders.map((item, index) => {
                                                                return <option key={index}
                                                                               value={item.value}>{item.lable}</option>
                                                            })}
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className={`flex w-full items-center `}>
                                                <div className='w-1/2 md:w-1/2 px-3 mb-6'>
                                                    <button onClick={(e) => handleUpdateAvatar(e)}
                                                            className={`bg-lime-600 text-whiteColor font-semiBold rounded-md py-3 px-4 leading-tight uppercase cursor-pointer hover:bg-lime-700`}>Save
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </form>

                                </div>
                                <div className={`inputs w-full p-6 mx-auto ${active === 2 ? 'block' : 'hidden'}`}>
                                    <Addresses userId={id} userInfoId={userInfoId} setIsLoadingData={setIsLoadingData}/>
                                </div>
                                <div className={`inputs w-full p-6 mx-auto ${active === 3 ? 'block' : 'hidden'}`}>
                                    <ChangePassword userId={id} setIsLoadingData={setIsLoadingData}/>
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