import {Employee} from "../models/Employee";

const API_BASE_URL = "http://localhost:8080/api";

class EmployeeService {
    async getEmployees(): Promise<Employee[]> {
        const response = await fetch(`${API_BASE_URL}/employees`);
        return await response.json();
    }

    async getEmployeeById(employeeId: string): Promise<Employee> {
        const response = await fetch(`${API_BASE_URL}/employees/${employeeId}`);
        return await response.json();
    }

    async createEmployee(employee: Employee): Promise<Employee> {
        const response = await fetch(`${API_BASE_URL}/employees`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(employee),
        });

        return await response.json();
    }

    async updateEmployee(employee: Employee): Promise<Employee> {
        const response = await fetch(`${API_BASE_URL}/employees/${employee.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(employee),
        });

        return await response.json();
    }

    async deleteEmployee(employeeId: string): Promise<void> {
        await fetch(`${API_BASE_URL}/employees/${employeeId}`, {
            method: "DELETE",
        });
    }
}

export default new EmployeeService();