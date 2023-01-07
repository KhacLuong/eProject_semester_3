import {ADD_PRODUCT_TO_CART, REMOVE_FROM_CART, ADJUST_QTY, REMOVE_ALL_ITEMS} from "../action/cartAction";

const INITIAL_STATE = {
    cart: [], // {id, name, description, price, img, qty}
    currentItem: null
};

const CartReducer = (state = INITIAL_STATE, action) => {
    switch (action.type) {
        case ADD_PRODUCT_TO_CART:
            // Check if item is in cart already
            const inCart = state.cart.find(item => item.id === action.payload.data.id ? true : false)
            return {
                ...state,
                cart: inCart
                    ? state.cart.map(item => item.id === action.payload.data.id ? {...item, qty: item.qty + 1} : item)
                    : [...state.cart, {...action.payload.data, qty: 1}]
            }
        case REMOVE_FROM_CART:
            return {
                ...state,
                cart: state.cart.filter(item => item.id !== action.payload.id)
            }
        case ADJUST_QTY:
            return {
                ...state,
                cart: state.cart.map(item => item.id === action.payload.id ? {...item, qty: +action.payload.qty} : item)
            }
        case REMOVE_ALL_ITEMS:
            return {
                ...state,
                cart: []
            }
        default:
            return state;
    }
};

export default CartReducer;