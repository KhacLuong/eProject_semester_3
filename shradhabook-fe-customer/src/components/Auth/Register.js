import React, {useState} from 'react';
import {RiCloseLine} from "react-icons/ri";
import {toast} from "react-toastify";
import {message} from "../../ultis/message";
import {postCreateUser} from "../../services/apiService";
import RegisterImage from "../../assets/image/register.png";
import Book_1 from "../../assets/image/books/book1.png"
import Book_2 from "../../assets/image/books/book2.png"
import Book_3 from "../../assets/image/books/book3.png"

const Register = (props) => {
    const {
        hasNumber,
        validateEmail,
        navigate,
        email,
        password,
        confirmPassword,
        setConfirmPassword,
        name,
        setName,
        toggleForm,
        setIsLoadingData,
        isActive,
        setIsActive,
        dispatch,
        handleOnChangeEmail,
        handleOnChangePassword,
        isValidEmail,
        isValidPassword,
        lottie
    } = props
    const [userType, setUserType] = useState("user");

    const checkValidateRegister = () => {
        const checkEmail = validateEmail(email)
        if (!checkEmail) {
            toast.error(message.email_error.invalid)
            return false;
        }
        if (confirmPassword !== password) {
            toast.error(message.password_error.confirm_password)
            return false;
        }
        if (password === "") {
            toast.error(message.password_error.password_is_empty)
            return false;
        }
        if (hasNumber(name)) {
            toast.error(message.name_error.name_has_number)
            return false;
        }
        if (name === "") {
            toast.error(message.name_error.name_is_empty)
        }
        if (password.trim().length < 6) {
            toast.error(message.password_error.password_is_short)
            return false;
        }
        return true;
    }
    const handleRegister = async (e) => {
        e.preventDefault();

        // check validate
        if (!checkValidateRegister()) {
            return;
        }
        setIsLoadingData(true);
        // create and submit data
        let res = await postCreateUser(name, email, password, confirmPassword, userType);
        if (res && res.status === true) {
            lottie.destroy()
            setIsLoadingData(false);
            setIsActive(!isActive);
            toast.success(res.message)
        }
    }

    return (
        <div className={`user signupBx`}>
            <div className={`close_signup`} onClick={() => navigate('/')}><RiCloseLine/></div>
            <div className={`formBx`}>
                <form>
                    <h2>Sign Up</h2>
                    <input className={`${isValidEmail ? 'border-1 text-darkColor focus:border-darkColor' : 'border-1'
                        + ' border-dangerColor-default_2 text-dangerColor-default_2'
                        + ' focus:border-dangerColor-default_2'}`} type={`text`} placeholder={`Email`} value={email}
                           onChange={(event) => handleOnChangeEmail(event)}/>
                    <input type={`text`} placeholder={`Full name`} value={name}
                           onChange={(event) => setName(event.target.value)}/>
                    <input className={`${isValidPassword ? 'border-1 text-darkColor focus:border-darkColor' : 'border-1'
                        + ' border-dangerColor-default_2 text-dangerColor-default_2'
                        + ' focus:border-dangerColor-default_2'}`} autoComplete="on" type={`password`} placeholder={`Password`} value={password}
                           onChange={(event) => handleOnChangePassword(event)}/>
                    <input autoComplete="on" type={`password`} placeholder={`Confirm password`}
                           value={confirmPassword}
                           onChange={(event) => setConfirmPassword(event.target.value)}/>
                    <input type={`submit`} value={`Create`} onClick={(e) => handleRegister(e)}/>
                    <p className={`signup`}>Already have an account? <a onClick={() => toggleForm()}>Sign
                        in</a>
                    </p>
                </form>
            </div>
            <div className={`imgBx`}>
                <img src={Book_2} alt={`register_image`}/>
            </div>
        </div>
    );
};

export default Register;