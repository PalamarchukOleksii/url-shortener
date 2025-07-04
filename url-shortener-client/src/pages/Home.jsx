import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase";
import {toast} from "react-toastify";
import ShortenUrlItem from "../components/ShortenedUrlItem.jsx";

function Home() {
    const [urls, setUrls] = useState([]);
    const [page, setPage] = useState(1);
    const count = 10;

    useEffect(() => {
        axiosBase
            .get("api/shortenedurls", {
                params: {page, count},
            })
            .then((response) => {
                setUrls(response.data);
            })
            .catch((err) => {
                console.error(err);
                toast.error(err.response.data.message);
            });
    }, [page]);

    const handlePrev = () => {
        if (page > 1) setPage((p) => p - 1);
    };

    const handleNext = () => {
        if (urls.length === count) setPage((p) => p + 1);
    };

    if (!urls) return <div>Loading...</div>;

    return (
        <div>
            <h1>Home</h1>
            <ul>
                {urls.map((url) => (
                    <ShortenUrlItem
                        key={url.id.value}
                        url={url}
                        onDelete={(deletedId) =>
                            setUrls((prev) => prev.filter((u) => u.id.value !== deletedId))
                        }
                    />
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
