import React, {useEffect, useState} from "react";
import axiosBase from "../api/axiosBase.js";
import {toast} from "react-toastify";
import {useAuth} from "../contexts/auth/useAuth.js";

function About() {
    const {user} = useAuth();
    const isAdmin = user?.roles?.includes("Admin");

    const [aboutData, setAboutData] = useState(null);
    const [description, setDescription] = useState("");
    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);
    const [editing, setEditing] = useState(false);

    useEffect(() => {
        const languageCode = "en";

        setLoading(true);
        axiosBase
            .get(`api/about/${languageCode}`)
            .then((response) => {
                setAboutData(response.data);
                setDescription(response.data.description || "");
            })
            .catch((err) => {
                console.error(err);
                toast.error(
                    err.response?.data?.message ||
                    err.response?.data?.detail ||
                    "Failed to load About page."
                );
            })
            .finally(() => setLoading(false));
    }, []);

    const handleSave = async () => {
        if (!aboutData?.id?.value) {
            toast.error("Invalid About data ID");
            return;
        }

        setSaving(true);
        try {
            await axiosBase.patch("api/about", {
                id: {value: aboutData.id.value},
                description,
            });
            toast.success("About page updated successfully!");
            setAboutData((prev) => ({...prev, description}));
            setEditing(false);
        } catch (err) {
            console.error(err);
            toast.error(
                err.response?.data?.message ||
                err.response?.data?.detail ||
                "Failed to update About page."
            );
        } finally {
            setSaving(false);
        }
    };

    const handleCancel = () => {
        // revert to saved description and exit editing
        setDescription(aboutData?.description || "");
        setEditing(false);
    };

    if (loading) return (
        <div>
            <h1>About</h1>
            <p>Loading...</p>
        </div>);

    return (
        <div>
            <h1>About</h1>

            {isAdmin ? (
                editing ? (
                    <>
          <textarea
              rows={6}
              cols={60}
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              disabled={saving}
          />
                        <br/>
                        <button onClick={handleSave} disabled={saving}>
                            {saving ? "Saving..." : "Save"}
                        </button>
                        <button
                            onClick={handleCancel}
                            disabled={saving}
                            style={{marginLeft: 8}}
                        >
                            Cancel
                        </button>
                    </>
                ) : (
                    <>
                        <p>{aboutData?.description}</p>
                        <button onClick={() => setEditing(true)}>Edit</button>
                    </>
                )
            ) : (
                <p>{aboutData?.description}</p>
            )}
        </div>
    );
}

export default About;
