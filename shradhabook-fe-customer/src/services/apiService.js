import axios from "../ultis/axiosCustomize";
const postCreateUser = (name, email, password, confirmPassword, userType) => {
    const data = {
        "name": name,
        "email": email,
        "password": password,
        "confirmPassword": confirmPassword,
        "userType": userType
    }
    return axios.post('User/register', data)
}
const postLogin = (email, password) => {
    const data = {
        "email": email,
        "password": password,
    }
    return axios.post('User/login', data)
}
const getListProduct = (page, limit, price, sortBy) => {
    // const data = {
    //     "page": page ?? null,
    //     "limit": limit ?? null,
    //     "price": price ?? null,
    //     "sortBy": sortBy
    // }
    // return axios.get('Products', data)
}
export {postCreateUser, postLogin, getListProduct}