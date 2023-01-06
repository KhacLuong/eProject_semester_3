import React, {useEffect, useState} from 'react';
import {NavLink, useNavigate} from "react-router-dom";
import MenuItem from "./MenuItem";
import {getListCategory} from "../../../services/apiService";
import './nav.scss'
import {BiChevronDown} from "react-icons/bi";

const MenuNavigation = () => {
    const defaultClass = `flex items-center justify-center py-4 mx-4 text-blackColor hover:text-dangerColor-default_2 md:p-0 ease-in duration-200 text-base font-medium`
    const [listCategory, setListCategory] = useState([]);
    const [showMenuPage, setShowMenuPage] = useState(false)
    const navigate = useNavigate();
    const listMenuPage = [
        {name: 'All Author', path: '/all-author'},
        {name: 'About Us', path: '/about-us'},
        {name: 'FAQs', path: '/faq'},
    ]
    useEffect(() => {
        fetchListCategories()
    }, [])

    const fetchListCategories = async () => {
        let res = await getListCategory();
        let menuCategories = [{
            name: "Categories",
            children: [
                {
                    id: 0,
                    code: 'al',
                    name: 'All categories',
                    parentId: 0,
                    slug: 'all-categories',
                    status: 'Active',
                    children: []
                }
            ]
        }]
        if (res.status === true) {
            menuCategories[0].children = menuCategories[0].children.concat(convertArrayToRecursive(res.data))
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
    const handleShowMenuPage = () => {
        setShowMenuPage(!showMenuPage)
    }
    return (
        <div className="items-center justify-between flex ml-6">
            <ul className="flex flex-col py-4 px-6 mt-4 md:flex-row md:mt-0 md:text-sm md:border-0">
                <NavLink to={'/'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Home
                </NavLink>
                <a className={`flex items-center py-2 mx-4 justify-center hover:text-dangerColor-default_2`}>
                    <ul className="menus">
                        {
                            listCategory.map((menu, index) => {
                                const depthLevel = 0;
                                return <MenuItem items={menu}
                                                 key={index}
                                                 depthLevel={depthLevel}/>;
                            })
                        }
                    </ul>
                </a>
                <NavLink to={'/products'}
                         className={({isActive}) => isActive ? 'active' : defaultClass}>
                    Products
                </NavLink>
                <a className={`relative flex items-center py-2 mx-4 justify-center cursor-pointer text-blackColor hover:text-dangerColor-default_2 md:p-0 ease-in duration-200 text-base font-medium`} onClick={handleShowMenuPage}>
                    <div className={`flex`}>
                        Pages<BiChevronDown className={`ml-1 text-xl`}/>
                    </div>
                    <ul className={`menu_page  ${showMenuPage?'block':'hidden'} absolute`}>
                        {
                            listMenuPage.map((item, index) => {
                                return <li key={index}>
                                    <div onClick={() => navigate(item.path)}>
                                        {item.name}
                                    </div>
                                </li>
                            })
                        }
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