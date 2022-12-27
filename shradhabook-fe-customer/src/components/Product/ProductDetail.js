import React from 'react';
import {useParams} from 'react-router-dom';

const ProductDetail = () => {
    const {id, slug} = useParams();
    const getProductById = () => {

    }
    return (
        <div>
            this is details product id = {id}
        </div>
    );
};

export default ProductDetail;