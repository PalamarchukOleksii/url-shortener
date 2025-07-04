import Navbar from "./Navbar";
import {ToastContainer} from "react-toastify";
import React from "react";

function Layout({children}) {
    return (
        <>
            <Navbar/>
            <main>{children}</main>
            <ToastContainer/>
        </>
    );
}

export default Layout;
