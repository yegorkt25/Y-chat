import React, { useState, useEffect } from "react";
import Chat from "./Chat";
import ChatList from "./ChatList";
import api from "../services/api";
import { jwtDecode } from "jwt-decode";

const Dashboard = () => {
  const handleLogout = () => {
    localStorage.removeItem("token");
    window.location.href = "/login";
  };

  const [username, setUsername] = useState("");
  const [selectedChat, setSelectedChat] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    let claims = jwtDecode(localStorage.getItem("token"));
    setUsername(claims.sub);
  }, []);

  return (
    <div className="h-screen grid grid-cols-4 grid-rows-5">
      <div className="col-span-4 text-center">
        <h1 className="text-3xl font-bold mt-14">Y-Chat</h1>
        <p>{username}</p>
        {error && <div style={{ color: "red" }}>{error}</div>}

      </div>

      <ChatList
        styles={
          "row-span-3 bg-slate-300 rounded flex flex-col w-5/6 m-auto h-full"
        }
        setSelected={setSelectedChat}
        setError={setError}
      />
      <Chat
        styles={"row-span-3 col-span-2 bg-slate-300 rounded flex flex-col"}
        selected={selectedChat}
        setError={setError}
        setSelected={setSelectedChat}
      />

      <div className="row-span-3">

      </div>
      <div className="col-span-4 p-4 text-end">
        <button
          onClick={handleLogout}
          className="mr-10 mt-20 text-lg hover:opacity-80"
        >
          Logout
        </button>
      </div>

    </div>
  );
};

export default Dashboard;
