import {useNavigate} from 'react-router-dom';
import {BsFillGridFill} from 'react-icons/bs'
import {FaThList} from 'react-icons/fa'
import './Product.scss'
import ReactPaginate from 'react-paginate';
import parse from "html-react-parser";
import {FiHeart, FiEye} from "react-icons/fi";
import {TbShoppingCart} from "react-icons/tb"
import {useState} from "react";
import book3 from "../../assets/image/books/book3.png"

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
        fetchListProducts,
        renderStar
    } = props
    const [hover, setHover] = useState(false);
    const [idProduct, setIdProduct] = useState(0);
    const [turnOffPrevNextBtn, setTurnOffPrevNextBtn] = useState(true)
    const handlePageClick = (event) => {
        fetchListProducts(+event.selected + 1, selectedPerPage)
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
    return (
        <div className={`product_list col-span-3`}>
            <div className={`shadow-md flex items-start justify-between border-b-[1px] py-2 pl-2`}>
                <div className={`flex items-center text-xl`}>
                    <BsFillGridFill title={`Grid View`} className={`cursor-pointer mr-2 hover:text-black`}/>
                    {/*<FaThList title={`List View`} className={`cursor-pointer text-lightColor hover:text-black`}/>*/}
                </div>
                <div className={`flex items-center`}>
                    <div>
                        <select value={selectedSort} onChange={handleChangeSort}
                                className="cursor-pointer text-right block py-0 pr-[30px] text-sm leading-6 text-gray-500 bg-transparent border-0  appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer ">
                            {optionSort.map((option, index) => (
                                <option key={index} value={option.value}>
                                    {option.text}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div
                        className={`relative flex items-center pl-[19px] ml-[14px] after:content-[''] after:absolute after:w-[1px] after:h-[20px] after:bg-lightColor after:left-0 after:top-[10%] `}>
                        <label htmlFor={`per_page`} className={`text-lightColor font-normal text-sm`}>Show</label>
                        <select name={`per_page`} id={`per_page`}
                                onChange={handleChangeQuantity}
                                value={selectedPerPage}
                                className="cursor-pointer py-0 text-left block text-sm leading-6 text-gray-500 bg-transparent border-0 appearance-none dark:text-gray-400 focus:outline-none focus:ring-0 peer">
                            {optionQuantity.map((option, index) => (
                                <option key={index} value={option.value}>
                                    {option.text}
                                </option>
                            ))}
                        </select>
                    </div>
                </div>
            </div>
            <div className={``}>
                <div className={`render_product grid grid-cols-3 gap-4 py-3`}>
                    {
                        listProducts.map((item, index) => {
                            return <div
                                className={`w-full py-5 border-b-[1px] border-r-[1px] border-l-[1px] flex justify-center items-center shadow-lg rounded-2xl`}
                                key={index}>
                                <div>
                                    <div className={`product-transition relative w-[300px] h-[420px]`}
                                         onMouseLeave={() => setHover(false)}>
                                        <div title={`${item.description}`}
                                             onClick={() => navigate(`product-detail/${item.id}/${item.slug}`)}
                                             onMouseOver={(e) => handleOnMouseOver(e, index)} state={item}
                                             className={`overflow-hidden rounded-2xl cursor-pointer`}>
                                            <img className={`w-full rounded-2xl block my-0 mx-auto`}
                                                 src={book3}/>
                                        </div>
                                        <div className={`group_action absolute right-[10px] bottom-[10px] z-10`}>
                                            <div className={`shop_action flex flex-col items-start relative`}>
                                                <button
                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1'
                                                        + ' visible translate-x-0' : 'opacity-0'
                                                        + ' translate-x-8'} 
                                                actionBtn text-dangerColor-default_3 transition duration-300 ease-in-out`}>
                                                    <FiHeart/></button>
                                                <div onClick={() => navigate(`product-detail/${item.id}/${item.slug}`)}
                                                     state={item.id}
                                                     className={`${hover && idProduct === index + 1 ? 'opacity-1'
                                                         + ' visible'
                                                         + ' translate-x-0' : 'opacity-0 translate-x-8'
                                                         + ' invisible'} actionBtn delay-100 transition duration-300 ease-in-out`}>
                                                    <FiEye/>
                                                </div>
                                                <button
                                                    className={`${hover && idProduct === index + 1 ? 'opacity-1'
                                                        + ' visible translate-x-0' : 'opacity-0'
                                                        + ' translate-x-8 invisible'} actionBtn delay-200 transition duration-300 ease-in-out`}>
                                                    <TbShoppingCart/></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div onMouseOver={(e) => handleOnMouseOver(e, index)}
                                         className={`product-caption relative pt-[20px] flex flex-col `}>
                                        <div onClick={() => navigate(`product-detail/${item.id}/${item.slug}`)}
                                             className={`cursor-pointer text-xl font-semibold overflow-hidden mb-1`}>
                                            <h2>{item.name}</h2>
                                        </div>
                                        <div
                                            className={`cursor-pointer detail_product_author text-xs text-lightColor leading-none font-normal hover:text-dangerColor-default_2`}>
                                            <div onClick={() => navigate(``)}>{item.author}</div>
                                        </div>
                                        <div className={`count_review text-xs my-[10px] flex items-center font-semibold`}>
                                            <div className={`flex items-center`}>
                                                {parse(renderStar(4))}
                                                <svg aria-hidden="true"
                                                     className="w-4 h-4 text-gray-300 dark:text-gray-500 "
                                                     fill="currentColor" viewBox="0 0 20 20"
                                                     xmlns="http://www.w3.org/2000/svg"><title>Fifth star</title>
                                                    <path
                                                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                                </svg>
                                            </div>
                                            <span className={`ml-2`}>5</span>
                                        </div>
                                        <div
                                            className={`detail_product_price flex items-center justify-between leading-normal mt-2`}>
                                            <div className={`text-xl font-bold text-dangerColor-default_2`}>
                                                ${item.price}
                                            </div>
                                            <div className={`flex items-center`}>
                                                <div className={`mr-1 text-xs font-medium`}>Q.ty:</div>
                                                <div>{item.quantity}</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        })
                    }
                    {listProducts && listProducts.left === 0 && <div></div>}
                </div>
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
        </div>
    );
};

export default ProductList;