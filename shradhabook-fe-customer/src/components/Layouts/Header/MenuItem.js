import React from 'react';
import {useState, useEffect, useRef} from "react";
import DropDown from "./DropDown";
import {useNavigate} from "react-router-dom";
import {getListProduct} from "../../../services/apiService";
import {BiChevronDown, BiChevronRight} from 'react-icons/bi'

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

    const onMouseClick = () => {
        window.innerWidth > 960 && setDropdown(true);
    }
    const onMouseLeave = (e) => {
        console.log(e.offsetTop)
        window.innerWidth > 1500 && setDropdown(false);
    }

    return (
        <li className="menu-items py-5" ref={ref} onMouseOver={onMouseClick} onMouseLeave={onMouseLeave}>
            {
                items.children.length !== 0 ? (
                    <>
                        <button className={`flex items-center justify-between px-4`} type="button" aria-haspopup="menu" aria-expanded={dropdown ? "true" : "false"}
                                onClick={() => setDropdown((prev) => !prev)}>
                            {items.name}
                            {" "}
                            {depthLevel > 0 && items.children.length != 0 ? <BiChevronRight className={`text-xl`}/> : <BiChevronDown className={`ml-1 text-xl`}/>}
                        </button>
                        <DropDown depthLevel={depthLevel}
                                  children={items.children}
                                  dropdown={dropdown}/>
                    </>
                ) : (
                    <div className={`cursor-pointer px-4`} >
                        {items.name}
                    < /div>
                )
            }
        </li>
    );
};

export default MenuItem;