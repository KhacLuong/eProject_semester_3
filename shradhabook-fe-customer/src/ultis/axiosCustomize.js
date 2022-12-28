import axios from "axios";
import NProgress from "nprogress"
import {store} from '../redux/store'

const instance = axios.create({
    baseURL: 'https://localhost:7000/api/'
});

NProgress.configure({
    showSpinner: false,
    trickleSpeed: 100
})

// Add a request interceptor
instance.interceptors.request.use(function (config) {
    // Do something before request is sent
    const access_token = store?.getState()?.user?.account?.accessToken
    config.headers["Authorization"] = `Bearer ${access_token}`
    NProgress.start();
    return config;
}, function (error) {
    // Do something with request error
    return Promise.reject(error);
});

// Add a response interceptor
instance.interceptors.response.use(function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    NProgress.done();
    return response && response.data ? response.data : response;
}, function (error) {

    // token expired

    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    NProgress.done();
    return error && error.response && error.response.data ? error.response.data : Promise.reject(error);
});

export default instance;