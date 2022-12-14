import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {
    BrowserRouter,
    Routes,
    Route
} from "react-router-dom";
import Content from "./components/Home/Content";
import HomePage from "./components/Home/HomePage";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
        <Routes>
            <Route path={'/'} element={<App />}>
                <Route index element={<HomePage />}></Route>
                <Route path={'/categories'} />
                <Route path={'/products'} />
                <Route path={'/blog'} />
                <Route path={'/contact'} />
                <Route path={'/pages'} element={<Content />}/>
            </Route>
        </Routes>
    </BrowserRouter>,
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
