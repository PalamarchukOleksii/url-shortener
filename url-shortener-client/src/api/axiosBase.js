import axios from "axios";
import {BACKEND_BASE_URL} from "../utils/constants.js";

const axiosBase = axios.create({
    baseURL: BACKEND_BASE_URL,
    withCredentials: true,
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

/*axiosBase.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response) {
            const {status, data} = error.response;

            if (status === 400) {
                if (data.error && Array.isArray(data.error)) {
                    data.error.forEach(({code, message}) => {
                        toast.error(`${code}: ${message}`);
                    });
                } else if (data.detail) {
                    toast.error(`Bad Request: ${data.detail}`);
                } else {
                    toast.error("Bad Request");
                }
            } else {
                const msg =
                    data.title || data.detail || error.response.statusText || "An error occurred";
                toast.error(`${status} - ${msg}`);
            }
        } else if (error.request) {
            toast.error("No response from server. Please check your network.");
        } else {
            toast.error(`Request error: ${error.message}`);
        }

        return Promise.reject(error);
    }
);*/

export default axiosBase;