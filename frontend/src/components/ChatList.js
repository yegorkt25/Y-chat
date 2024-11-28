import React, { useState, useEffect } from "react";
import AddChatPopup from "./AddChatPopup";
import api from "../services/api";

function ChatList({ styles, setSelected, setError }) {
  const [showPopup, setShowPopup] = useState(false);
  const [chats, setChats] = useState([]);
  const [selectedChat, setSelectedChat] = useState(null);

  const handleAddChat = async (newChat) => {
    try {
      const res = await api.post('/api/Chat',
        {
          name: newChat.name,
          usernames: newChat.users
        },
        {
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem('token')}`
          },
        });
  
      console.log(res.data);
      setChats([...chats, res.data]);
    } catch (ex) {
      setError(`One of users doesn't exist ${ex}`);
    }
    
  };

  useEffect(() => {
    const fetchChats = async () => {
      const res = await api.get('/api/Chat', {
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem('token')}`
        },
      });

      console.log();
      setChats(res.data);
    };

    try {
      fetchChats();
    } catch (ex) {
      console.log(ex)
      setError(`One of users doesn't exist ${ex}`);
    }
  }, []);

  const handleSelectChat = (chatId) => {
    setSelectedChat(chatId);
    setSelected(chatId);
  };

  return (
    <div className={styles}>
      <div className="overflow-y-auto overflow-x-hidden scrollbar scrollbar-thumb-slate-300">
        {chats.map((chat) => (
          <button
            key={chat.id}
            onClick={() => handleSelectChat(chat)}
            className="block w-5/6 text-left p-2 m-5 mx-auto bg-white rounded-lg hover:opacity-80 cursor-pointer"
            style={{
              backgroundColor: selectedChat && selectedChat.id === chat.id ? "#007BFF" : "#f8f9fa",
            }}
          >
            <strong>{chat.name}</strong>
          </button>
        ))}
      </div>

      <button
        className="justify-center m-auto mb-5 bg-white w-5/6 rounded-lg hover:opacity-80"
        onClick={() => setShowPopup(true)}
      >
        Add Chat
      </button>
      {showPopup && (
        <>
          <AddChatPopup
            onClose={() => setShowPopup(false)}
            onAddChat={handleAddChat}
          />
          <div
            style={{
              position: "fixed",
              top: 0,
              left: 0,
              width: "100%",
              height: "100%",
              backgroundColor: "rgba(0, 0, 0, 0.5)",
              zIndex: 999,
            }}
            onClick={() => setShowPopup(false)}
          />

        </>
      )}
    </div>
  );
}

export default ChatList;
