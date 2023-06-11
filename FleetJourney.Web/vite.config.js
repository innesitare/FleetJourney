import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import dotenv from 'dotenv';

dotenv.config();

export default defineConfig(() => {
    return {
        plugins: [react()],
        preview: {
            port: 4040,
        },
        define: {
            'process.env': process.env,
        },
    };
});
