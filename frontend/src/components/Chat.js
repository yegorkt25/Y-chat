import { useState } from "react";

function Chat({ styles }) {
    const handleSend = () => {
        //TODO: write code to send message to server
    }

    const handleKeyPress = (e) => {
        if (e.key === "Enter") {
            e.preventDefault();
            handleSend();
        }
    };

    const [message, setMessage] = useState('');

    return <>
        <div className={styles}>
            <div className="flex flex-row justify-center p-5">
                <h1 className="text-2xl">Chat ... TODO</h1>
            </div>
            <div className="flex flex-row justify-center gap-9 mt-auto mb-5">
                <input className="resize-none w-5/6 h-max p-2 rounded-md" placeholder="Type in a message" type="text" value={message} onChange={e => setMessage(e.target.value)} onKeyPress={handleKeyPress}/>
                <button className="text-lg" onClick={handleSend}>Send</button>
            </div>
        </div>
    </>;
}

export default Chat;