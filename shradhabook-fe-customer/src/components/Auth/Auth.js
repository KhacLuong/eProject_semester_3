import React, {useEffect, useState} from 'react';
import {Link} from "react-router-dom";
import './auth.scss';
import LoginImage from '../../assets/image/login.png';
import RegisterImage from "../../assets/image/register.png";
import BackgroundImage from "../../assets/image/background_2.png";
import {ToastContainer, toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {message} from "../../ultis/message";
import {postCreateUser} from "../../services/apiService";
import {RiCloseLine} from 'react-icons/ri'

const Auth = () => {
    const [isActive, setIsActive] = useState(false);
    const [email, setEmail] = useState("");
    const [name, setName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [userType, setUserType] = useState("CUSTOMER");

    const toggleForm = () => {
        setIsActive(!isActive);
        setEmail("");
        setName("");
        setConfirmPassword("");
        setPassword("");
    }
    const hasNumber = (string) => {
        return /\d/.test(string);
    }
    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    };

    const checkValidate = () => {
        const isValidEmail = validateEmail(email)
        if (!isValidEmail) {
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
        return true;
    }
    const handleLogin = () => {

    }

    const handleRegister = async (e) => {
        e.preventDefault();
        // check validate
        if(!checkValidate()) {
            return;
        }
        // create and submit data
        let res = await postCreateUser(name, email, password, confirmPassword, userType);
        if(res && res.status === true) {
            toast.success(res.message)
            setIsActive(!isActive);
        }

        if (res.response && res.response.data.status === false) {
            toast.error(res.response.data.message)
        } else if (res.response && res.response.data.status === 400) {
            toast.error(res.response.data.errors.password)
        }
    }

    const handleForgotPassword = () => {

    }

    return (

        <section style={{
            background: `url(${BackgroundImage})`,
            backgroundPosition: 'center',
            backgroundSize: 'cover'
        }} className={`${isActive ? 'active_section' : ''}`}>
            <div className={`login_container ${isActive ? 'active_container' : ''}`}>
                <div className={`user signinBx`}>
                    <Link to={`/`} className={`close_signin`}><RiCloseLine/></Link>
                    <div className={`imgBx`}>
                        <img src={LoginImage} alt={`login_image`}/>
                    </div>
                    <div className={`formBx`}>
                        <form>
                            <h2>Sign In</h2>
                            <input type={`text`} placeholder={`Email`} value={email}
                                   onChange={(event) => setEmail(event.target.value)}/>
                            <input autoComplete="on" type={`password`} placeholder={`Password`} value={password}
                                   onChange={(event) => setPassword(event.target.value)}/>
                            <input type={`submit`} value={`Login`} onClick={() => handleLogin()}/>
                            <div className={`remember_user`}>
                                <input className={`checkbox`} type={`checkbox`}/>
                                <p>Remember me</p>
                            </div>
                            <div className={`forgot_password`}>
                                <p>
                                    Forgot password? <a>Click here</a>
                                </p>
                            </div>
                            <p className={`signup`}>Don't have an account? <a onClick={() => toggleForm()}>Sign
                                up</a>
                            </p>

                        </form>
                    </div>
                </div>
                <div className={`user signupBx`}>
                    <Link to={`/`} className={`close_signup`}><RiCloseLine/></Link>
                    <div className={`formBx`}>
                        <form>
                            <h2>Sign Up</h2>
                            <input type={`text`} placeholder={`Email`} value={email}
                                   onChange={(event) => setEmail(event.target.value)}/>
                            <input type={`text`} placeholder={`Full name`} value={name}
                                   onChange={(event) => setName(event.target.value)}/>
                            <input autoComplete="on" type={`password`} placeholder={`Password`} value={password}
                                   onChange={(event) => setPassword(event.target.value)}/>
                            <input autoComplete="on" type={`password`} placeholder={`Confirm password`} value={confirmPassword}
                                   onChange={(event) => setConfirmPassword(event.target.value)}/>
                            <input type={`submit`} value={`Create`} onClick={(e) => handleRegister(e)}/>
                            <p className={`signup`}>Already have an account? <a onClick={() => toggleForm()}>Sign
                                in</a>
                            </p>
                        </form>
                    </div>
                    <div className={`imgBx`}>
                        <img src={RegisterImage} alt={`register_image`}/>
                    </div>
                </div>
            </div>
            <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light"
            />
        </section>
    );
};

export default Auth;