import React, {useEffect, useRef, useState} from 'react';
import {useNavigate} from "react-router-dom";
import './auth.scss';
import LoginImage from '../../assets/image/login.png';
import RegisterImage from "../../assets/image/register.png";
import BackgroundImage from "../../assets/image/background_2.png";
import {toast} from 'react-toastify';
import {message} from "../../ultis/message";
import {postCreateUser, postLogin} from "../../services/apiService";
import {RiCloseLine} from 'react-icons/ri'
import {useDispatch} from "react-redux";
import {doLogin} from "../../redux/action/userAction";
import lottie from 'lottie-web'
import legoLoader from '../../assets/loader/lego-loader.json'
import Login from "./Login";
import Register from "./Register";

const Auth = () => {
    const [isActive, setIsActive] = useState(false);
    const [email, setEmail] = useState("");
    const [name, setName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [isLoadingData, setIsLoadingData] = useState(false);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const animationWindow = useRef();
    const [isValidEmail, setIsValidEmail] = useState(true)
    const [isValidPassword, setIsValidPassword] = useState(true)

    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    };
    const hasNumber = (string) => {
        return /\d/.test(string);
    }
    useEffect(() => {
        lottie.loadAnimation({
            container: animationWindow.current,
            loop: true,
            autoplay: true,
            animationData: legoLoader
        })
        lottie.setSpeed(3);
    }, [])

    const toggleForm = () => {
        setIsActive(!isActive);
        setEmail("");
        setName("");
        setConfirmPassword("");
        setPassword("");
    }
    const handleOnChangeEmail = (e) => {
        setEmail(e.target.value);
        const checkEmail = validateEmail(email)
        if (!checkEmail) {
            setIsValidEmail(false)
        } else {
            setIsValidEmail(true)
        }
        if (e.target.value.trim() === "") {
            setIsValidEmail(true)
        }
    }
    const handleOnChangePassword = (e) => {
        if (e.target.value.trim().length < 6) {
            setIsValidPassword(false)
        } else if (e.target.value.trim().length >= 6) {
            setIsValidPassword(true)
        }
        if (e.target.value === "") {
            setIsValidPassword(true)
        }
        setPassword(e.target.value);
    }
    return (
        <section style={{
            background: `url(${BackgroundImage})`,
            backgroundPosition: 'center',
            backgroundSize: 'cover'
        }} className={`${isActive ? 'active_section' : ''}`}>
            <div className={`login_container ${isActive ? 'active_container' : ''}`}>
                <Login
                    setIsLoadingData={setIsLoadingData}
                    validateEmail={validateEmail}
                    loginImage={LoginImage}
                    animationWindow={animationWindow}
                    isLoadingData={isLoadingData}
                    navigate={navigate}
                    email={email}
                    password={password}
                    dispatch={dispatch}
                    toggleForm={toggleForm}
                    handleOnChangeEmail={handleOnChangeEmail}
                    handleOnChangePassword={handleOnChangePassword}
                    isValidEmail={isValidEmail}
                    isValidPassword={isValidPassword}
                />
                <Register
                    isActive={isActive}
                    setIsActive={setIsActive}
                    setIsLoadingData={setIsLoadingData}
                    hasNumber={hasNumber}
                    validateEmail={validateEmail}
                    registerImage={RegisterImage}
                    navigate={navigate}
                    name={name}
                    setName={setName}
                    email={email}
                    password={password}
                    confirmPassword={confirmPassword}
                    setConfirmPassword={setConfirmPassword}
                    toggleForm={toggleForm}
                    dispatch={dispatch}
                    handleOnChangeEmail={handleOnChangeEmail}
                    handleOnChangePassword={handleOnChangePassword}
                    isValidEmail={isValidEmail}
                    isValidPassword={isValidPassword}
                />
            </div>

        </section>
    );
};

export default Auth;