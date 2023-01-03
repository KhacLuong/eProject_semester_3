import React, {useEffect, useState} from 'react';
import {NavLink} from "react-router-dom";
import MenuItem from "./MenuItem";
import {getListCategory} from "../../../services/apiService";
import './nav.scss'
import {BiChevronDown} from "react-icons/bi";

const MenuNavigation = () => {
    const defaultClass = `block py-2 mx-5 text-blackColor hover:text-dangerColor-default_2 md:p-0 ease-in duration-200 text-base font-medium`
    const [listCategory, setListCategory] = useState([]);

    useEffect(() => {
        fetchListCategories()
    }, [])

    const fetchListCategories = async () => {
        let res = await getListCategory();
        let menuCategories = [{
            name: "Categories",
            children: []
        }]
        if(res.status === true) {
            menuCategories[0].children = convertArrayToRecursive(res.data)
        } else {
            menuCategories[0].children = []
        }
        setListCategory(menuCategories)
    }
    const convertArrayToRecursive = (arr, parentId = 0) => {
        if (arr.length !== 0) {
            return arr.filter((item) => {
                return item.parentId === parentId;
            }).map((child) => {
                return {...child, children: convertArrayToRecursive(arr, child.id)};
            });
        }
        return [];
    };

    return (
        <div className="items-center justify-between flex">
            <ul className="flex flex-col p-4 mt-4 md:flex-row  md:mt-0 md:text-sm md:border-0">
                <NavLink to={'/'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Home
                </NavLink>
                <a className={`flex items-center justify-center hover:text-dangerColor-default_2`}>
                    <ul className="menus"> {
                        listCategory.map((menu, index) => {
                            const depthLevel = 0;
                            return <MenuItem items={menu}
                                             key={index}
                                             depthLevel={depthLevel}/>;
                        })
                    } </ul>
                </a>
                <NavLink to={'/products'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Products
                </NavLink>
                <a className={`block py-2 mx-5 cursor-pointer text-blackColor hover:text-dangerColor-default_2 md:p-0 ease-in duration-200 text-base font-medium`}>
                    <div className={`flex`}>
                        Pages<BiChevronDown className={`ml-1 text-xl`}/>
                    </div>
                    <ul>

                    </ul>
                </a>
                <NavLink to={'/blogs'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Blogs
                </NavLink>
                <NavLink to={'/contact'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Contact
                </NavLink>
            </ul>
        </div>
    );
};

export default MenuNavigation;