import {Car} from "../models/Car";

const API_BASE_URL = "http://localhost:8080/api/cars";

class CarPoolService {
    async getCars(): Promise<Car[]> {
        const response = await fetch(API_BASE_URL);
        return await response.json();
    }

    async createCar(newCar: Car): Promise<Car> {
        const response = await fetch(API_BASE_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newCar),
        });

        return await response.json();
    }

    async updateCar(car: Car): Promise<Car> {
        const response = await fetch(`${API_BASE_URL}/${car.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(car),
        });

        return await response.json();
    }

    async deleteCar(id: string): Promise<void> {
        await fetch(`${API_BASE_URL}/${id}`, {
            method: "DELETE",
        });
    }
}

export default new CarPoolService();
