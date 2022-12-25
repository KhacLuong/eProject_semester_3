import React from 'react';
import { useLocation } from 'react-router-dom';
import {Data} from "./Data";
const ProductDetail = () => {
    const { state } = useLocation();
    const getProductById = () => {

    }
    return (
        <div>
            this is details product id = {state}
        </div>
    );
};

export default ProductDetail;