import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../services/api";

function Register({ setAuth }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [rptPassword, setRptPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();
    if (password === rptPassword) {
      try {
        const formData = new FormData();
        formData.append("username", username);
        formData.append("password", password);

        if (username !== "" && password !== "") {
          const response = await api.post(
            "/api/Auth/register/",
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

          if (response.status === 200) {
            setSuccess("User created successfully.");
          }
        } else {
          setError("Please fill in all fields.");
        }
      } catch (error) {
        setError("Invalid username or password. Please try again.");
        console.error("Login error: ", error);
      }
    } else {
      setError("Passwords do not match.");
    }
  };

  return (
    <form
      onSubmit={handleRegister}
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
      <div>
        <input
          type="password"
          value={rptPassword}
          onChange={(e) => setRptPassword(e.target.value)}
          placeholder="Repeat password here"
          className="placeholder:text-center p-0.5 mb-6 px-1.5"
        />
      </div>
      {error && <div style={{ color: "red" }}>{error}</div>}
      {success && <div>{success}</div>}
      <button type="submit">Register</button>
      <Link className="font-thin mt-4" to="/login">
        Back
      </Link>
    </form>
  );
}

export default Register;
