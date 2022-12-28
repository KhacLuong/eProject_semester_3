import React, {useEffect, useRef, useState} from 'react';
import {useNavigate} from "react-router-dom";
import './auth.scss';
import BackgroundImage from "../../assets/image/background_2.png";
import {useDispatch} from "react-redux";
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
    const [isValidEmail, setIsValidEmail] = useState(true)
    const [isValidPassword, setIsValidPassword] = useState(true)
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const animationWindow = useRef();

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
                    lottie={lottie}
                />
                <Register
                    lottie={lottie}
                    isActive={isActive}
                    setIsActive={setIsActive}
                    setIsLoadingData={setIsLoadingData}
                    hasNumber={hasNumber}
                    validateEmail={validateEmail}
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