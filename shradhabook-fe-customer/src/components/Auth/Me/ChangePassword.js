import React, {useState} from 'react';
import {putChangePassword} from "../../../services/apiService";
import {toast} from "react-toastify";
import {message} from "../../../ultis/message";

const ChangePassword = (props) => {
    const {userId, setIsLoadingData} = props
    const [currentPassword, setCurrentPassword] = useState('')
    const [newPassword, setNewPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')

    const validate = () => {
        if (currentPassword.trim() === "" || newPassword.trim() === "" || confirmPassword.trim() === "") {
            toast.error(message.password_error.password_is_empty)
            return false;
        }
        if (currentPassword.trim().length < 6 || newPassword.trim().length < 6 || confirmPassword.trim().length < 6 ) {
            toast.error(message.password_error.password_is_short)
            return false;
        }
        if (confirmPassword !== newPassword) {
            toast.error(message.password_error.confirm_password)
            return false;
        }
        return true
    }
    const handleClearPassword = (e) => {
        e.preventDefault();
        setCurrentPassword('')
        setNewPassword('')
        setConfirmPassword('')
    }
    const handleChangePassword = async (e) => {
        e.preventDefault();
        if (!validate()) {
            return;
        }
        setIsLoadingData(true);
        let res = await putChangePassword(userId, currentPassword, newPassword)
        if (res && res.status === true) {
            setTimeout(() => {
                setIsLoadingData(false)
            }, 3000)
            toast.success(res.message)
            handleClearPassword(e)
        } else {
            setTimeout(() => {
                setIsLoadingData(false)
            }, 3000)
            toast.error(res.message)
        }
    }
    const handleChangeCurrentPassword = (e) => {
        setCurrentPassword(e.target.value)
    }
    const handleChangeNewPassword = (e) => {
        setNewPassword(e.target.value)
    }
    const handleChangeConfirmPassword = (e) => {
        setConfirmPassword(e.target.value)
    }

    return (
        <>
            <h2 className="text-2xl text-gray-900">Change Password:</h2>
            <form className="mt-6 border-t border-gray-400 w-full pt-4">
                <div className='flex flex-wrap mx-3 mb-6 '>
                    <div className={`flex w-full`}>
                        <div className='w-1/2 md:w-1/2 px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor='current-password'>Current password</label>
                            <input onChange={(e) => handleChangeCurrentPassword(e)}
                                   className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                   id='current-password' type='password'
                                   placeholder='Enter current password'
                                   value={currentPassword}
                                   autoComplete={`one`} />
                        </div>
                    </div>
                    <div className={`flex w-full`}>
                        <div className='w-1/2 md:w-1/2 px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor='new-password'>New password</label>
                            <input onChange={(e) => handleChangeNewPassword(e)}
                                   className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                   id='new-password' type='password'
                                   placeholder='Enter current password' value={newPassword}
                                   autoComplete={`one`} />
                        </div>
                    </div>
                    <div className={`flex w-full`}>
                        <div className='w-1/2 md:w-1/2 px-3 mb-6'>
                            <label
                                className='block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2'
                                htmlFor='confirmPassword'>Confirm password</label>
                            <input onChange={(e) => handleChangeConfirmPassword(e)}
                                   className='appearance-none block w-full bg-white text-gray-700 border border-gray-400 shadow-inner rounded-md py-3 px-4 leading-tight focus:outline-none  focus:border-gray-500'
                                   id='confirmPassword' type='password'
                                   placeholder='Enter current password'
                                   value={confirmPassword}
                                   autoComplete={`one`} />
                        </div>
                    </div>
                    <div className={`flex w-full items-center`}>
                        <div className='w-1/2 md:w-1/2 px-3 mb-6'>
                            <button onClick={(e) => handleChangePassword(e)}
                                    className={`bg-lime-600 text-whiteColor font-semiBold rounded-md py-3 px-4 leading-tight uppercase cursor-pointer hover:bg-lime-700 mr-4`}>Save
                            </button>
                            <button onClick={(e) => handleClearPassword(e)}
                                    className={`bg-dangerColor-default_2 text-whiteColor font-semiBold rounded-md py-3 px-4 leading-tight uppercase cursor-pointer hover:bg-dangerColor-hover_2`}>Clear
                            </button>
                        </div>
                    </div>
                </div>
            </form>
        </>
    );
};

export default ChangePassword;