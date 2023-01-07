import { combineReducers } from 'redux';
import userReducer from "./userReducer";
import CartReducer from "./cartReducer";

const rootReducer = combineReducers({
    user: userReducer,
    cart: CartReducer
});

export default rootReducer;