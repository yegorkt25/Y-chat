import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const Login = ( {setAuth})=> {

    const [username, setUsername] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async(e)=>{
        e.preventDefault();

       try {
            // const formData = new FormData();
            // formData.append('username', username);

            // const response = await api.post('/api/login/',{
            //     username : username,
            // },{
            //     headers: {
            //         'Content-Type': 'application/json'
            //     }
            // });
           // localStorage.setItem('token', response.data.access_token);
           localStorage.setItem('User', username+"#1234");
            setAuth(true);
            navigate('/');
        } catch (error) {
            setError('Invalid username. Please try again.')
            console.error('Login error: ', error);
        }
    };

    return (
        <form onSubmit={handleLogin} className='h-screen flex items-center justify-center flex-col'>
            <div>
                <input type='text' value={username} onChange={(e)=> setUsername(e.target.value)} placeholder='Username here' className='placeholder:text-center p-0.5 mb-6 px-1.5'/>
            </div>
            {error && <div style={{color:'red'}}>{error}</div>}
            <button type='submit'>Log in</button>
        </form>
    );
};



export default Login;