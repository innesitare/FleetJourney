import React, { useState } from "react";
import { Car } from "../../models/Car.ts";

type UpdateCarWindowProperties = {
    car: Car;
    onClose: () => void;
    onCarUpdated: (car: Car) => void;
};

const UpdateCarWindow: React.FC<UpdateCarWindowProperties> = ({car, onClose, onCarUpdated}) => {
    const [updatedCar, setUpdatedCar] = useState<Car>(car);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setUpdatedCar((previousCar) => ({
            ...previousCar,
            [name]: value,
        }));
    };

    const handleSaveClick = async () => {
        try {
            await fetch(`http://localhost:8080/api/cars/${car.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(updatedCar),
            });

            onCarUpdated(updatedCar);
            onClose();
        } catch (error) {
            console.error("Error updating car", error);
        }
    };

    const handleCancelClick = () => {
        onClose();
    };

    return (
        <div className="fixed top-0 left-0 flex items-center justify-center w-full h-full bg-gray-900 bg-opacity-50">
            <div className="bg-white p-6 rounded shadow-xl flex flex-col">
                <div className="mb-4">
                    <label htmlFor="brand" className="text-black-2 font-semibold">
                        Brand:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="brand"
                        placeholder="Brand"
                        value={updatedCar.brand}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="model" className="text-black-2 font-semibold">
                        Model:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="model"
                        placeholder="Model"
                        value={updatedCar.model}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="currentMileage" className="text-black-2 font-semibold">
                        Current Mileage:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="currentMileage"
                        placeholder="Current Mileage"
                        value={updatedCar.currentMileage}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="endOfLifeMileage" className="text-black-2 font-semibold">
                        End of Life Mileage:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="endOfLifeMileage"
                        placeholder="End of Life Mileage"
                        value={updatedCar.endOfLifeMileage}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="maintenanceInterval" className="text-black-2 font-semibold">
                        Maintenance Interval:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="maintenanceInterval"
                        placeholder="Maintenance Interval"
                        value={updatedCar.maintenanceInterval}
                        onChange={handleInputChange}
                    />
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

export default UpdateCarWindow;