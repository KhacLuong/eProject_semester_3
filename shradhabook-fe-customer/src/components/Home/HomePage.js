import React from 'react';

const HomePage = () => {
    return (
        <div className={`px-36 py-10`}>
            This is home page
            <div>
                <input type="number"
                       defaultValue="0"/>
            </div>
        </div>
    );
};

export default HomePage;