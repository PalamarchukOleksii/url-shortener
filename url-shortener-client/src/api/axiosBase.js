import axios from "axios";
import {BACKEND_BASE_URL} from "../utils/constants.js";

const axiosBase = axios.create({
    baseURL: import.meta.env.VITE_BACKEND_BASE_URL || BACKEND_BASE_URL,
    withCredentials: true,
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

export default axiosBase;