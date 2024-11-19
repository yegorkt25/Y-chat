import React, { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { AuthContext } from '../App';

const PrivateRoute = () =>{
    const { isAuthenticated } = useContext(AuthContext);
    
  return isAuthenticated ? 
    <Outlet />
  :
    <Navigate to='/login' replace />
}

export default PrivateRoute;