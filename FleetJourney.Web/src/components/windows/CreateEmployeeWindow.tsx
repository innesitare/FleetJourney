import React, { useState } from "react";
import { Employee } from "../../models/Employee";

import EmployeeService from "../../services/EmployeeService";

type CreateEmployeeWindowProps = {
    onClose: () => void;
    onEmployeeCreated: (employee: Employee) => void;
};

const CreateEmployeeWindow: React.FC<CreateEmployeeWindowProps> = ({onClose, onEmployeeCreated}) => {
    const [newEmployee, setNewEmployee] = useState<Employee>({
        id: "",
        email: "",
        name: "",
        lastName: "",
        birthdate: "",
    });

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewEmployee((previousEmployee) => ({
            ...previousEmployee,
            [name]: value,
        }));
    };

    const handleSaveClick = async () => {
        const createdEmployee: Employee = await EmployeeService.createEmployee(newEmployee);
        onEmployeeCreated(createdEmployee);
        onClose();
    };

    const handleCancelClick = () => {
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
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={newEmployee.email}
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
                        value={newEmployee.name}
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
                        value={newEmployee.lastName}
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
                        value={newEmployee.birthdate}
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

export default CreateEmployeeWindow;
