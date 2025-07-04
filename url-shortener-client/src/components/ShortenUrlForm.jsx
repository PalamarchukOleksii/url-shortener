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
            onCreated?.(response.data); // notify parent
            setOriginalUrl(""); // clear input
        } catch (err) {
            console.error(err);
            toast.error(err.response?.data?.message || "Failed to shorten URL.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{marginBottom: "1rem"}}>
            <input
                type="url"
                placeholder="Enter URL to shorten"
                value={originalUrl}
                onChange={(e) => setOriginalUrl(e.target.value)}
                required
                style={{width: "300px", marginRight: "0.5rem"}}
            />
            <button type="submit" disabled={loading}>
                {loading ? "Shortening..." : "Shorten URL"}
            </button>
        </form>
    );
};

export default ShortenUrlForm;
