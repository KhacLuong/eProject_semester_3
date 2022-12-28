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
const getListProduct = (query) => {
    return instance.get(`Products`, {
            params: query
        }
    )
}

const getMyInfo = (id) => {
    return instance.get(`User/${id}`);
}

const getListCategory = () => {
    return instance.get('Categories')
}
export {postCreateUser, postLogin, deleteLogout, getListProduct, getListCategory, getMyInfo}