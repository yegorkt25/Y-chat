import React, { useState } from "react";

const AddChatPopup = ({ onClose, onAddChat }) => {
  const [chatName, setChatName] = useState("");
  const [users, setUsers] = useState([""]);

  const handleUserChange = (index, value) => {
    const updatedUsers = [...users];
    updatedUsers[index] = value;
    setUsers(updatedUsers);
  };

  const handleAddField = () => {
    setUsers([...users, ""]);
  };

  const handleDeleteUser = (index) => {
    const updatedUsers = users.filter((_, i) => i !== index);
    setUsers(updatedUsers);
  };

  const handleSubmit = () => {
    const filteredUsers = users.filter((user) => user.trim() !== "");
    if (!chatName.trim()) {
      return;
    }
    onAddChat({ name: chatName.trim(), users: filteredUsers });
    onClose();
  };

  return (
    <div className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 bg-white border border-gray-300 p-5 rounded-lg z-[1000]">
      <h3>Create or Rename Chat</h3>
      <div className="mb-4">
        <input
          type="text"
          value={chatName}
          onChange={(e) => setChatName(e.target.value)}
          placeholder="Enter chat name"
          className="w-full p-2.5 border border-gray-300 rounded"
        />
      </div>
      {users.map((user, index) => (
        <div
          key={index}
          className="mb-2.5 flex items-center"
        >
          <input
            type="text"
            value={user}
            onChange={(e) => handleUserChange(index, e.target.value)}
            placeholder="Enter user name"
            className="w-[200px] p-1.5 mr-2.5 border border-gray-300 rounded"
          />
          <button
            onClick={() => handleDeleteUser(index)}
            className="px-2.5 py-1.5 bg-red-600 text-white rounded cursor-pointer"
          >
            Delete
          </button>
        </div>
      ))}
      <button
        onClick={handleAddField}
        className="mb-2.5 px-2.5 py-1.5 bg-blue-600 text-white rounded cursor-pointer"
      >
        Add Another User
      </button>
      <br />
      <button
        onClick={handleSubmit}
        className="mr-2.5 px-2.5 py-1.5 bg-green-600 text-white rounded cursor-pointer"
      >
        Save Chat
      </button>
      <button
        onClick={onClose}
        className="px-2.5 py-1.5 bg-gray-600 text-white rounded cursor-pointer"
      >
        Cancel
      </button>
    </div>
  );
};

export default AddChatPopup;
