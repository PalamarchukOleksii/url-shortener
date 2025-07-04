import {NavLink, useNavigate} from "react-router-dom"; // adjust path if needed
import {useAuth} from "../contexts/auth/useAuth.js";
import axiosBase from "../api/axiosBase.js";

function Navbar() {
    const {isAuthenticated, refreshUser} = useAuth();
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            await axiosBase.post("api/users/signout");
            await refreshUser();
            navigate("/");
        } catch (error) {
            console.error("Logout failed", error);
        }
    };

    return (
        <nav>
            <ul>
                <li><NavLink to="/" end>Home</NavLink></li>
                <li><NavLink to="/about">About</NavLink></li>

                {!isAuthenticated ? (
                    <>
                        <li><NavLink to="/signin">Sign In</NavLink></li>
                        <li><NavLink to="/signup">Sign Up</NavLink></li>
                    </>
                ) : (
                    <li>
                        <button onClick={handleLogout} type="button">
                            Logout
                        </button>
                    </li>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;
