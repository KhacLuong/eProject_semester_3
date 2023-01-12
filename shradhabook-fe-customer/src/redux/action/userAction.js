export const FETCH_USER_LOGIN_SUCCESS = 'FETCH_USER_LOGIN_SUCCESS'
export const FETCH_USER_LOGOUT_SUCCESS = 'FETCH_USER_LOGOUT_SUCCESS'
export const REFRESH_USER_TOKEN = 'REFRESH_USER_TOKEN'
export const doLogin = (data) => {
    return {
        type: FETCH_USER_LOGIN_SUCCESS,
        payload: data
    }
}

export const doLogout = () => {
    return {
        type: FETCH_USER_LOGOUT_SUCCESS,
    }
}

export const doRefreshToken = (data) => {
    return {
        type: REFRESH_USER_TOKEN,
        payload: data
    }
}