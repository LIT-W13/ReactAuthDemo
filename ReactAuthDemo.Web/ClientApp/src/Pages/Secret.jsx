import React, {useEffect, useState} from 'react';
import axios from 'axios';

const Secret = () => {
    const [number, setNumber] = useState();

    useEffect(() => {
        const getSecretData = async () => {
            const { data } = await axios.get('/api/secret/getdata');
            setNumber(data.number);
        }

        getSecretData();
    }, []);

    return (
        <div>
            <h1>Welcome to the secret page!</h1>
            <h2>The secret number is {number}</h2>
        </div>
    )

}

export default Secret;