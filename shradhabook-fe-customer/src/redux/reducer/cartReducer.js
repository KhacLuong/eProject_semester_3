import {FETCH_ADD_PRODUCT_TO_CART} from "../action/cartAction";

const INITIAL_STATE = {
    isOpenCartModel: false,
};

const CartReducer = (state = INITIAL_STATE, action) => {
    switch (action.type) {
        case FETCH_ADD_PRODUCT_TO_CART:
            return {
                isOpenCartModel: true
            };
        default:
            return state;
    }
};

export default CartReducer;