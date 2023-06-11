import React from 'react';
import { Route, Routes } from 'react-router-dom';

import PageLoader from './components/PageLoader.tsx';
import DashboardPage from './pages/DashboardPage.tsx';
import TripsPage from './pages/TripsPage.tsx';
import EmployeesPage from "./pages/EmployeesPage.tsx";
import CarPoolPage from "./pages/CarPoolPage.tsx";
import CallbackPage from "./pages/CallbackPage.tsx";
import ProfilePage from './pages/ProfilePage.tsx';

const App: React.FC = () => {
    return (
        <PageLoader>
            <Routes>
                <Route path="/" element={<DashboardPage />} />
                <Route path="/employees" element={<EmployeesPage />} />
                <Route path="/cars" element={<CarPoolPage />} />
                <Route path="/trips" element={<TripsPage />} />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="/callback" element={<CallbackPage />} />
            </Routes>
        </PageLoader>
    );
}

export default App;