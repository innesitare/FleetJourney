import React, { useEffect, useState, ReactNode } from 'react';

interface PageLoaderProps {
    children: ReactNode;
}

const PageLoader: React.FC<PageLoaderProps> = ({ children }) => {
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const preloader = document.getElementById('preloader');

        if (preloader) {
            setTimeout(() => {
                preloader.style.display = 'none';
                setLoading(false);
            }, 2000);
        }

        setTimeout(() => setLoading(false), 1000);
    }, []);

    return loading ? (
        <p className="text-center text-danger">Failed to load app</p>
    ) : (
        <>{children}</>
    );
};

export default PageLoader;