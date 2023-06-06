import Breadcrumb from '../components/Breadcrumb';
import TripsTable from '../components/tables/TripsTable.tsx';
import DefaultLayout from '../layout/DefaultLayout';

const Trips = () => {
    return (
        <DefaultLayout>
            <Breadcrumb pageName="Trips" />
            <div className="flex flex-col gap-10">
                <TripsTable />
            </div>
        </DefaultLayout>
    );
};

export default Trips;
