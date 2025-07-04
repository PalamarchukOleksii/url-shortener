import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase";
import {BACKEND_BASE_URL} from "../utils/constants.js";

function Home() {
    const [urls, setUrls] = useState([]);
    const [error, setError] = useState(null);
    const [page, setPage] = useState(1);
    const count = 10; // items per page

    useEffect(() => {
        axiosBase
            .get("api/shortenedurls", {
                params: {page, count},
            })
            .then((response) => {
                setUrls(response.data);
                setError(null);
            })
            .catch((err) => {
                console.error(err);
                setError("Failed to load shortened URLs.");
            });
    }, [page]);

    const handlePrev = () => {
        if (page > 1) setPage((p) => p - 1);
    };

    const handleNext = () => {
        if (urls.length === count) setPage((p) => p + 1);
    };

    if (error) return <div>Error: {error}</div>;
    if (!urls) return <div>Loading...</div>;

    return (
        <div>
            <h1>Home</h1>
            <ul>
                {urls.map((url) => (
                    <li key={url.id}>
                        <a href={url.originalUrl} target="_blank" rel="noopener noreferrer">
                            {url.originalUrl}
                        </a>{" "}
                        —{" "}
                        <a
                            href={`${BACKEND_BASE_URL}/${url.shortCode}`}
                            target="_blank"
                            rel="noopener noreferrer"
                        >
                            {`${BACKEND_BASE_URL}/${url.shortCode}`}
                        </a>
                    </li>
                ))}
            </ul>

            <div style={{marginTop: "1rem"}}>
                <button onClick={handlePrev} disabled={page === 1}>
                    Previous
                </button>
                <span style={{margin: "0 1rem"}}>Page: {page}</span>
                <button onClick={handleNext} disabled={urls.length < count}>
                    Next
                </button>
            </div>
        </div>
    );
}

export default Home;
