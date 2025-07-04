import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase.js";

function About() {
    const [aboutData, setAboutData] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const languageCode = "en";

        axiosBase.get(`api/about/${languageCode}`)
            .then((response) => setAboutData(response.data))
            .catch((err) => {
                console.error(err);
                setError("Failed to load about info.");
            });


    }, []);

    if (error) return <div>Error: {error}</div>;
    if (!aboutData) return <div>Loading...</div>;

    return (
        <div>
            <h1>About</h1>
            <p>{aboutData.description}</p>
        </div>
    );
}

export default About;
