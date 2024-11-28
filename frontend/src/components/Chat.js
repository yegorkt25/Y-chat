import { useEffect, useRef, useState } from "react";
import api from "../services/api";
import Pusher from "pusher-js";

function Chat({ styles, selected, setError, setSelected }) {
  const messagesEndRef = useRef(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  const handleSend = async () => {
    try {
      let res = await api.post(
        `/api/Messages/${selected.id}`,
        {
          content: message,
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );

      let updated = { ...selected };
      updated.messages.push(res.data);
      setSelected(updated);
      setMessage("");
    } catch (ex) {
      setError(`Error sending message ${ex}`);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      e.preventDefault();
      setMessage("");
      handleSend();
    }
  };

  const [message, setMessage] = useState("");
  // const [messages, setMessages] = useState([]);

  // useEffect(() => {
  // var pusher = new Pusher("04f8c12e154fdb5d68fd", {
  //   cluster: "eu",
  // });

  // var channel = pusher.subscribe("chat");
  // channel.bind("message", function (data) {
  //   setMessages([...messages, data]);
  // });
  // }, []);

  useEffect(() => {
    if (selected) {  // Only scroll if a chat is selected
      scrollToBottom();
    }
  }, [selected, selected?.messages]);

  return (
    <>
      <div className={styles}>
        <div className="flex flex-col justify-center p-6 overflow-y-auto overflow-x-hidden scrollbar scrollbar-thumb-slate-300">
          <div className="text-center italic font-bold">
            Users:{" "}
            {selected && selected.users.map((user) => user.username).join(", ")}
          </div>
          {selected &&
            selected.messages.map((i, index) => {
              return (
                <>
                  <div key={index} className="p-2 m-2 text-sm inline w-fit">
                    <b>{i.user.username}</b>
                    <br />

                    <i>{i.content}</i>
                  </div>
                </>
              );
            })}
          <div ref={messagesEndRef} />
        </div>
        <div className="flex flex-row justify-center gap-9 mt-auto mb-5 ml-2">
          <input
            className="w-5/6 h-max p-2 rounded-md"
            placeholder="Type in a message"
            type="text"
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            onKeyPress={handleKeyPress}
          />
          <button className="text-lg mr-5" onClick={handleSend}>
            Send
          </button>
        </div>
      </div>
    </>
  );
}

export default Chat;
