export const ADD_PRODUCT_TO_CART = 'ADD_PRODUCT_TO_CART'
export const REMOVE_FROM_CART = 'REMOVE_FROM_CART'
export const ADJUST_QTY = 'ADJUST_QTY'
export const LOAD_CURRENT_ITEM = 'LOAD_CURRENT_ITEM'
export const doAddToCart = (data) => {
    return {
        type: ADD_PRODUCT_TO_CART,
        payload: {
            data: data
        }
    }
}
export const doRemoveFromCart = (id) => {
    return {
        type: REMOVE_FROM_CART,
        payload: {
            id: id
        }
    }
}
export const adJustQty = (id, value) => {
    return {
        type: ADJUST_QTY,
        payload: {
            id: id,
            qty: value
        }
    }
}
export const loadCurrentItem = (id) => {
    return {
        type: LOAD_CURRENT_ITEM,
        payload: id
    }
}