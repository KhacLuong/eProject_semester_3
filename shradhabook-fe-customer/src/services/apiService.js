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

export {postCreateUser}