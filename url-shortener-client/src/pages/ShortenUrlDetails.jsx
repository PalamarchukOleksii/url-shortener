import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import axiosBase from "../api/axiosBase";
import {BACKEND_BASE_URL} from "../utils/constants.js";
import {toast} from "react-toastify";
import {useAuth} from "../contexts/auth/useAuth.js";

function ShortenUrlDetails() {
    const {id} = useParams();
    const [urlDetails, setUrlDetails] = useState(null);
    const navigate = useNavigate();
    const {user, isAuthenticated} = useAuth();
    const [canDelete, setCanDelete] = useState(false);

    useEffect(() => {
        if (!id) return;

        axiosBase
            .get(`api/shortenedurls/${id}`)
            .then((res) => setUrlDetails(res.data))
            .catch((err) => toast.error(err.response?.data?.message || err.response.data.detail || "Failed to load details about shortened Url."));
    }, [id]);

    useEffect(() => {
        if (!isAuthenticated || !user || !urlDetails) {
            setCanDelete(false);
            return;
        }

        const isOwner = user.id === urlDetails.creatorId?.value;
        const isAdmin = user.roles?.includes("Admin");

        setCanDelete(isOwner || isAdmin);
    }, [isAuthenticated, user, urlDetails]);

    const handleDelete = async () => {
        if (!window.confirm("Are you sure you want to delete this URL?")) return;

        try {
            await axiosBase.delete(`api/shortenedurls/${urlDetails.id.value}`);
            toast.success("URL deleted.");
            navigate(-1);
        } catch (err) {
            console.error(err);
            toast.error(
                err.response?.data?.message ||
                err.response?.data?.detail ||
                "Failed to delete URL."
            );
        }
    };

    if (!urlDetails) return <div>Loading...</div>;

    return (
        <div>
            <h1>Shortened URL Details</h1>
            <p>
                <strong>Original URL:</strong>{" "}
                <a
                    href={urlDetails.originalUrl}
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    {urlDetails.originalUrl}
                </a>
            </p>
            <p>
                <strong>Short URL:</strong>{" "}
                <a
                    href={`${BACKEND_BASE_URL}/${urlDetails.shortCode}`}
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    {`${BACKEND_BASE_URL}/${urlDetails.shortCode}`}
                </a>
            </p>
            <p>
                <strong>Short Code:</strong> {urlDetails.shortCode}
            </p>
            <p>
                <strong>Creator Login:</strong> {urlDetails.creatorLogin}
            </p>
            <p>
                <strong>Creator ID:</strong> {urlDetails.creatorId.value}
            </p>
            <p>
                <strong>Created At:</strong>{" "}
                {new Date(urlDetails.createdAt).toLocaleString()}
            </p>
            <p>
                <strong>Redirect Count:</strong> {urlDetails.redirectCount}
            </p>

            {canDelete && (
                <button style={{marginLeft: "0.5rem"}} onClick={handleDelete}>
                    Delete
                </button>
            )}
        </div>
    );
}

export default ShortenUrlDetails;
