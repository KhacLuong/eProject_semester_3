import React, {useEffect, useState} from 'react';
import Banner from "../Layouts/Banner/Banner";
import ProductFilter from "./ProductFilter";
import ProductList from "./ProductList";
import {getListProduct} from "../../services/apiService";
import {Data} from "./Data";

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
    const [price, setPrice] = useState([0, 1000]);
    const [page, setPage] = useState(1);
    const [listProducts, setListProducts] = useState([])
    const [totalPage, setTotalPage] = useState(0)

    useEffect(() => {
        fetchListProducts(page, selectedPerPage);
    }, [])


    const renderStar = (count_of_star) => {
        let data = []
        for (let i = 1; i <= count_of_star; i++) {
            data.push(`<svg aria-hidden="true" className="w-5 h-5 text-yellow-400" fill="currentColor" viewBox="0 0 20 20"
                         xmlns="http://www.w3.org/2000/svg">
                    <title>Rating star</title>
                    <path
                            d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z">
                    </path>
                </svg>`)
        }
        return data.join('')
    }
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
        fetchListProducts(page, parseInt(event.target.value));
    };
    const fetchListProducts = async (page, per_page) => {
        setPage(page)
        per_page = parseInt(selectedPerPage)
        // const data = {
        //     name : 'name=' + name;
        //     code = 'code=' + code;
        //     const paramStatus = 'status=' + status;
        //     const paramCategoryId = 'categoryId=' + categoryId;
        //     const paramAuthorId = 'AuthorId=' + AuthorId;
        //     const paramManufactuerId = 'manufactuerId=' + manufactuerId;
        //     const paramLowPrice = 'lowPrice=' + lowPrice;
        //     const paramHightPrice = 'hightPrice=' + hightPrice;
        //     const paramSortBy = 'sortBy=' + sortBy;
        //     const paramPageSize = 'pageSize=' + pageSize;
        //     const paramPageIndex = 'pageIndex=' + pageIndex;
        // }
        // let res = Data
        // if(res.EC === 0) {
        //     setTotalPage(Math.ceil(+res.DT.products.length / per_page))
        //     setListProducts(res.DT.products)
        // }
        let res = await getListProduct(page, per_page)
        if (res.status === true) {
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