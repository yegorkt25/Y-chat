import React, { useState, createContext } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import PrivateRoute from "./components/PrivateRoute";
import Index from "./components/Index";
import Register from "./components/Register";

const AuthContext = createContext(null);

function App() {
  const [isAuthenticated, setAuth] = useState(
    localStorage.getItem("token") || false
  );

  return (
    <AuthContext.Provider value={{ isAuthenticated, setAuth }}>
      <Router>
        <Routes>
          <Route path="/login" element={<Login setAuth={setAuth} />} />
          <Route path="/register" element={<Register setAuth={setAuth} />} />
          <Route element={<PrivateRoute />}>
            <Route path="/" element={<Index />} />
          </Route>
        </Routes>
      </Router>
    </AuthContext.Provider>
  );
}

// export default App;

export { App, AuthContext };
