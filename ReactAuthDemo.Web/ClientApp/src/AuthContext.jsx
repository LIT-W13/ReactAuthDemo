import { useState, useEffect, useContext, createContext } from 'react';
import axios from 'axios';

const AuthContext = createContext();

const Loader = () => {
    return (
    <div className="d-flex vh-100 justify-content-center align-items-center bg-light">
      <div className="spinner-border text-primary" style={{ width: "5rem", height: "5rem" }} role="status">
        <span className="visually-hidden">Loading...</span>
      </div>
    </div>
  );
}

const AuthProdivder = ({ children }) => {
    const [user, setUser] = useState(null);
    const [isLoadingUser, setIsLoadingUser] = useState(true);

    useEffect(() => {
        const loadUser = async () => {
            setIsLoadingUser(true);
            const { data } = await axios.get('/api/account/getcurrentuser');
            setUser(data);
            setIsLoadingUser(false);
        }
        loadUser();
    }, []);


    if(isLoadingUser){
        return <Loader />
    }


    return (
        <AuthContext.Provider value={{user, setUser}}>
            {children}
        </AuthContext.Provider>
    )

}


const useAuth = () => useContext(AuthContext);

export { useAuth };

export default AuthProdivder;