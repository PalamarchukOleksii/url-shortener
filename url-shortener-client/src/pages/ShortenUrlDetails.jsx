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
            .catch((err) =>
                toast.error(
                    err.response?.data?.message ||
                    err.response?.data?.detail ||
                    "Failed to load details about shortened URL."
                )
            );
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

    if (!urlDetails) return <div className="p-6 text-gray-600">Loading...</div>;

    return (
        <div className="p-6 max-w-3xl mx-auto">
            <div className="mb-4">
                <button
                    onClick={() => navigate(-1)}
                    className="px-4 py-2 bg-gray-200 text-gray-800 rounded hover:bg-gray-300 transition"
                >
                    ← Go Back
                </button>
            </div>

            <h1 className="text-3xl font-bold mb-6">Shortened URL Details</h1>

            <div className="space-y-4 bg-white shadow-md rounded-xl p-6 border">
                <p>
                    <span className="font-semibold">Original URL:</span>{" "}
                    <a
                        href={urlDetails.originalUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                        className="text-blue-600 underline break-all"
                    >
                        {urlDetails.originalUrl}
                    </a>
                </p>

                <p>
                    <span className="font-semibold">Short URL:</span>{" "}
                    <a
                        href={`${BACKEND_BASE_URL}/${urlDetails.shortCode}`}
                        target="_blank"
                        rel="noopener noreferrer"
                        className="text-blue-600 underline break-all"
                    >
                        {`${BACKEND_BASE_URL}/${urlDetails.shortCode}`}
                    </a>
                </p>

                <p>
                    <span className="font-semibold">Short Code:</span>{" "}
                    {urlDetails.shortCode}
                </p>

                <p>
                    <span className="font-semibold">Creator Login:</span>{" "}
                    {urlDetails.creatorLogin}
                </p>

                <p>
                    <span className="font-semibold">Creator ID:</span>{" "}
                    {urlDetails.creatorId?.value}
                </p>

                <p>
                    <span className="font-semibold">Created At:</span>{" "}
                    {new Date(urlDetails.createdAt).toLocaleString()}
                </p>

                <p>
                    <span className="font-semibold">Redirect Count:</span>{" "}
                    {urlDetails.redirectCount}
                </p>

                {canDelete && (
                    <button
                        onClick={handleDelete}
                        className="mt-4 px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700 transition"
                    >
                        Delete
                    </button>
                )}
            </div>
        </div>
    );
}

export default ShortenUrlDetails;
