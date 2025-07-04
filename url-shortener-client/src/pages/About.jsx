import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase.js";
import {toast} from "react-toastify";

function About() {
    const [aboutData, setAboutData] = useState(null);

    useEffect(() => {
        const languageCode = "en";

        axiosBase.get(`api/about/${languageCode}`)
            .then((response) => setAboutData(response.data))
            .catch((err) => {
                console.error(err);
                toast.error(err.response.data.message);
            });
    }, []);

    if (!aboutData) return <div>Loading...</div>;

    return (
        <div>
            <h1>About</h1>
            <p>{aboutData.description}</p>
        </div>
    );
}

export default About;
