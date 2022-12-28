import React, {useEffect} from 'react';
import {useParams} from 'react-router-dom';
import {fetProductById} from "../../services/apiService";

const ProductDetail = () => {
    const {id, slug} = useParams();

    useEffect(() => {

    }, [])
    const getProductById = () => {

    }

    return (
        <div className={`product_details`}>
            <div className={`breadcrumb_wrap`}>
                <nav className={`woocommerce_breadcrumb`}>
                    <a>Home</a>
                    <a>List posts</a>
                    <a className={`active`}>Top 10 Books to Make It a Great Year</a>
                </nav>
            </div>
        </div>
    );
};

export default ProductDetail;