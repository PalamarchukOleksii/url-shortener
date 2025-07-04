import React, {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import axiosBase from "../api/axiosBase";
import {BACKEND_BASE_URL} from "../utils/constants.js";
import {toast} from "react-toastify";

function ShortenUrlDetails() {
    const {id} = useParams();
    const [urlDetails, setUrlDetails] = useState(null);

    useEffect(() => {
        if (!id) return;

        axiosBase
            .get(`api/shortenedurls/${id}`)
            .then((res) => setUrlDetails(res.data))
            .catch((error) => toast.error(error.response.data.message));
    }, [id]);

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
                <strong>Short Code:</strong> {urlDetails.shortCode}
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
                <strong>Creator ID:</strong> {urlDetails.creatorId.value}
            </p>
            <p>
                <strong>Created At:</strong>{" "}
                {new Date(urlDetails.createdAt).toLocaleString()}
            </p>
            <p>
                <strong>Redirect Count:</strong> {urlDetails.redirectCount}
            </p>
        </div>
    );
}

export default ShortenUrlDetails;
