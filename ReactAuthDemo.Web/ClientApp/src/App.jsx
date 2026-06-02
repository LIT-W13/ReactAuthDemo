import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Signup from './Pages/Signup';
import Login from './Pages/Login';
import AuthProdivder from './AuthContext';
import Secret from './Pages/Secret';
import PrivateRoute from './components/PrivateRoute';
import Logout from './Pages/Logout';

const App = () => {
    return (
        <AuthProdivder>
            <Layout>
                <Routes>
                    <Route path='/' element={<Home />} />
                    <Route path='/signup' element={<Signup />} />
                    <Route path='/login' element={<Login />} />
                    <Route path='/secret' element={
                        <PrivateRoute>
                            <Secret />
                        </PrivateRoute>
                    } />
                    <Route path='/logout' element={<Logout />} />
                </Routes>
            </Layout>
        </AuthProdivder>
    );
}

export default App;