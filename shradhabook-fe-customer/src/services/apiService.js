import axios from "axios";

const postCreateUser = (name, email, password, confirmPassword, userType) => {
    const data = {
        "name": name,
        "email": email,
        "password": password,
        "confirmPassword": confirmPassword,
        "userType": userType
    }
    return axios.post('https://localhost:7000/api/User/register', data)
        .then(function (response) {
            return response
        })
        .catch(function (error) {
            return error
        });
}

export {postCreateUser}