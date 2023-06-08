import React, { useState } from "react";
import { Employee } from "../../models/Employee";
import EmployeeService from "../../services/EmployeeService";

type UpdateEmployeeWindowProperties = {
    employee: Employee;
    onClose: () => void;
    onEmployeeUpdated: (employee: Employee) => void;
};

const UpdateEmployeeWindow: React.FC<UpdateEmployeeWindowProperties> = ({employee, onClose, onEmployeeUpdated}) => {
    const [updatedEmployee, setUpdatedEmployee] = useState<Employee>(employee);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const { name, value } = e.target;
        setUpdatedEmployee((prevEmployee) => ({
            ...prevEmployee,
            [name]: value,
        }));
    };

    const handleSaveClick = async (): Promise<void> => {
        const updatedEmployeeData: Employee = await EmployeeService.updateEmployee(updatedEmployee);
        onEmployeeUpdated(updatedEmployeeData);
        onClose();
    };

    const handleCancelClick = (): void => {
        onClose();
    };

    return (
        <div className="fixed top-0 left-0 flex items-center justify-center w-full h-full bg-gray-900 bg-opacity-50">
            <div className="bg-white p-6 rounded shadow-xl flex flex-col">
                <div className="mb-4">
                    <label htmlFor="email" className="text-black-2 font-semibold">
                        Email:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="email"
                        placeholder="Email"
                        value={updatedEmployee.email}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="name" className="text-black-2 font-semibold">
                        Name:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="name"
                        placeholder="Name"
                        value={updatedEmployee.name}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="lastName" className="text-black-2 font-semibold">
                        Last Name:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="lastName"
                        placeholder="Last Name"
                        value={updatedEmployee.lastName}
                        onChange={handleInputChange}
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="birthdate" className="text-black-2 font-semibold">
                        Birthdate:
                    </label>
                    <input
                        className="w-full p-2 border border-gray-300 rounded"
                        type="text"
                        name="birthdate"
                        placeholder="Birthdate"
                        value={updatedEmployee.birthdate}
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

export default UpdateEmployeeWindow;
