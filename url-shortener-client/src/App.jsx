import {Route, Routes} from "react-router-dom";
import Home from "./pages/Home.jsx";
import About from "./pages/About.jsx";
import SignIn from "./pages/SignIn.jsx";
import SignUp from "./pages/SignUp.jsx";
import ShortenUrlDetails from "./pages/ShortenUrlDetails.jsx";
import Layout from "./components/Layout.jsx";

function App() {

    return (
        <Layout>
            <Routes>
                <Route exact path="/" element={<Home/>}/>
                <Route path="/about" element={<About/>}/>
                <Route path="/signin" element={<SignIn/>}/>
                <Route path="/signup" element={<SignUp/>}/>
                <Route path="/shortenedurls/:id" element={<ShortenUrlDetails/>}/>
            </Routes>
        </Layout>
    )
}

export default App
