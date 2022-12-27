import instance from "../ultis/axiosCustomize";
const axiosRetry = require('axios-retry');
const postCreateUser = (name, email, password, confirmPassword, userType) => {
    const data = {
        "name": name,
        "email": email,
        "password": password,
        "confirmPassword": confirmPassword,
        "userType": userType
    }
    return instance.post('User/register', data)
}

const postLogin = (email, password) => {
    return instance.post('Auth/login', {email, password})
}
const deleteLogout = (accessToken) => {
    // instance.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`
    return instance.post('Auth/logout')
}
const getListProduct = (pageIndex, pageSize) => {
    // const paramName = 'name=' + name;
    // const paramCode = 'code=' + code;
    // const paramStatus = 'status=' + status;
    // const paramCategoryId = 'categoryId=' + categoryId;
    // const paramAuthorId = 'AuthorId=' + AuthorId;
    // const paramManufactuerId = 'manufactuerId=' + manufactuerId;
    // const paramLowPrice = 'lowPrice=' + lowPrice;
    // const paramHightPrice = 'hightPrice=' + hightPrice;
    // const paramSortBy = 'sortBy=' + sortBy;
    const paramPageSize = 'pageSize=' + pageSize;
    const paramPageIndex = 'pageIndex=' + pageIndex;
    // return axios.get(`Products?${name?paramName:''}&${code?paramCode:''}&${status?paramStatus:''}&${categoryId?paramCategoryId:''}&${AuthorId?paramAuthorId:''}&${manufactuerId?paramManufactuerId:''}&${lowPrice?paramLowPrice:''}&${hightPrice?paramHightPrice:''}&${sortBy?paramSortBy:''}&${pageSize?paramPageSize:''}&${pageIndex?paramPageIndex:''}`)
    return instance.get(`Products?${pageSize ? paramPageSize : ''}&${pageIndex ? paramPageIndex : ''}`)
}
export {postCreateUser, postLogin, deleteLogout, getListProduct}