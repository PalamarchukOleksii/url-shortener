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
        setDescription(aboutData?.description || "");
        setEditing(false);
    };

    if (loading) {
        return (
            <div className="max-w-2xl mx-auto mt-10 p-4 text-center">
                <h1 className="text-2xl font-semibold mb-4">About</h1>
                <p className="text-gray-500">Loading...</p>
            </div>
        );
    }

    return (
        <div className="max-w-2xl mx-auto mt-10 p-6 bg-white rounded-lg shadow-md border">
            <h1 className="text-2xl font-bold mb-4">About</h1>

            {isAdmin ? (
                editing ? (
                    <div className="space-y-4">
                        <textarea
                            rows={6}
                            className="w-full p-3 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            disabled={saving}
                        />
                        <div className="flex gap-3">
                            <button
                                onClick={handleSave}
                                disabled={saving}
                                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
                            >
                                {saving ? "Saving..." : "Save"}
                            </button>
                            <button
                                onClick={handleCancel}
                                disabled={saving}
                                className="bg-gray-300 text-gray-800 px-4 py-2 rounded hover:bg-gray-400 disabled:opacity-50"
                            >
                                Cancel
                            </button>
                        </div>
                    </div>
                ) : (
                    <>
                        <p className="text-gray-700 mb-4">{aboutData?.description}</p>
                        <button
                            onClick={() => setEditing(true)}
                            className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                        >
                            Edit
                        </button>
                    </>
                )
            ) : (
                <p className="text-gray-700">{aboutData?.description}</p>
            )}
        </div>
    );
}

export default About;
