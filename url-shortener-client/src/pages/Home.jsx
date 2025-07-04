import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase";
import {toast} from "react-toastify";
import ShortenUrlItem from "../components/ShortenedUrlItem.jsx";
import ShortenUrlForm from "../components/ShortenUrlForm.jsx";
import {useAuth} from "../contexts/auth/useAuth.js";

function Home() {
    const {isAuthenticated} = useAuth();
    const [urls, setUrls] = useState([]);
    const [page, setPage] = useState(1);
    const [count, setCount] = useState(10);
    const [hasNextPage, setHasNextPage] = useState(false);

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
                toast.error(
                    err.response?.data?.message ||
                    err.response?.data?.detail ||
                    "Failed to load shortened URLs."
                );
            });

        axiosBase
            .get("api/shortenedurls", {
                params: {page: page + 1, count},
            })
            .then((response) => {
                setHasNextPage(response.data.length > 0);
            })
            .catch((err) => {
                console.error("Failed to check next page", err);
                setHasNextPage(false);
            });
    }, [page, count]);

    const handlePrev = () => {
        if (page > 1) setPage((p) => p - 1);
    };

    const handleNext = () => {
        if (urls.length === count) setPage((p) => p + 1);
    };

    const handleAddUrl = (newUrl) => {
        if (page === 1) {
            setUrls((prev) => [newUrl, ...prev.slice(0, count - 1)]);
        }
    };

    const handleCountChange = (e) => {
        setCount(Number(e.target.value));
        setPage(1);
    };

    if (!urls) {
        return (
            <div className="p-6">
                <h1 className="text-2xl font-bold mb-4">Home</h1>
                <p className="text-gray-500">Loading...</p>
            </div>
        );
    }

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-3xl font-bold mb-6">Home</h1>

            {isAuthenticated && (
                <div className="mb-6">
                    <ShortenUrlForm onCreated={handleAddUrl}/>
                </div>
            )}

            <div className="flex items-center mb-4 gap-2">
                <label htmlFor="itemsPerPage" className="text-sm font-medium">
                    Items per page:
                </label>
                <select
                    id="itemsPerPage"
                    value={count}
                    onChange={handleCountChange}
                    className="px-2 py-1 border border-gray-300 rounded-md text-sm"
                >
                    <option value={5}>5</option>
                    <option value={10}>10</option>
                    <option value={20}>20</option>
                    <option value={50}>50</option>
                </select>
            </div>

            <ul className="space-y-4">
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

            <div className="mt-6 flex items-center justify-center gap-4">
                <button
                    onClick={handlePrev}
                    disabled={page === 1}
                    className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
                >
                    Previous
                </button>
                <span className="text-sm text-gray-600">Page: {page}</span>
                <button
                    onClick={handleNext}
                    disabled={!hasNextPage}
                    className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
                >
                    Next
                </button>
            </div>
        </div>
    );
}

export default Home;
