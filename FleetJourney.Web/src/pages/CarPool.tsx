import Breadcrumb from '../components/Breadcrumb';
import CarPoolTable from '../components/tables/CarPoolTable.tsx';
import DefaultLayout from '../layout/DefaultLayout';

const CarPool = () => {
    return (
        <DefaultLayout>
            <Breadcrumb pageName="Cars" />
            <div className="flex flex-col gap-10">
                <CarPoolTable />
            </div>
        </DefaultLayout>
    );
};

export default CarPool;