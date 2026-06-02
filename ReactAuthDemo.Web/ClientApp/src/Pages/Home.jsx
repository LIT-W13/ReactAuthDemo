import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';
import { useAuth } from '../AuthContext';


const Home = () => {

    const { user } = useAuth();

    return (
        <div className="app-container">
            <div className="d-flex flex-column justify-content-center align-items-center">
                <h1>Welcome to React</h1>
                {!!user && <h1>Hello {user.firstName} {user.lastName}</h1>}
            </div>
        </div>
    );
};

export default Home;