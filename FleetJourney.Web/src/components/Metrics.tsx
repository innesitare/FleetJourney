import { useEffect, useState } from 'react';
import EmployeeService from '../services/EmployeeService';
import TripService from '../services/TripService';
import CarPoolService from '../services/CarPoolService';

const Metrics = () => {
    const [employeeCount, setEmployeeCount] = useState(0);
    const [tripCount, setTripCount] = useState(0);
    const [carCount, setCarCount] = useState(0);
    const [tripPercentage, setTripPercentage] = useState(0);

    useEffect(() => {
        fetchEmployeeCount();
        fetchTripCount();
        fetchCarCount();
    }, []);

    useEffect(() => {
        calculateTripPercentage();
    }, [employeeCount, tripCount]);

    const fetchEmployeeCount = async () => {
        const employees = await EmployeeService.getEmployees();
        setEmployeeCount(employees.length);
    };

    const fetchTripCount = async () => {
        const trips = await TripService.getTrips();
        setTripCount(trips.length);
    };

    const fetchCarCount = async () => {
        const cars = await CarPoolService.getCars();
        setCarCount(cars.length);
    };

    const calculateTripPercentage = () => {
        if (employeeCount > 0) {
            const percentage = (tripCount / employeeCount) * 100;
            setTripPercentage(Number(percentage.toFixed(2)));
        }
    };

    return (
        <div className="mt-4 grid grid-cols-12 gap-4 md:mt-2 md:gap-6 2xl:mt-7.5 2xl:gap-7.5">
            <div className="col-span-4 md:col-span-3 xl:col-span-2">
                <div
                    className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark">
                    <div
                        className="flex h-11.5 w-11.5 items-center justify-center rounded-full bg-meta-2 dark:bg-meta-4">
                        <svg fill="#000000" height="20" width="22" version="1.1" id="Layer_1"
                             xmlns="http://www.w3.org/2000/svg" viewBox="0 0 297 297">
                            <path
                                d="M284.583,276.914h-30.892c-1.702-19.387-5.386-59.954-7.771-75.858c-2.715-18.113-17.518-33.527-36.835-38.357 l-20.107-5.027c-3.25,19.41-20.159,34.253-40.477,34.253s-37.227-14.843-40.477-34.253l-20.107,5.027 c-19.317,4.829-34.121,20.244-36.835,38.357c-2.385,15.904-6.069,56.471-7.771,75.858H12.417 c-5.546,0-10.043,4.497-10.043,10.043c0,5.546,4.497,10.043,10.043,10.043h272.165c5.546,0,10.043-4.497,10.043-10.043 C294.625,281.411,290.13,276.914,284.583,276.914z M227.18,276.914h-14.06v-65.258H83.881v65.258H69.821v-72.288 c0-3.883,3.147-7.03,7.03-7.03h143.3c3.883,0,7.03,3.147,7.03,7.03V276.914z"></path>
                            <path
                                d="M148.501,114.634c27.103,0,48.125-35.753,48.125-66.509C196.626,18.89,177.735,0,148.501,0s-48.125,18.89-48.125,48.125 C100.376,78.882,121.398,114.634,148.501,114.634z"></path>
                            <path
                                d="M129.774,124.617c-0.053,7.43-1.516,17.95-8.265,25.567c0,14.883,12.107,27.681,26.991,27.681 c14.884,0,26.991-12.798,26.991-27.681c-6.748-7.617-8.212-18.136-8.263-25.567c-5.86,2.617-12.139,4.078-18.727,4.078 C141.914,128.695,135.634,127.234,129.774,124.617z"></path>
                        </svg>
                    </div>
                    <div className="mt-4 flex items-end justify-between">
                        <div>
                            <h4 className="text-title-md font-bold text-black dark:text-white">{employeeCount}</h4>
                            <span className="text-sm font-medium">Total Employees</span>
                        </div>
                    </div>
                </div>
            </div>
            <div className="col-span-4 md:col-span-3 xl:col-span-2">
                <div
                    className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark">
                    <div
                        className="flex h-11.5 w-11.5 items-center justify-center rounded-full bg-meta-2 dark:bg-meta-4">
                        <svg version="1.1" id="_x32_" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"
                             width="24" height="22" fill="#000000">
                            <path className="st0" d="M472.271,113.106c-13.991-49.444-54.964-70.478-101.933-62.483c-43.254,7.362-65.847,61.281-76.372,93.978 c3.124-43.308-4.871-103.534-56.541-131.656C158.477-30.028,108.51,46.922,103.513,56.915c-4.997,9.994-5.013,11.906,8.994,10.993 c4.169-0.273,40.505-10.5,76.153-4.052l16.786-15.474l7.776,21.806c29.544,10.352,53.247,36.882,57.51,76.661 c-23.968-29.466-60.929-48.437-102.558-48.437c-72.39,0-131.055,57.079-131.055,127.487c0,5.668,0.11,1.6,0.11,1.6 c0.203,2.67,1.951,4.974,4.466,5.888c2.514,0.906,5.34,0.257,7.198-1.663c0,0,1.467-2.272,8.026-8.268 c11.555-10.572,24.265-19.885,37.881-27.826l12.446-23.242l16.801,8.924c25.218-10.048,52.716-15.591,81.51-15.591 c19.003,0,37.443,2.413,55.026,6.933c-40.552-2.685-101.684,18.457-125.886,70.071c-29.98,63.958-0.468,114.722,3.794,118.696 c3.498,3.256,7.729,0.719,8.869-4.958c2.514-12.406,9.4-35.594,22.048-61.6l-5.106-20.626l14.053,3.598 c17.629-31.073,43.425-63.7,79.932-85.678c-21.877,48.196-40.63,113.715-31.495,197.012H52.546v15.81H228.79 c2.17,14.522,5.231,29.582,9.228,45.142h-87.536v15.81h91.752h86.896h58.196v-15.81h-71.063 c-7.136-10.127-16.145-25.186-24.046-45.142h176.26v-15.81H286.58c-13.741-42.964-20.705-104.088,0.188-183.894 c9.962,12.874,22.641,31.557,33.212,54.316l13.382,1.687l-5.981,16.005c7.324,19.597,12.601,41.489,13.335,64.918 c0.187,5.981,3.794,10.408,5.637,6.489c28.231-60.164,19.346-121.741-20.221-157.67c24.952,8.229,45.346,28.294,61.226,51.247 l18.956,4.068l-3.529,21.361c13.288,25.28,21.455,50.311,24.359,64.661c1.156,5.652,4.154,9.338,7.761,2.905 c21.548-38.335,24.327-111.896-14.6-152.166c-30.184-31.214-83.446-33.268-116.237-24.617 c21.533-36.203,79.136-38.897,79.136-38.897s9.119-20.986,11.118-23.984c1.999-2.998,12.992,20.44,12.992,20.44l58.008,23.695 c0,0,7.449,5.091,8.9-3.56C475.097,130.945,475.628,122.896,472.271,113.106z"></path>
                        </svg>
                    </div>
                    <div className="mt-4 flex items-end justify-between">
                        <div>
                            <h4 className="text-title-md font-bold text-black dark:text-white">{tripCount}</h4>
                            <span className="text-sm font-medium">Total Trips</span>
                        </div>
                    </div>
                </div>
            </div>
            <div className="col-span-4 md:col-span-3 xl:col-span-2">
                <div
                    className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark">
                    <div
                        className="flex h-11.5 w-11.5 items-center justify-center rounded-full bg-meta-2 dark:bg-meta-4">
                        <svg width="24" height="22" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path fillRule="evenodd" clipRule="evenodd" d="M6.77988 6.77277C6.88549 6.32018 7.28898 6 7.75372 6H16.2463C16.711 6 17.1145 6.32018 17.2201 6.77277L17.7398 9H17H7H6.26019L6.77988 6.77277ZM2 11H2.99963C2.37194 11.8357 2 12.8744 2 14V15C2 16.3062 2.83481 17.4175 4 17.8293V20C4 20.5523 4.44772 21 5 21H6C6.55228 21 7 20.5523 7 20V18H17V20C17 20.5523 17.4477 21 18 21H19C19.5523 21 20 20.5523 20 20V17.8293C21.1652 17.4175 22 16.3062 22 15V14C22 12.8744 21.6281 11.8357 21.0004 11H22C22.5523 11 23 10.5523 23 10C23 9.44772 22.5523 9 22 9H21C20.48 9 20.0527 9.39689 20.0045 9.90427L19.9738 9.77277L19.1678 6.31831C18.851 4.96054 17.6405 4 16.2463 4H7.75372C6.35949 4 5.14901 4.96054 4.8322 6.31831L4.02616 9.77277L3.99548 9.90426C3.94729 9.39689 3.51999 9 3 9H2C1.44772 9 1 9.44772 1 10C1 10.5523 1.44772 11 2 11ZM7 11C5.34315 11 4 12.3431 4 14V15C4 15.5523 4.44772 16 5 16H6H18H19C19.5523 16 20 15.5523 20 15V14C20 12.3431 18.6569 11 17 11H7ZM6 13.5C6 12.6716 6.67157 12 7.5 12C8.32843 12 9 12.6716 9 13.5C9 14.3284 8.32843 15 7.5 15C6.67157 15 6 14.3284 6 13.5ZM16.5 12C15.6716 12 15 12.6716 15 13.5C15 14.3284 15.6716 15 16.5 15C17.3284 15 18 14.3284 18 13.5C18 12.6716 17.3284 12 16.5 12Z" fill="#000000"></path>
                        </svg>
                    </div>
                    <div className="mt-4 flex items-end justify-between">
                        <div>
                            <h4 className="text-title-md font-bold text-black dark:text-white">{carCount}</h4>
                            <span className="text-sm font-medium">Total Cars</span>
                        </div>
                    </div>
                </div>
            </div>
            <div className="col-span-4 md:col-span-3 xl:col-span-6">
                <div
                    className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark">
                    <div
                        className="flex h-11.5 w-11.5 items-center justify-center rounded-full bg-meta-2 dark:bg-meta-4">
                        <svg width="24" height="22" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path fillRule="evenodd" clipRule="evenodd" d="M5 6C5 4.34315 6.34315 3 8 3C9.65685 3 11 4.34315 11 6V14C11 15.6569 9.65685 17 8 17C6.34315 17 5 15.6569 5 14V6ZM8 5C7.44772 5 7 5.44772 7 6V14C7 14.5523 7.44772 15 8 15C8.55228 15 9 14.5523 9 14V6C9 5.44772 8.55228 5 8 5ZM3 20C3 19.4477 3.44772 19 4 19H20C20.5523 19 21 19.4477 21 20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20ZM16 9C14.3431 9 13 10.3431 13 12V14C13 15.6569 14.3431 17 16 17C17.6569 17 19 15.6569 19 14V12C19 10.3431 17.6569 9 16 9ZM15 12C15 11.4477 15.4477 11 16 11C16.5523 11 17 11.4477 17 12V14C17 14.5523 16.5523 15 16 15C15.4477 15 15 14.5523 15 14V12Z" fill="#000000"></path>
                        </svg>
                    </div>
                    <div className="mt-4 flex items-end justify-between">
                        <div>
                            <h4 className="text-title-md font-bold text-black dark:text-white">{tripPercentage}%</h4>
                            <span className="text-sm font-medium">Percentage of trips per available employees</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Metrics;