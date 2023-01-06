import React from 'react';

const Search = () => {
    return (
        <div className={`w-3/5`}>
            <form className={`my-2 focus:border-none focus:outline-none w-full `}>
                <label htmlFor="default-search"
                       className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                <div className="relative">
                    <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg aria-hidden="true" className="w-5 h-5 text-gray-500 dark:text-gray-400" fill="none"
                             stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                                  d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path>
                        </svg>
                    </div>
                    <input type="search" id="default-search"
                           className="border-none focus:border-none focus:outline-none outline-none block w-full p-4 pl-10 text-sm text-gray-900 rounded-full bg-gray-50 dark:bg-gray-700 dark:text-white"
                           placeholder="Search product..." required/>
                    <button type="submit"
                            className="text-white absolute right-2 bottom-2 bg-dangerColor-default_3 focus:outline-none font-medium rounded-full text-sm px-4 py-2 hover:bg-dangerColor-hover_2">Search
                    </button>
                </div>
            </form>
        </div>);
};

export default Search;