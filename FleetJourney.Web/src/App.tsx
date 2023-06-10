import React, { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';

import Dashboard from './pages/Dashboard.tsx';
import Trips from './pages/Trips.tsx';
import Employees from "./pages/Employees.tsx";
import CarPool from "./pages/CarPool.tsx";
import Callback from "./pages/Callback.tsx";

const App: React.FC = () => {
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
                <Route path="/callback" element={<Callback />} />
            </Routes>
        </>
    );
}

export default App;