import React, {useState} from "react";
import {Car} from "../../models/Car.ts";

type CreateCarWindowProperties = {
    onClose: () => void;
    onCarCreated: (car: Car) => void;
}

const CreateCarWindow : React.FC<CreateCarWindowProperties> = ({onClose, onCarCreated,}) => {
    const [newCar, setNewCar] = useState<Car>({
        id: "",
        licensePlateNumber: "",
        brand: "",
        model: "",
        currentMileage: 0,
        endOfLifeMileage: 0,
        maintenanceInterval: 0,
    });

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewCar((previousCar) => ({
            ...previousCar,
            [name]: value,
        }));
    };

    const handleSaveClick = async () => {
        try {
            await fetch("http://localhost:8080/api/cars", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newCar),
            });

            onCarCreated(newCar);
            onClose();
        } catch (error) {
            console.error("Error creating car", error);
        }
    };

    const handleCancelClick = () => {
        onClose();
    };

    return (
        <div className="fixed top-0 left-0 flex items-center justify-center w-full h-full bg-gray-900 bg-opacity-50">
            <div className="bg-white p-6 rounded shadow-xl flex flex-col">
                <div className="mb-4">
                    <label htmlFor="licensePlateNumber" className="text-black-2 font-semibold">
                        License Plate Number:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="licensePlateNumber"
                        placeholder="License Plate Number"
                        value={newCar.licensePlateNumber}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="brand" className="text-black-2 font-semibold">
                        Brand:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="brand"
                        placeholder="Brand"
                        value={newCar.brand}
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
                        value={newCar.model}
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
                        value={newCar.currentMileage}
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
                        value={newCar.endOfLifeMileage}
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
                        value={newCar.maintenanceInterval}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="flex justify-end">
                    <button
                        className="bg-blue-500 hover:bg-blue-600 text-black-2 font-semibold py-2 px-4 rounded"
                        onClick={handleSaveClick}
                    >
                        Save
                    </button>
                    <button
                        className="mr-2 bg-gray-200 hover:bg-gray-300 text-black-2 font-semibold py-2 px-4 rounded"
                        onClick={handleCancelClick}
                    >
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    );
};

export default CreateCarWindow;