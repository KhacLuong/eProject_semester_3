import React, {useState} from 'react';
import {Link} from "react-router-dom";
import './auth.scss'
import LoginImage from '../../assets/image/login.png';
import RegisterImage from "../../assets/image/register.png";
import BackgroundImage from "../../assets/image/background_2.png"

const Auth = () => {
    const [isActive, setIsActive] = useState(false);
    const toggleForm = () => {
        setIsActive(!isActive);
    }
    const handleLogin = () => {

    }

    const handleRegister = () => {

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
                    <div className={`imgBx`}>
                        <img src={LoginImage} alt={`login_image`}/>
                    </div>
                    <div className={`formBx`}>
                        <form>
                            <h2>Sign In</h2>
                            <input type={`text`} placeholder={`Email`}/>
                            <input type={`password`} placeholder={`Password`}/>
                            <input type={`submit`} value={`Login`}/>
                            <div>
                                <input className={`checkbox`} type={`checkbox`}/>
                                <p>Remember me</p>
                            </div>
                            <p className={`forgot_password`}>
                                Forgot password? <a href={`#`}>Click here</a>
                            </p>
                            <p className={`signup`}>Don't have an account? <a href={`#`} onClick={() => toggleForm()}>Sign up</a>
                            </p>

                        </form>
                    </div>
                </div>
                <div className={`user signupBx`}>
                    <div className={`formBx`}>
                        <form>
                            <h2>Sign Up</h2>
                            <input type={`text`} placeholder={`Email`}/>
                            <input type={`password`} placeholder={`Create-password`}/>
                            <input type={`password`} placeholder={`Confirm-password`}/>
                            <input type={`submit`} value={`Create an Account`}/>
                            <p className={`signup`}>Already have an account? <a href={`#`} onClick={() => toggleForm()}>Sign in</a>
                            </p>
                        </form>
                    </div>
                    <div className={`imgBx`}>
                        <img src={RegisterImage} alt={`register_image`}/>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default Auth;