import axios from 'axios'

const api = axios.create({
    baseURL: 'http://3.249.239.233',
});

export default api;