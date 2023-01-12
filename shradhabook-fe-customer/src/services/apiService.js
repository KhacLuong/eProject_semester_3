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
const postRefreshToken = (email, refreshToken) => {
    return instance.post(`Auth/refresh-token`, {email,refreshToken})
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
const getMyAddress = (id) => {
    return instance.get(`Address?userInfoId=${id}`, {id})
}
const putChangePassword = (id, currentPassword, newPassword) => {
    const data = {
        oldPassword: currentPassword,
        password: newPassword
    }
    return instance.put(`User/password/${id}`, data)
}
const putUserInfo = (id, query) => {
    return instance.put(`UserInfo/${id}`, {
        params: query
    })
}
const postAvatarUser = (email, data) => {
    return instance.post(`FileUpload/avatar?email=${email}`, data, {
        headers: {
            'Content-Type': `multipart/form-data; boundary=${data._boundary}`,
        }
    }).then(res => {
        console.log(res)
    }).catch(er => {
        console.log(er)
    })
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
const getQuantityProductByCategory = () => {
    return instance.get(`Products/GetQuantityProductByCategory`)
}
const getQuantityProductByAuthor = () => {
    return instance.get(`Products/GetQuantityProductByAuthor`)
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
    return instance.get(`Products/GetProductWishListByUserId${id}`, {
        params: query
    })
}
const postProductToWishList = (userId, productId) => {
    return instance.post(`/WishListProduct?userId=${userId}&prouctId=${productId}`)
}
const deleteProductInWishList = (userId, productId) => {
    return instance.delete(`/WishListProduct?userId=${userId}&prouctId=${productId}`)
}
const getCountProductInWishList = (userId) => {
    return instance.get(`/WishListProduct/GetTotalWishListAndCart${userId}`)
}
const getListProductMostView = (number) => {
    return instance.get(`Products/GetProductByTheMostView?numberRetrieving=${number}`)
}
const getListProductLatestReleases = (number) => {
    return instance.get(`Products/GetProductByLatestReleases?numberRetrieving=${number}`)
}
const getListProductLowestPrice = (number) => {
    return instance.get(`Products/GetProductByTheLowestPrice?numberRetrieving=${number}`)
}

const postVerifyOrder = (userId) => {
    return instance.post(`Order/verify?userId=${userId}`)
}
const getListOrderByUserId = (userId) => {
    return instance.get(`Order/user/${userId}?page=1&itemPerPage=10`)
}

const putCancelOrder = (orderId) => {
    return instance.put(`Order/cancel/${orderId}`)
}
const postOrder = (data) => {
    return instance.post(`Order`, data)
}

export {getQuantityProductByCategory, getQuantityProductByAuthor, putCancelOrder, getListOrderByUserId, postOrder, postVerifyOrder, postAvatarUser,putUserInfo, putChangePassword, getMyAddress, postCreateUser, postLogin, deleteLogout, getListProduct, getListCategory, getMyInfo, updateViewCountProductById, getProductById, getProduct, postRefreshToken, getListAuthor, getWishListById, postProductToWishList, getListProductMostView, getListProductLatestReleases, getListProductLowestPrice, deleteProductInWishList, getCountProductInWishList}