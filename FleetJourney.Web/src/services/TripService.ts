import { Trip } from "../models/Trip";
import { API_BASE_URL } from "../config";

class TripService {
    getTrips = async (): Promise<Trip[]> => {
        const response = await fetch(`${API_BASE_URL}/trips`);
        return response.json();
    }

    createTrip = async (trip: Trip): Promise<Trip> => {
        const response = await fetch(`${API_BASE_URL}/trips`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(trip),
        });

        return response.json();
    }

    updateTrip = async (trip: Trip): Promise<Trip> => {
        const response = await fetch(`${API_BASE_URL}/trips/${trip.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(trip),
        });

        return response.json();
    }

    deleteTrip = async (tripId: string): Promise<void> => {
        await fetch(`${API_BASE_URL}/trips/${tripId}`, {
            method: "DELETE",
        });
    }
}

export default new TripService();