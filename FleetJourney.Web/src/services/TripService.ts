import { Trip } from "../models/Trip";

const API_BASE_URL = "http://localhost:8080/api";

class TripService {
    async getTrips(): Promise<Trip[]> {
        const response = await fetch(`${API_BASE_URL}/trips`);
        return await response.json();
    }

    async createTrip(trip: Trip): Promise<Trip> {
        const response = await fetch(`${API_BASE_URL}/trips`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(trip),
        });

        return await response.json();
    }

    async updateTrip(trip: Trip): Promise<Trip> {
        const response = await fetch(`${API_BASE_URL}/trips/${trip.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(trip),
        });

        return await response.json();
    }

    async deleteTrip(tripId: string): Promise<void> {
        await fetch(`${API_BASE_URL}/trips/${tripId}`, {
            method: "DELETE",
        });
    }
}

export default new TripService();