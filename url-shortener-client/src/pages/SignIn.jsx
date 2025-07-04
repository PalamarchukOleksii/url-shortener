import React, {useState} from "react";
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";
import axiosBase from "../api/axiosBase.js";
import {useAuth} from "../contexts/auth/useAuth.js";

function SignIn() {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const {refreshUser} = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            await axiosBase.post("api/users/signin", {
                login,
                password,
            });
            toast.success("Signed in successfully!");

            await refreshUser();

            navigate("/");
        } catch (error) {
            console.error(error);
            toast.error(error.response?.data?.message || error.response.data.detail || "Failed to Sign in.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <h1>Sign In</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Login:</label>
                    <input
                        type="text"
                        value={login}
                        onChange={(e) => setLogin(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? "Signing in..." : "Sign In"}
                </button>
            </form>
        </>
    );
}

export default SignIn;
