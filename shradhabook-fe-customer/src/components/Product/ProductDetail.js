import React from 'react';
import {useLocation, useParams} from 'react-router-dom';
import {Data} from "./Data";
const ProductDetail = () => {
    const { id, slug } = useParams();
    const location = useLocation().pathname.split('/')
    console.log('id: ', id, 'slug: ', slug)
    const getProductById = () => {

    }
    return (
        <div>
            this is details product id = {location}
        </div>
    );
};

export default ProductDetail;