import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {getMyInfo} from "../../services/apiService";
import * as moment from 'moment'
const Me = () => {
    const {id} = useParams()
    const [userName, setUserName] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [gender, setGender] = useState('')
    const [birthDay, setBirthday] = useState('')
    useEffect(() => {
        fetchMyInfo();
    })
    const fetchMyInfo = async () => {
        let res = await getMyInfo(id)
        console.log(res.data)
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
        {value: '', lable: '...Choose'},
        {value: 'female', lable: 'Female'},
        {value: 'male', lable: 'Male'},
        {value: 'other', lable: 'Other'},
    ]
    return (
        <div className={`me`}>
            <div className={`container flex items-center justify-between py-16 mx-auto xl:px-30`}>
                <div className={`profile_container grid grid-cols-4 w-full`}>
                    <div className={`profile_navbar col-span-1`}>
                        <div className={`avatar`}>
                            <img/>
                        </div>
                        <div className={`navbar_action`}>
                            <div className={`account cursor-pointer`}>Account</div>
                            <div className={`passsword cursor-pointer`}>Password</div>
                            <div className={`My history cursor-pointer`}>My history</div>
                        </div>
                    </div>
                    <div className={`profile_content col-span-3`}>
                        <div className="bg-gray-200 min-h-screen font-mono">
                            <div className="container mx-auto">
                                <div className="inputs w-full p-6 mx-auto">
                                    <h2 className="text-2xl text-gray-900">Account Setting</h2>
                                    <form className="mt-6 border-t border-gray-400 pt-4">
                                        <div className='flex flex-wrap mx-3 mb-6'>
                                            <div className={`flex w-full`}>
                                                <div className='w-2/4 md:w-2/4 px-3 mb-6'>
                                                    <label
                                                        className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                                        htmlFor='grid-text-1'>email address</label>
                                                    <input readOnly={true}
                                                        className='appearance-none block w-full bg-lightColor text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                                        id='grid-text-1' type='text' placeholder='Enter email' defaultValue={email} required/>
                                                </div>

                                            </div>
                                        </div>
                                    </form>
                                    <h2 className="text-2xl text-gray-900">My info:</h2>
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