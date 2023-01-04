import React, {useEffect, useState} from 'react';
import {NavLink} from "react-router-dom";
import MenuItem, {menuItems} from "./MenuItem";
import {getListCategory} from "../../../services/apiService";
import './nav.scss'

const MenuNavigation = () => {
    const defaultClass = `block py-2 pl-3 pr-4 text-blackColor rounded hover:text-dangerColor-default_2 md:p-0 ease-in duration-100 text-base font-medium`
    const [listCategory, setListCategory] = useState([]);

    useEffect(() => {
        fetchListCategories()
    }, [])

    const fetchListCategories = async () => {
        let res = await getListCategory();
        if(res.status === true) {
            res = convertArrayToRecursive(res.data)
        } else {
            res = []
        }

        let menuCategories = [{
            name: "Categories",
            children: []
        }]
        menuCategories[0].children = res;
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

    const renderMenuItem = () => {

    }
    return (
        <div className="items-center justify-between flex py-3">
            <ul className="flex flex-col p-4 mt-4 md:flex-row md:space-x-8 md:mt-0 md:text-sm md:border-0">
                <NavLink to={'/'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Home
                </NavLink>
                <NavLink to={'/categories'}>
                    <ul className="menus"> {
                        listCategory.map((menu, index) => {
                            const depthLevel = 0;
                            return <MenuItem items={menu}
                                             key={index}
                                             depthLevel={depthLevel}/>;
                        })
                    } </ul>
                </NavLink>

                <NavLink to={'/products'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Products
                </NavLink>
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