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
const postRefreshToken = (query) => {
    return instance.post(`Auth/refresh-token`, {
        params: query
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
const getListAuthor = () => {
    return instance.get('Authors')
}
const getListProduct = (query) => {
    return instance.get(`Products`, {
            params: query
        }
    )
}
const updateViewCountProductById = (id) => {
    return instance.post(`Products/IncreaseViewCountProduct${id}`);
}
const getProductById = (id) => {
    return instance.get(`Products/Detail${id}`);
}
const getProduct = (id) => {
    return instance.get(`Products/${id}`)
}
const getWishListById = (id, query) => {
    return instance.get(`WishListUser/GetProductWishListByUserId${id}`, {
        params: query
    })
}
const postProductToWishList = (userId, productId) => {
    return instance.post(`WishListUser?userId=${userId}&prouctId=${productId}`)
}

const deleteProductInWishList = (userId, productId) => {
    return instance.post(`WishListUser?userId=${userId}&prouctId=${productId}`)
}

export {postCreateUser, postLogin, deleteLogout, getListProduct, getListCategory, getMyInfo, updateViewCountProductById, getProductById, getProduct, postRefreshToken, getListAuthor, getWishListById, postProductToWishList}