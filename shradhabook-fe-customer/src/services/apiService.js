import instance from "../ultis/axiosCustomize";


// =================== API FOR USER ===================
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
const postRefreshToken = (id) => {
    return instance.post(`Auth/refresh-token?id=${id}`, {
        withCredentials: true
    })
}
const postLogin = (email, password) => {
    return instance.post('Auth/login', {email, password})
}
const deleteLogout = (accessToken) => {
    // instance.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`
    return instance.post('Auth/logout')
}
const getMyInfo = (id) => {
    return instance.get(`User/${id}`);
}


// =================== API FOR PRODUCT ===================
const getListCategory = () => {
    return instance.get('Categories')
}
const getListProduct = (query) => {
    return instance.get(`Products`, {
            params: query
        }
    )
}
const updateViewCountProductById = (id) => {
    return instance.get(`Products/Detail${id}`);
}
const getProductById = (id) => {
    return instance.get(`Products/Detail${id}`);
}


export {postCreateUser, postLogin, deleteLogout, getListProduct, getListCategory, getMyInfo, updateViewCountProductById, getProductById, postRefreshToken}