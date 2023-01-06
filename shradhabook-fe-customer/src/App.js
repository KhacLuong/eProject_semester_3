import './App.css';
import {Route, Routes} from "react-router-dom";
import React, {useState} from "react";
import HomePage from "./components/Home/HomePage";
import ProductPage from "./components/Product/ProductPage";
import ProductDetail from "./components/Product/ProductDetail/ProductDetail";
import ContactPage from "./components/Contact/ContactPage";
import Auth from "./components/Auth/Auth";
import Layout from "./components/Layouts/Layout";
import {ToastContainer} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import Wishlist from "./components/Shopping/Wishlist";
import Cart from "./components/Shopping/Cart";
import BlogPage from "./components/Blog/BlogPage";
import BlogDetail from "./components/Blog/BlogDetail";
import NotFound from "./components/Product/NotFound";
import PrivateRoute from "./routes/PrivateRoute";
import Me from "./components/Auth/Me";
import MyHistory from "./components/Auth/MyHistory";
import CategoryPage from "./components/Category/CategoryPage";
import ModalCart from './ultis/ModalCart'
const App = () => {
    const [open, setOpen] = useState(false)
    return (
        <>
            <Routes>
                <Route path={'/'} element={<Layout/>}>
                    <Route path={'/'} index element={<HomePage setOpen={setOpen}/>}/>
                    <Route path={'/categories/:id/:slug'} element={<CategoryPage/>}/>
                    <Route path={'/products'} element={<ProductPage setOpen={setOpen}/>}/>
                    <Route path={`/products/product-detail/:id/:slug`} element={<ProductDetail setOpen={setOpen}/>}/>
                    <Route path={'/blogs'} element={<BlogPage/>}/>
                    <Route path={'/blogs/blog-detail/:id/:slug'} element={<BlogDetail/>}/>
                    <Route path={'/contact'} element={<ContactPage/>}/>
                    <Route path={'/user/wishlist/:id'} element={<PrivateRoute>
                        <Wishlist/>
                    </PrivateRoute>}/>
                    <Route path={'/user/shopping-cart/:id'} element={<PrivateRoute>
                        <Cart/>
                    </PrivateRoute>}/>
                    <Route path={'/user/my-profile/:id'} element={<PrivateRoute>
                        <Me/>
                    </PrivateRoute>}/>
                    <Route path={'/user/my-history/:id'} element={<PrivateRoute>
                        <MyHistory/>
                    </PrivateRoute>}/>
                </Route>
                <Route path={'*'} element={<NotFound/>}/>
                <Route path={'/login'} element={<Auth/>}/>
            </Routes>
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
            <ModalCart open={open} setOpen={setOpen}/>
        </>
    );
}

export default App;
