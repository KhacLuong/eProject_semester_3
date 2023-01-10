import React, {useEffect, useState} from 'react';
import Banner from "../Layouts/Banner/Banner";
import ProductFilter from "./ProductFilter/ProductFilter";
import ProductList from "./ProductAll/ProductList";
import {getListAuthor, getListProduct, getListCategory} from "../../services/apiService";
import {renderStar} from "../../ultis/renderStar";
import jwt_decode from "jwt-decode";
import {useSelector} from "react-redux";

const ProductPage = (props,{doAddToCart}) => {
    const {setOpen} = props
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
        {id: 0, value: '', text: 'Default sorting'},
        {id: 1, value: 'popularity', text: 'Sort by name'},
        {id: 2, value: 'rating', text: 'Sort by most view'},
        {id: 3, value: 'date', text: 'Sort by release date'},
        {id: 4, value: 'price', text: 'Sort by price: high to low'},
        {id: 5, value: 'price-desc', text: 'Sort by price: low to high'},
    ]
    const optionQuantity = [
        {value: '6', text: '6'},
        {value: '9', text: '9'},
        {value: '12', text: '12'},
        {value: '15', text: '15'},
        {value: '18', text: '18'},
    ]

    const [selectedSort, setSelectedSort] = useState(optionSort[0].id);
    const [selectedPerPage, setSelectedPerPage] = useState(optionQuantity[1].value);
    const [listProducts, setListProducts] = useState([])
    const [listAuthor, setListAuthor] = useState([])
    const [listCategory, setListCategory] = useState([])
    const [price, setPrice] = useState([0, 10000]);

    const [pageIndex, setPageIndex] = useState(1);
    const [totalPage, setTotalPage] = useState(0);
    const [name, setName] = useState('');
    const [code, setCode] = useState('');
    const [status, setStatus] = useState('');
    const [categoryName, setCategoryName] = useState('');
    const [AuthorName, setAuthorName] = useState('');
    const [lowPrice, setLowPrice] = useState(null);
    const [hightPrice, setHightPrice] = useState(null);
    const [activeCategory, setActiveCategory] = useState('');
    const [activeAuthor, setActiveAuthor] = useState('');
    const [bannerTitle, setBannerTitle] = useState('product')
    const account = useSelector(state => state.user.account);
    const [userId, setUserId] = useState('')

    let decoded = ''
    useEffect(() => {
        if (account.accessToken) {
            decoded = jwt_decode(account.accessToken);
            setUserId(decoded.Id)
        }
        const fetchData = async () => {
            await fetchListProducts(pageIndex)
            await fetchListAuthor()
            await fetchListCategory()
        }
        fetchData();
    }, [])

    useEffect(() => {
        const fetchData = async () => {
            await fetchListProducts(pageIndex)
        }
        fetchData();
    }, [selectedPerPage, lowPrice, hightPrice,AuthorName, categoryName, name, selectedSort ])

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
        setLowPrice(price[0])
        setHightPrice(price[1])
    }
    const handleChangeSort = event => {
        setSelectedSort(event.target.value);
    };
    const handleChangeQuantity = event => {
        setSelectedPerPage(event.target.value)
    };
    const handleOnClickCategory = (name, id) => {
        setCategoryName(name);
        setBannerTitle(name)
        const classCategory = `category_${id}`
        setActiveCategory(classCategory)
    }
    const handleOnClickAuthor = (name, id) => {
        setAuthorName(name);
        const classAuthor = `author_${id}`
        setActiveAuthor(classAuthor)
    }
    const handleSearchProduct = (e) => {
        setName(e.target.value);
    }
    const handleRefreshCategory = () => {
        setCategoryName('')
        setActiveCategory('')
        setBannerTitle('product')
    }
    const handleRefreshAuthor = () => {
        setAuthorName('')
        setActiveAuthor('')
    }

    const fetchListProducts = async (page) => {
        setPageIndex(page)
        const params = {
            'name': name,
            'code': code,
            'status': status,
            'categoryName': categoryName,
            'AuthorName': AuthorName,
            'lowPrice': lowPrice,
            'hightPrice': hightPrice,
            'sortBy': selectedSort,
            'pageSize': selectedPerPage,
            'pageIndex': page
        }
        let res = await getListProduct(params)
        if (res.status === true) {
            setTotalPage(res.data.totalPage)
            setListProducts(res.data.products)
        }
    }

    const fetchListAuthor = async () => {
        let res = await getListAuthor()
        if (res.status === true) {
            setListAuthor(res.data)
        }
    }
    const fetchListCategory = async () => {
        let res = await getListCategory()
        if (res.status === true) {
            setListCategory(res.data)
        }
    }

    return (
        <div className={`product_page`}>
            <Banner bannerTitle={bannerTitle}/>
            <div className={`product_content container mx-auto xl:px-30 grid grid-cols-4 gap-8 py-14`}>
                <ProductFilter
                    handleOnClickAuthor={handleOnClickAuthor}
                    handleUpdatePrice={handleUpdatePrice}
                    handleFilterByPrice={handleFilterByPrice}
                    handleOnClickCategory={handleOnClickCategory}
                    handleRefreshCategory={handleRefreshCategory}
                    handleRefreshAuthor={handleRefreshAuthor}
                    renderStar={renderStar}
                    stars={stars}
                    price={price}
                    listAuthor={listAuthor}
                    listCategory={listCategory}
                    activeCategory={activeCategory}
                    activeAuthor={activeAuthor}
                />
                <ProductList
                    userId={userId}
                    totalPage={totalPage}
                    listProducts={listProducts}
                    selectedSort={selectedSort}
                    optionQuantity={optionQuantity}
                    optionSort={optionSort}
                    selectedPerPage={selectedPerPage}
                    handleChangeSort={handleChangeSort}
                    handleChangeQuantity={handleChangeQuantity}
                    handleSearchProduct={handleSearchProduct}
                    fetchListProducts={fetchListProducts}
                    renderStar={renderStar}
                    setOpen={setOpen}
                    doAddToCart={doAddToCart}
                />
            </div>
        </div>
    );
};


export default ProductPage;