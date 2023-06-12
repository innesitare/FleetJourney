import { Car } from "../models/Car";
import { API_BASE_URL } from "../config";

class CarPoolService {
    getCars = async (): Promise<Car[]> => {
        const response = await fetch(`${API_BASE_URL}/cars`);
        return response.json();
    }

    getCar = async(carId: string): Promise<Car> => {
        const response = await fetch(`${API_BASE_URL}/cars/${carId}`);
        return response.json();
    }

    createCar = async (newCar: Car): Promise<Car> => {
        const response = await fetch(`${API_BASE_URL}/cars`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newCar),
        });

        return response.json();
    }

    updateCar = async (car: Car): Promise<Car> => {
        const response = await fetch(`${API_BASE_URL}/cars/${car.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(car),
        });

        return response.json();
    }

    deleteCar = async (id: string): Promise<void> => {
        await fetch(`${API_BASE_URL}/cars/${id}`, {
            method: "DELETE",
        });
    }
}

export default new CarPoolService();