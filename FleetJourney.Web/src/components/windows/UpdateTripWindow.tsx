import React, {ChangeEvent, useEffect, useState} from "react";
import { Trip } from "../../models/Trip";
import TripService from "../../services/TripService";
import {Employee} from "../../models/Employee.ts";
import {Car} from "../../models/Car.ts";
import EmployeeService from "../../services/EmployeeService.ts";
import CarPoolService from "../../services/CarPoolService.ts";

type UpdateTripWindowProps = {
    trip: Trip;
    onClose: () => void;
    onUpdate: (updatedTrip: Trip) => void;
};

const UpdateTripWindow: React.FC<UpdateTripWindowProps> = ({trip, onClose, onUpdate}) => {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [cars, setCars] = useState<Car[]>([]);
    const [updatedTrip, setUpdatedTrip] = useState<Trip>(trip);

    useEffect(() => {
        fetchEmployees();
        fetchCars();
    }, []);

    const fetchEmployees = async () => {
        const fetchedEmployees: Employee[] = await EmployeeService.getEmployees();
        setEmployees(fetchedEmployees);
    };

    const fetchCars = async () => {
        const fetchedCars: Car[] = await CarPoolService.getCars();
        setCars(fetchedCars);
    };

    const handleSelectChange = (e: ChangeEvent<HTMLSelectElement>) => {
        const { name, value } = e.target;
        setUpdatedTrip((previousTrip) => ({
            ...previousTrip,
            [name]: value,
        }));
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const { name, value } = e.target;
        setUpdatedTrip((prevTrip) => ({
            ...prevTrip,
            [name]: value,
        }));
    };

    const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const { name, checked } = e.target;
        setUpdatedTrip((prevTrip) => ({
            ...prevTrip,
            [name]: checked,
        }));
    };

    const handleSaveClick = async (): Promise<void> => {
        const updatedTripData: Trip = await TripService.updateTrip(updatedTrip);
        onUpdate(updatedTripData);
        onClose();
    };

    const handleCancelClick = (): void => {
        onClose();
    };

    return (
        <div className="fixed top-0 left-0 flex items-center justify-center w-full h-full bg-gray-900 bg-opacity-50">
            <div className="bg-white p-6 rounded shadow-xl flex flex-col">
                <div className="mb-4">
                    <label htmlFor="employeeId" className="text-black-2 font-semibold">
                        Employee:
                    </label>
                    <select
                        className="w-full p-2 border border-gray-300 rounded"
                        name="employeeId"
                        value={updatedTrip.employeeId}
                        onChange={handleSelectChange}
                    >
                        {employees.map((employee) => (
                            <option key={employee.id} value={employee.id}>
                                {`${employee.name} ${employee.lastName}`}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="mb-4">
                    <label htmlFor="carId" className="text-black-2 font-semibold">
                        Car:
                    </label>
                    <select
                        className="w-full p-2 border border-gray-300 rounded"
                        name="carId"
                        value={updatedTrip.carId}
                        onChange={handleSelectChange}
                    >
                        {cars.map((car) => (
                            <option key={car.id} value={car.id}>
                                {`${car.brand} (${car.model})`}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="mb-4">
                    <label htmlFor="startMileage" className="text-black-2 font-semibold">
                        Start Mileage:
                    </label>
                    <input
                        type="number"
                        className="w-full p-2 border border-gray-300 rounded"
                        name="startMileage"
                        placeholder="Start Mileage"
                        value={updatedTrip.startMileage}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="endMileage" className="text-black-2 font-semibold">
                        End Mileage:
                    </label>
                    <input
                        type="number"
                        className="w-full p-2 border border-gray-300 rounded"
                        name="endMileage"
                        placeholder="End Mileage"
                        value={updatedTrip.endMileage}
                        onChange={handleInputChange}
                    />
                </div>
                <div>
                <div className="flex items-center mb-3">
                    <input
                        type="checkbox"
                        id="privateTripCheckbox"
                        checked={updatedTrip.isPrivateTrip}
                        onChange={handleCheckboxChange}
                        name="isPrivateTrip"
                        className="mr-3 mt-1"
                    />
                    <label htmlFor="privateTripCheckbox" className="text-black dark:text-white">
                        Is it a private trip?
                    </label>
                </div>
                </div>
                <div className="flex justify-end">
                    <button
                        className="px-4 py-2 mr-2 text-black-2 font-semibold bg-blue-500 rounded hover:bg-blue-700"
                        onClick={handleSaveClick}
                    >
                        Save
                    </button>
                    <button
                        className="px-4 py-2 text-black-2 font-semibold bg-gray-500 rounded hover:bg-gray-700"
                        onClick={handleCancelClick}
                    >
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    );
};

export default UpdateTripWindow;
