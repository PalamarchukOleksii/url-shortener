import React, {useEffect, useState} from "react";
import {AuthContext} from "./AuthContext";
import axiosBase from "../../api/axiosBase.js";

export function AuthProvider({children}) {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchUser();
    }, []);

    const fetchUser = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await axiosBase.get("api/users/me");
            setUser(response.data);
        } catch (err) {
            setUser(null);
            if (err.response?.status !== 401) {
                setError(err.response?.data?.message || "Failed to fetch user data");
            }
        } finally {
            setLoading(false);
        }
    };

    const refreshUser = async () => {
        return fetchUser();
    };

    const clearError = () => {
        setError(null);
    };

    const isAuthenticated = !!user;

    const value = {
        user,
        loading,
        error,
        isAuthenticated,
        refreshUser,
        clearError
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}