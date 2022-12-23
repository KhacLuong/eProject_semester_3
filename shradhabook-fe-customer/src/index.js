import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import 'flowbite';
import {
    BrowserRouter,
    Routes,
    Route
} from "react-router-dom";
import Content from "./components/Home/Content";
import HomePage from "./components/Home/HomePage";
import ContactPage from "./components/Contact/ContactPage";
import Product from "./components/Product/Product";
import Auth from "./components/Auth/Auth";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
        <Routes>
            <Route path={'/'} element={<App/>}>
                <Route index element={<HomePage/>}></Route>
                <Route path={'/categories'}/>
                <Route path={'/products'} element={<Product/>}/>
                <Route path={'/blog'}/>
                <Route path={'/contact'} element={<ContactPage/>}/>
                <Route path={'/pages'} element={<Content/>}/>
                <Route path={'/wishlist/{id}'} element={<Content/>}/>
                <Route path={'/shopping-cart/{id}'} element={<Content/>}/>
            </Route>
            <Route path={'/login'} element={<Auth/>}/>
        </Routes>
    </BrowserRouter>,
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
