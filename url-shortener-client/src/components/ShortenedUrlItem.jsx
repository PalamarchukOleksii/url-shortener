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
        <li>
            <p>
                <strong>Original URL:</strong>{" "}
                <a href={url.originalUrl} target="_blank" rel="noopener noreferrer">
                    {url.originalUrl}
                </a>
            </p>

            <p>
                <strong>Short URL:</strong>{" "}
                <a
                    href={`${BACKEND_BASE_URL}/${url.shortCode}`}
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    {`${BACKEND_BASE_URL}/${url.shortCode}`}
                </a>
            </p>

            {isAuthenticated && (
                <>
                    <button onClick={() => navigate(`/shortenedurls/${url.id.value}`)}>
                        See Details
                    </button>
                    {canDelete && (
                        <button style={{marginLeft: "0.5rem"}} onClick={handleDelete}>
                            Delete
                        </button>
                    )}
                </>
            )}
        </li>
    );
}

export default ShortenUrlItem;
