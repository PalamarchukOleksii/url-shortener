import Navbar from "./Navbar";
import {ToastContainer} from "react-toastify";
import React from "react";

function Layout({children}) {
    return (
        <div className="min-h-screen bg-gray-50 text-gray-900">
            <Navbar/>
            <main className="max-w-4xl mx-auto px-4 py-6">
                {children}
            </main>
            <ToastContainer position="top-right" autoClose={3000} hideProgressBar/>
        </div>
    );
}

export default Layout;
