import React from 'react';

const DropDown = ({submenu,dropdown,depthlevel}) => {
    depthlevel = depthlevel+1
    const dropDownClass = depthlevel > 1 ? 'dropdown-submenu' : ''
    return (
        <div>
            
        </div>
    );
};

export default DropDown;