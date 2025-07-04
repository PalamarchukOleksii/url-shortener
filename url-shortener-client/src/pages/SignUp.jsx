import React, {useState} from "react";
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";
import axiosBase from "../api/axiosBase.js";

function SignUp() {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [passwordConfirm, setPasswordConfirm] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (password !== passwordConfirm) {
            toast.error("Passwords do not match.");
            return;
        }

        setLoading(true);

        try {
            await axiosBase.post("api/users/signup", {
                login,
                password,
            });
            toast.success("Sign up successful! Please sign in.");
            navigate("/signin");
        } catch (error) {
            console.error(error);
            toast.error(err.response?.data?.message || err.response.data.detail || "Failed to Sign up.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <h1>Sign Up</h1>
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
                <div>
                    <label>Confirm Password:</label>
                    <input
                        type="password"
                        value={passwordConfirm}
                        onChange={(e) => setPasswordConfirm(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? "Signing up..." : "Sign Up"}
                </button>
            </form>
        </>
    );
}

export default SignUp;
