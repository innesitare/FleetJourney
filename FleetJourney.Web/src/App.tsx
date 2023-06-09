import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';

import SignIn from './pages/Authentication/SignIn';
import SignUp from './pages/Authentication/SignUp';
import Dashboard from './pages/Dashboard/Dashboard.tsx';
import Settings from './pages/Settings';
import Trips from './pages/Trips.tsx';
import Alerts from './pages/UiElements/Alerts';
import Employees from "./pages/Employees.tsx";
import CarPool from "./pages/CarPool.tsx";

function App() {
    const [loading, setLoading] = useState<boolean>(true);

    const preloader = document.getElementById('preloader');

    if (preloader) {
        setTimeout(() => {
            preloader.style.display = 'none';
            setLoading(false);
        }, 2000);
    }

    useEffect(() => {
        setTimeout(() => setLoading(false), 1000);
    }, []);

    return loading ? (
        <p className=" text-center text-danger">Failed to lead app</p>
    ) : (
        <>
            <Routes>
                <Route path="/" element={<Dashboard />} />
                <Route path="/employees" element={<Employees />} />
                <Route path="/cars" element={<CarPool />} />
                <Route path="/trips" element={<Trips />} />
                <Route path="/settings" element={<Settings />} />
                <Route path="/ui/alerts" element={<Alerts />} />
                <Route path="/auth/signin" element={<SignIn />} />
                <Route path="/auth/signup" element={<SignUp />} />
            </Routes>
        </>
    );
}

export default App;
