import {postProductToWishList} from "../services/apiService";
import {toast} from "react-toastify";

export const AddProductToWishList = async (userId, productId) => {
    let res = await postProductToWishList(userId, productId)
    if (res && res.status === true) {
        toast.success(res.message);
    } else {
        toast.error(res.message);
    }
}