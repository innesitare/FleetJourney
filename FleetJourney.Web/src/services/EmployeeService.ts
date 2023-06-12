import { Employee } from "../models/Employee";
import { API_BASE_URL } from '../config';

class EmployeeService {
    getEmployees = async (): Promise<Employee[]> => {
        const response = await fetch(`${API_BASE_URL}/employees`);
        return response.json();
    }

    getEmployeeById = async (employeeId: string): Promise<Employee> => {
        const response = await fetch(`${API_BASE_URL}/employees/${employeeId}`);
        return response.json();
    }

    createEmployee = async (employee: Employee): Promise<Employee> => {
        const response = await fetch(`${API_BASE_URL}/employees`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(employee),
        });

        return response.json();
    }

    updateEmployee = async (employee: Employee): Promise<Employee> => {
        const response = await fetch(`${API_BASE_URL}/employees/${employee.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(employee),
        });

        return response.json();
    }

    deleteEmployee = async (employeeId: string): Promise<void> => {
        await fetch(`${API_BASE_URL}/employees/${employeeId}`, {
            method: "DELETE",
        });
    }
}

export default new EmployeeService();