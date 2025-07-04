import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../contexts/auth/useAuth";
import {BACKEND_BASE_URL} from "../utils/constants";
import axiosBase from "../api/axiosBase";
import {toast} from "react-toastify";

function ShortenUrlItem({url, onDelete}) {
    const navigate = useNavigate();
    const {user, isAuthenticated} = useAuth();
    const [canDelete, setCanDelete] = useState(false);

    useEffect(() => {
        if (!isAuthenticated || !user) {
            setCanDelete(false);
            return;
        }

        const isOwner = user.id === url.creatorId?.value;
        const isAdmin = user.roles?.includes("Admin");

        setCanDelete(isOwner || isAdmin);
    }, [isAuthenticated, url.creatorId?.value, user]);

    const handleDelete = async () => {
        if (!window.confirm("Are you sure you want to delete this URL?")) return;

        try {
            await axiosBase.delete(`api/shortenedurls/${url.id.value}`);
            toast.success("URL deleted.");
            if (onDelete) onDelete(url.id.value);
        } catch (err) {
            console.error(err);
            toast.error(err.response?.data?.message || err.response.data.detail || "Failed to delete URL.");
        }
    };

    return (
        <li className="bg-white shadow-sm rounded-xl p-4 mb-4 border border-gray-200">
            <p className="mb-2 text-sm">
                <span className="font-semibold">Original URL:</span>
                <a
                    href={url.originalUrl}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-blue-600 hover:underline break-all"
                >
                    {url.originalUrl}
                </a>
            </p>

            <p className="mb-4 text-sm">
                <span className="font-semibold">Short URL:</span>
                <a
                    href={`${BACKEND_BASE_URL}/${url.shortCode}`}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-blue-600 hover:underline break-all"
                >
                    {`${BACKEND_BASE_URL}/${url.shortCode}`}
                </a>
            </p>

            {isAuthenticated && (
                <div className="flex gap-2">
                    <button
                        onClick={() => navigate(`/shortenedurls/${url.id.value}`)}
                        className="px-3 py-1 text-sm bg-blue-500 hover:bg-blue-600 text-white rounded-md"
                    >
                        See Details
                    </button>
                    {canDelete && (
                        <button
                            onClick={handleDelete}
                            className="px-3 py-1 text-sm bg-red-500 hover:bg-red-600 text-white rounded-md"
                        >
                            Delete
                        </button>
                    )}
                </div>
            )}
        </li>
    );
}

export default ShortenUrlItem;
