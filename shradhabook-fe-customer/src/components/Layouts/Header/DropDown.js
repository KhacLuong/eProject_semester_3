import React from 'react';
import MenuItem from "./MenuItem";

const DropDown = (props) => {
    let {depthLevel, children, dropdown } = props
    depthLevel += + 1
    const dropDownClass = depthLevel > 1 ? 'dropdown-submenu' : "";

    return (
        <ul className = {
            `dropdown show ${dropDownClass} ${dropdown ? "show" : ""}`
        }>
            {
                children.map((children, index) => {
                    return <MenuItem items={children} key={index} depthLevel={depthLevel}/>
                })
            }
        </ul>
    );
};

export default DropDown;