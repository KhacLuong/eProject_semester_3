import React from 'react';
import {useState, useEffect, useRef} from "react";
import DropDown from "./DropDown";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import {getListProduct} from "../../../services/apiService";

const MenuItem = ({items, depthLevel}) => {
    const [dropdown, setDropdown] = useState(false)
    const navigate = useNavigate();
    let ref = useRef();
    useEffect(() => {
        const handler = (event) => {
            if (dropdown && ref.current && !ref.current.contains(event.target)) {
                setDropdown(false)
            }
        };
        document.addEventListener("mousedown", handler);
        document.addEventListener("touchstart", handler);
        return () => {
            // cleanup the event listener
            document.removeEventListener("mousedown", handler);
            document.removeEventListener("touchstart", handler);
        }
    }, [dropdown])

    const onMouseEnter = () => {
        window.innerWidth > 960 && setDropdown(true);
    }
    const onMouseLeave = () => {
        window.innerWidth > 960 && setDropdown(false);
    }
    const test = async (id) => {
        const params = {
            'categoryId': id,
        }
        let res = await getListProduct(params)
        if (res.status === true) {
            navigate('/Products')
        }
    }
    return (
        <li className="menu-items" ref={ref} onMouseEnter={onMouseEnter} onMouseLeave={onMouseLeave}>
            {
                items.children.length !== 0 ? (
                    <>
                        <button type="button" aria-haspopup="menu" aria-expanded={dropdown ? "true" : "false"}
                                onClick={() => setDropdown((prev) => !prev)}>
                            {items.name}
                            {" "}
                            {depthLevel > 0 && items.children.length != 0 ? <span> &raquo; </span> : <span className="arrow"/>}
                        </button>
                        <DropDown depthLevel={depthLevel}
                                  children={items.children}
                                  dropdown={dropdown}/>
                    </>
                ) : (
                    <div className={`cursor-pointer`} onClick={()=>test(items.id)}>
                        {items.name}
                    < /div>
                )
            }
        </li>
    );
};

export default MenuItem;