import React, {useState} from "react";
import axiosBase from "../api/axiosBase";
import {toast} from "react-toastify";

const ShortenUrlForm = ({onCreated}) => {
    const [originalUrl, setOriginalUrl] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!originalUrl.trim()) {
            toast.warn("Please enter a URL.");
            return;
        }

        setLoading(true);
        try {
            const response = await axiosBase.post("api/shortenedurls/shorten", {
                originalUrl,
            });

            toast.success("URL shortened successfully!");
            onCreated?.(response.data);
            setOriginalUrl("");
        } catch (err) {
            console.error(err);
            toast.error(
                err.response?.data?.message ||
                err.response?.data?.detail ||
                "Failed to shorten URL."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <form
            onSubmit={handleSubmit}
            className="flex max-w-4xl mx-auto gap-2 mb-4"
        >
            <input
                type="url"
                placeholder="Enter URL to shorten"
                value={originalUrl}
                onChange={(e) => setOriginalUrl(e.target.value)}
                required
                className="flex-grow px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:ring-blue-200"
            />
            <button
                type="submit"
                disabled={loading}
                className={`w-32 rounded-md text-white transition ${
                    loading ? "bg-gray-400 cursor-not-allowed" : "bg-blue-500 hover:bg-blue-600"
                }`}
            >
                {loading ? "Shortening..." : "Shorten URL"}
            </button>
        </form>

    );
};

export default ShortenUrlForm;
