import {FETCH_USER_LOGIN_SUCCESS, FETCH_USER_LOGOUT_SUCCESS, REFRESH_USER_TOKEN} from "../action/userAction";

const INITIAL_STATE = {
    account: {
        access_token: '',
        refresh_token: '',
        username: '',
        email: '',
        user_type: '',
    },
    isAuthenticated: false,
};

const userReducer = (state = INITIAL_STATE, action) => {
    switch (action.type) {
        case FETCH_USER_LOGIN_SUCCESS:
            return {
                ...state,
                account: {
                    accessToken: action?.payload?.data?.accessToken,
                    refreshToken: action?.payload?.data?.refreshToken,
                    username: action?.payload?.data?.name,
                    email: action?.payload?.data?.email,
                    user_type: action?.payload?.data?.userType,
                },
                isAuthenticated: true
            };
        case FETCH_USER_LOGOUT_SUCCESS:
            return {
                ...state,
                account: {
                    access_token: '',
                    refresh_token: '',
                    username: '',
                    email: '',
                    user_type: '',
                },
                isAuthenticated: false
            }
        case REFRESH_USER_TOKEN:
            return {
                ...state,
                account: {
                    accessToken: action?.payload?.data?.accessToken,
                    refreshToken: action?.payload?.data?.refreshToken,
                    username: action?.payload?.data?.name,
                    email: action?.payload?.data?.email,
                    user_type: action?.payload?.data?.userType,
                },
                isAuthenticated: true
            }
        default:
            return state;
    }
};

export default userReducer;