import axios from "axios";
import NProgress from "nprogress"
import {store} from '../redux/store'
import {PostRefreshToken} from "../services/apiService";
import {doLogin} from "../redux/action/userAction";

const instance = axios.create({
    baseURL: 'https://localhost:7000/api/',
});
NProgress.configure({
    showSpinner: false,
    trickleSpeed: 100
})

// Add a request interceptor
instance.interceptors.request.use(function (config) {
    // Do something before request is sent
    const access_token = store.getState().user.account.accessToken
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
}, async function (error) {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true

        const email = store.getState().user.account.email
        const refreshToken = store.getState().user.account.refreshToken
        const res = await PostRefreshToken(email, refreshToken);

        instance.defaults.headers.common["Authorization"] = `Bearer ${res.data.accessToken}`
        return instance(originalRequest)
    }
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    NProgress.done();
    return error && error.response && error.response.data ? error.response.data : Promise.reject(error);
});

export default instance;