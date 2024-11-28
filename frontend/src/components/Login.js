import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../services/api";

const Login = ({ setAuth }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      formData.append("username", username);
      formData.append("password", password);

      const response = await api.post(
        "/api/Auth/login",
        {
          username: username,
          password: password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log(response.data);
      localStorage.setItem("token", response.data);
      setAuth(true);
      navigate("/");
    } catch (error) {
      setError(`Invalid username. Please try again. ${error}`);
      console.error("Login error: ", error);
    }
  };

  return (
    <form
      onSubmit={handleLogin}
      className="h-screen flex items-center justify-center flex-col"
    >
      <div>
        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="Username here"
          className="placeholder:text-center p-0.5 mb-6 px-1.5"
        />
      </div>
      <div>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Password here"
          className="placeholder:text-center p-0.5 mb-6 px-1.5"
        />
      </div>
      {error && <div style={{ color: "red" }}>{error}</div>}
      <button type="submit">Log in</button>
      <Link className="font-thin mt-4" to="/register">
        Register
      </Link>
    </form>
  );
};

export default Login;
