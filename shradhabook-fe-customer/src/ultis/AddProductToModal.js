import {doAddToCart} from "../redux/action/cartAction";
import {useDispatch} from "react-redux";

export const AddProductToModal = (data) => {
    const dispatch = useDispatch();
    dispatch(doAddToCart(data))
}