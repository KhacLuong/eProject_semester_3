import React, {useState} from 'react';
import {RiCloseLine} from "react-icons/ri";
import {toast} from "react-toastify";
import {message} from "../../ultis/message";
import {postLogin} from "../../services/apiService";
import {doLogin} from "../../redux/action/userAction";
import LoginImage from '../../assets/image/login.png';
import Book_1 from "../../assets/image/books/book_1.png"
import Book_2 from "../../assets/image/books/book2.png"
import Book_3 from "../../assets/image/books/book3.png"

const Login = (props) => {
    const {
        validateEmail,
        setIsLoadingData,
        animationWindow,
        isLoadingData,
        navigate,
        email,
        password,
        dispatch,
        toggleForm,
        handleOnChangeEmail,
        handleOnChangePassword,
        isValidPassword,
        isValidEmail,
        lottie
    } = props
    // const [isRemember, setIsRemember] = useState(false);

    const checkValidateLogin = () => {
        const checkEmail = validateEmail(email)
        if (!checkEmail) {
            toast.error(message.email_error.invalid)
            return false;
        }
        if (password.trim() === "") {
            toast.error(message.password_error.password_is_empty)
            return false;
        }
        if (password.trim().length < 6) {
            toast.error(message.password_error.password_is_short)
            return false;
        }
        return true;
    }

    const handleLogin = async (e) => {
        e.preventDefault();
        // check validate
        if (!checkValidateLogin()) {
            return;
        }
        setIsLoadingData(true);
        let data = await postLogin(email, password)
        if (data && data.status === true) {
            dispatch(doLogin(data))
            lottie.destroy()
            setIsLoadingData(false);
            toast.success(data.message);
            navigate('/')
        } else {
            setIsLoadingData(false);
            toast.error(data.message);
        }
    }
    const handleForgotPassword = async (e) => {
        e.preventDefault();
    }

    // const handleChecKedRemember = () => {
    //     setIsRemember(!isRemember);
    // }

    return (
        <div className={`user signinBx`}>
            <div ref={animationWindow}
                 className={`z-20 absolute animationWindow ${isLoadingData ? 'block' : 'hidden'}`}></div>
            <div className={`close_signin`} onClick={() => navigate('/')}><RiCloseLine/></div>
            <div className={`imgBx`}>
                <img src={Book_3} alt={`login_image`}/>
            </div>
            <div className={`formBx`}>
                <form>
                    <h2>Sign In</h2>
                    <input className={`${isValidEmail ? 'border-1 text-darkColor focus:border-darkColor' : 'border-1'
                        + ' border-dangerColor-default_2 text-dangerColor-default_2'
                        + ' focus:border-dangerColor-default_2'}`} type={`text`} placeholder={`Email`} value={email}
                           onChange={(event) => handleOnChangeEmail(event)}/>
                    <input className={`${isValidPassword ? 'border-1 text-darkColor focus:border-darkColor' : 'border-1'
                        + ' border-dangerColor-default_2 text-dangerColor-default_2'
                        + ' focus:border-dangerColor-default_2'}`} autoComplete="on" type={`password`}
                           placeholder={`Password`} value={password}
                           onChange={(event) => handleOnChangePassword(event)}/>
                    <input type={`submit`} value={`Login`} onClick={(e) => handleLogin(e)}/>
                    {/*<div className={`remember_user`}>*/}
                    {/*    <input value={isRemember} onChange={handleChecKedRemember} className={`checkbox`} type={`checkbox`}/>*/}
                    {/*    <p>Remember me</p>*/}
                    {/*</div>*/}
                    <div className={`forgot_password`}>
                        <p>
                            Forgot password? <a onClick={(e) => handleForgotPassword(e)}>Click here</a>
                        </p>
                    </div>
                    <p className={`signup`}>Don't have an account? <a onClick={() => toggleForm()}>Sign
                        up</a>
                    </p>
                </form>
            </div>
        </div>
    );
};

export default Login;