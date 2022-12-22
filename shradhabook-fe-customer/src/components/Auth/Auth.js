import React, {useState} from 'react';
import {Link} from "react-router-dom";
import './auth.scss'
import LoginImage from '../../assets/image/login.png';
import RegisterImage from "../../assets/image/register.png";
import BackgroundImage from "../../assets/image/background_2.png"
import axios from 'axios'

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
    const handleLogin = () => {

    }

    const handleRegister = async (e) => {
        e.preventDefault();
        const data = {
            "name": name,
            "email": email,
            "password": password,
            "confirmPassword": confirmPassword,
            "role": userType
        }
        let res = await axios.post('https://localhost:7000/api/User/customer/register', data)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
        console.log(">>>>> check res: ", res);
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
                            <input type={`text`} placeholder={`Email`} value={email}
                                   onChange={(event) => setEmail(event.target.value)}/>
                            <input type={`password`} placeholder={`Password`} value={password}
                                   onChange={(event) => setPassword(event.target.value)}/>
                            <input type={`submit`} value={`Login`} onClick={() => handleLogin()}/>
                            <div>
                                <input className={`checkbox`} type={`checkbox`}/>
                                <p>Remember me</p>
                            </div>
                            <p className={`forgot_password`}>
                                Forgot password? <a>Click here</a>
                            </p>
                            <p className={`signup`}>Don't have an account? <a onClick={() => toggleForm()}>Sign
                                up</a>
                            </p>

                        </form>
                    </div>
                </div>
                <div className={`user signupBx`}>
                    <div className={`formBx`}>
                        <form>
                            <h2>Sign Up</h2>
                            <input type={`text`} placeholder={`Email`} value={email}
                                   onChange={(event) => setEmail(event.target.value)}/>
                            <input type={`text`} placeholder={`Full name`} value={name}
                                   onChange={(event) => setName(event.target.value)}/>
                            <input type={`password`} placeholder={`Password`} value={password}
                                   onChange={(event) => setPassword(event.target.value)}/>
                            <input type={`password`} placeholder={`Confirm password`} value={confirmPassword}
                                   onChange={(event) => setConfirmPassword(event.target.value)}/>
                            <input type={`submit`} value={`Create an Account`} onClick={(e) => handleRegister(e)}/>
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
        </section>
    );
};

export default Auth;