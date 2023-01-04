import React, {useEffect, useState} from 'react';
import Banner from "../Layouts/Banner/Banner";
import ProductFilter from "./ProductFilter";
import ProductList from "./ProductList";
import {getListProduct} from "../../services/apiService";
import {renderStar} from "../../ultis/renderStar";

const ProductPage = () => {
    const minDistance = 0;
    const stars = [
        {
            star: 5, count: 20, path: ''
        },
        {
            star: 4, count: 12, path: ''
        },
        {
            star: 3, count: 16, path: ''
        },
        {
            star: 2, count: 6, path: ''
        },
        {
            star: 1, count: 2, path: ''
        },
    ]
    const optionSort = [
        {value: '', text: 'Default sorting'},
        {value: 'popularity', text: 'Sort by popularity'},
        {value: 'rating', text: 'Sort by average rating'},
        {value: 'date', text: 'Sort by latest'},
        {value: 'price', text: 'Sort by price: low to high'},
        {value: 'price-desc', text: 'Sort by price: high to low'},
    ]
    const optionQuantity = [
        {value: '6', text: '6'},
        {value: '9', text: '9'},
        {value: '12', text: '12'},
        {value: '15', text: '15'},
        {value: '18', text: '18'},
    ]

    const [selectedSort, setSelectedSort] = useState(optionSort[0].value);
    const [selectedPerPage, setSelectedPerPage] = useState(optionQuantity[2].value);
    const [listProducts, setListProducts] = useState([])
    const [price, setPrice] = useState([0, 1000]);

    const [page, setPage] = useState(1);
    const [totalPage, setTotalPage] = useState(0);
    const [name, setName] = useState('');
    const [code, setCode] = useState('');
    const [status, setStatus] = useState('');
    const [categoryId, setCategoryId] = useState(null);
    const [AuthorId, setAuthorId] = useState(null);
    const [manufactuerId, setManufactuerId] = useState(null);
    const [lowPrice, setLowPrice] = useState(null);
    const [hightPrice, setHightPrice] = useState(null);
    const [sortBy, setSortBy] = useState(null);

    useEffect(() => {
        fetchListProducts().then((res) => {
            return res
        });
    }, [])

    const handleUpdatePrice = (e, data, activeThumb) => {
        if (!Array.isArray(data)) {
            return;
        }
        if (activeThumb === 0) {
            setPrice([Math.min(data[0], data[1] - minDistance), data[1]]);
        } else {
            setPrice([price[0], Math.max(data[1], price[0] + minDistance)]);
        }
    }
    const handleFilterByPrice = () => {

    }
    const handleChangeSort = event => {
        setSelectedSort(event.target.value);
    };
    const handleChangeQuantity = event => {
        setSelectedPerPage(event.target.value);
        fetchListProducts().then((res) => {
            return res
        });
    };
    const fetchListProducts = async () => {
        setPage(page)
        const params = {
            'name': name,
            'code': code,
            'status': status,
            'categoryId': categoryId,
            'AuthorId': AuthorId,
            'manufactuerId': manufactuerId,
            'lowPrice': lowPrice,
            'hightPrice': hightPrice,
            'sortBy': sortBy,
            'pageSize': selectedPerPage,
            'pageIndex': page
        }
        let res = await getListProduct(params)
        if (res.status === true) {
            setTotalPage(res.data.totalPage)
            setListProducts(res.data.products)
        }
        // console.log(page, per_page);
    }

    return (
        <div className={`product_page`}>
            <Banner bannerTitle={`product`}/>
            <div className={`product_content container mx-auto xl:px-30 grid grid-cols-4 gap-8 py-14`}>
                <ProductFilter
                    handleUpdatePrice={handleUpdatePrice}
                    handleFilterByPrice={handleFilterByPrice}
                    renderStar={renderStar}
                    stars={stars}
                    price={price}
                />
                <ProductList
                    totalPage={totalPage}
                    listProducts={listProducts}
                    selectedSort={selectedSort}
                    optionQuantity={optionQuantity}
                    optionSort={optionSort}
                    selectedPerPage={selectedPerPage}
                    handleChangeSort={handleChangeSort}
                    handleChangeQuantity={handleChangeQuantity}
                    fetchListProducts={fetchListProducts}
                    renderStar={renderStar}
                />
            </div>
        </div>
    );
};

export default ProductPage;