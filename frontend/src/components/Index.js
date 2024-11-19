import React, { useState, useEffect } from 'react';
import api from '../services/api';
import Chat from './Chat';
import UsersList from './UsersList';
import ChatList from './ChatList';

const Dashboard = () => {
    const handleLogout = () => {
        localStorage.removeItem('token');
        window.location.href = '/login';
    };

    // useEffect(()=>{
    //     const fetchCountries = async() =>{
    //         try{
    //             const response = await api.get('/api/common/country/');
    //         } catch(error) {
    //             console.error('Error fetching countries: ', error);
    //         }
    //     };
    //     fetchCountries();
    // },[]);

    const [username, setUsername] = useState('');

    useEffect(() => {
        let username = localStorage.getItem('User');
        setUsername(username);
    }, []);

    return (
        <div className='h-screen grid grid-cols-4 grid-rows-5'>
            <div className='col-span-4 text-center'>
                <h1 className='text-3xl font-bold mt-14'>Web Chat</h1>
                <p>{ username }</p>
            </div>
            <div className="row-span-3"><ChatList /></div>
            <Chat styles={"row-span-3 col-span-2 bg-slate-300 rounded flex flex-col"}/>
            <div className="row-span-3"></div>
            <div className="col-span-4 p-4 text-end">
                <button onClick={handleLogout} className='mr-10 mt-20 text-lg hover:opacity-80'>Logout</button>
            </div>
            
        </div>
    );
};

export default Dashboard;