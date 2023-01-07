import {useNavigate} from 'react-router-dom';
import {BsFillGridFill} from 'react-icons/bs'
import '../Product.scss'
import ReactPaginate from 'react-paginate';
import {FaThList} from "react-icons/fa";
import React, {useState} from "react";
import book14 from "../../../assets/image/books/book14.png"
import {updateViewCountProductById} from "../../../services/apiService";
import ProductGridView from "./ProductGridView";
import ProductListView from "./ProductListView";
import {connect} from "react-redux";
import {doAddToCart} from '../../../redux/action/cartAction'

const ProductList = (props) => {
    const navigate = useNavigate();
    const {
        totalPage,
        optionSort,
        optionQuantity,
        selectedPerPage,
        selectedSort,
        listProducts,
        handleChangeSort,
        handleChangeQuantity,
        handleSearchProduct,
        fetchListProducts,
        renderStar,
        setOpen,
        userId,
    } = props

    const [hover, setHover] = useState(false);
    const [idProduct, setIdProduct] = useState(0);
    const [turnOffPrevNextBtn, setTurnOffPrevNextBtn] = useState(true)
    const [imageProduct, setImageProduct] = useState(book14);
    const [activeLayout, setActiveLayout] = useState(true);
    const handlePageClick = (event) => {
        fetchListProducts(+event.selected + 1)
        console.log(`User requested page number ${+event.selected + 1}`)
        if (+event.selected + 1 === 1) {
            setTurnOffPrevNextBtn(true)
        }
        if (+event.selected + 1 === totalPage) {
            setTurnOffPrevNextBtn(true)
        }
    };
    const handleOnMouseOver = (event, index) => {
        setHover(true);
        setIdProduct(index + 1);
    }
    const handleChangeLayout = () => {
        setActiveLayout(!activeLayout)
    }
    const handleClickGoProductDetail = async (id, slug) => {
        let res = await updateViewCountProductById(id)
        if (res === 'Success') {
            navigate(`product-detail/${id}/${slug}`)
        }
    }

    return (<div className={`product_list col-span-3`}>
        <div className={`shadow-md flex items-start justify-between border-b-[1px] py-2 pl-2`}>
            <div className={`flex items-center text-xl`} onClick={handleChangeLayout}>
                {
                    activeLayout
                        ? <BsFillGridFill title={`Grid View`}
                                          className={`cursor-pointer mr-2 text-blackColor hover:text-dangerColor-default_2`}/>
                        : <FaThList title={`List View`}
                                    className={`cursor-pointer text-blackColor hover:text-dangerColor-default_2`}/>
                }
            </div>
            <div className={`grid grid-cols-2`}>
                <div
                    className={`col-span-1 flex items-center py-0 text-sm leading-6 text-gray-500 bg-transparent border-0 dark:text-gray-400 focus:outline-none focus:ring-0 min-w-full`}>
                    <div className={`mr-2 font-medium text-blackColor`}>
                        Search:
                    </div>
                    <input onChange={handleSearchProduct} className={`w-full border-none outline-none`}
                           placeholder={`products...`}/>
                </div>
                <div className={`col-span-1 flex items-center`}>
                    <div
                        className={`relative after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-12 after:top-[10%]`}>
                        <select value={selectedSort} onChange={handleChangeSort}
                                className="cursor-pointer text-right block py-0 pr-[30px] text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer ">
                            {optionSort.map((option, index) => (<option key={index} value={option.id}>
                                {option.text}
                            </option>))}
                        </select>
                    </div>
                    <div
                        className={`relative flex items-center pl-[19px] ml-[14px] after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-0 after:top-[10%] `}>
                        <label htmlFor={`per_page`} className={`text-lightColor font-normal text-sm`}>Show</label>
                        <select name={`per_page`} id={`per_page`}
                                onChange={handleChangeQuantity}
                                value={selectedPerPage}
                                className="cursor-pointer py-0 text-left block text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer">
                            {optionQuantity.map((option, index) => (<option key={index} value={option.value}>
                                {option.text}
                            </option>))}
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div className={`show_product_list`}>
            {
                activeLayout
                    ? <ProductGridView userId={userId} listProducts={listProducts} hover={hover} setHover={setHover}
                                       handleClickGoProductDetail={handleClickGoProductDetail}
                                       handleOnMouseOver={handleOnMouseOver} imageProduct={imageProduct}
                                       idProduct={idProduct} renderStar={renderStar} setOpen={setOpen}
                                       doAddToCart={props.doAddToCart}/>
                    : <ProductListView userId={userId} listProducts={listProducts} hover={hover} setHover={setHover}
                                       handleClickGoProductDetail={handleClickGoProductDetail}
                                       handleOnMouseOver={handleOnMouseOver} imageProduct={imageProduct}
                                       idProduct={idProduct} renderStar={renderStar} setOpen={setOpen}
                                       doAddToCart={props.doAddToCart}/>
            }
            <div className={`pagination flex items-center justify-center mt-10`}>
                <ReactPaginate
                    nextLabel="next"
                    previousLabel="previous"
                    onPageChange={handlePageClick}
                    pageRangeDisplayed={3}
                    marginPagesDisplayed={2}
                    pageCount={totalPage}
                    pageClassName="pageClassName"
                    pageLinkClassName="pageLinkClassName font-semiBold"
                    previousClassName="previousClassName"
                    previousLinkClassName="previousLinkClassName font-semiBold"
                    nextClassName="nextClassName"
                    nextLinkClassName="nextLinkClassName font-semiBold"
                    breakLabel="..."
                    breakClassName="breakLinkClassName text-xl font-medium"
                    containerClassName="flex items-center justify-center inline-flex -space-x-px"
                    activeLinkClassName="text-whiteColor bg-dangerColor-default_2"
                    renderOnZeroPageCount={null}
                    disabledClassName={turnOffPrevNextBtn ? 'hidden' : ''}
                    disabledLinkClassName={turnOffPrevNextBtn ? 'hidden' : ''}
                />
            </div>
        </div>
    </div>);
};
const mapDispatchToProps = dispatch => {
    return {
        doAddToCart: (data) => dispatch(doAddToCart(data))
    }
}
export default connect(null,mapDispatchToProps)(ProductList);