import React, {useEffect} from 'react';
import {getMyAddress} from "../../../services/apiService";

const Addresses = (props) => {
    const {userId, userInfoId, setIsLoadingData} = props

    useEffect(() => {
        if (userInfoId !== null) {
            const fetchData = async () => {
                await fetchMyAddress();
            }
            fetchData()
        }
    }, [userInfoId])
    const fetchMyAddress = async () => {
        let res = await getMyAddress(userInfoId)
        if (res.status === true) {
        }
    }
    return (
        <>
            <h2 className="text-2xl text-gray-900">My Address:</h2>
            <form className="mt-6 border-t border-gray-400 pt-4">
                <div className='flex flex-wrap mx-3 mb-6'>
                    <div className={`flex w-full`}>
                        <div className='w-full md:w-full px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor=''>user name <span
                                className={`text-dangerColor-default_2`}>*</span></label>
                            <input
                                   className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                    type='text' placeholder='Enter user name'/>
                        </div>
                        <div className='w-full md:w-full px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor=''>phone number <span
                                className={`text-dangerColor-default_2`}>*</span></label>
                            <input
                                   onKeyPress={(e) => !/[0-9]/.test(e.key) && e.preventDefault()}
                                   className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500' type='text'
                                   placeholder='Enter your phone'/>
                        </div>
                    </div>
                </div>
                <div className='flex flex-wrap mx-3 mb-6'>
                    <div className={`flex w-full`}>
                        <div className='w-full md:w-full px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor=''>Birthday <span
                                className={`text-dangerColor-default_2`}>*</span></label>
                            <input  type="date"
                                   className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner py-2 rounded"
                                   placeholder="Select date"/>
                        </div>
                        <div className='w-full md:w-full px-3 mb-6'>
                            <label htmlFor={``}
                                   className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'>Gender <span
                                className={`text-dangerColor-default_2`}>*</span></label>
                            <div className="flex-shrink w-full inline-block relative">
                                {/*<select id={`gender`}*/}
                                {/*       */}
                                {/*        value={}*/}
                                {/*        className="block appearance-none text-gray-600 w-full bg-white border border-gray-400 shadow-inner px-4 py-2 pr-8 rounded">*/}
                                {/*    {optionGenders.map((item, index) => {*/}
                                {/*        return <option key={index}*/}
                                {/*                       value={item.value}>{item.lable}</option>*/}
                                {/*    })}*/}
                                {/*</select>*/}
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </>
    );
};

export default Addresses;