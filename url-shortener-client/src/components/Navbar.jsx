import {NavLink, useNavigate} from "react-router-dom";
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
        <nav className="bg-white shadow-md">
            <div className="max-w-4xl mx-auto px-4 py-3 flex items-center justify-between">
                <div className="flex gap-4 items-center">
                    <NavLink
                        to="/"
                        end
                        className={({isActive}) =>
                            `font-medium ${isActive ? "text-blue-600" : "text-gray-800"} hover:text-blue-500`
                        }
                    >
                        Home
                    </NavLink>
                    <NavLink
                        to="/about"
                        className={({isActive}) =>
                            `font-medium ${isActive ? "text-blue-600" : "text-gray-800"} hover:text-blue-500`
                        }
                    >
                        About
                    </NavLink>
                </div>

                <div className="flex gap-4 items-center">
                    {!isAuthenticated ? (
                        <>
                            <NavLink
                                to="/signin"
                                className={({isActive}) =>
                                    `font-medium ${isActive ? "text-blue-600" : "text-gray-800"} hover:text-blue-500`
                                }
                            >
                                Sign In
                            </NavLink>
                            <NavLink
                                to="/signup"
                                className={({isActive}) =>
                                    `font-medium ${isActive ? "text-blue-600" : "text-gray-800"} hover:text-blue-500`
                                }
                            >
                                Sign Up
                            </NavLink>
                        </>
                    ) : (
                        <button
                            onClick={handleLogout}
                            type="button"
                            className="font-medium text-gray-800 hover:text-red-500"
                        >
                            Logout
                        </button>
                    )}
                </div>
            </div>
        </nav>
    );
}

export default Navbar;
