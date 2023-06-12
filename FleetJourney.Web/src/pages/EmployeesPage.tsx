import Breadcrumb from '../components/Breadcrumb';
import EmployeesTable from '../components/tables/EmployeesTable.tsx';
import DefaultLayout from '../layout/DefaultLayout';

const EmployeesPage = () => {
    return (
        <DefaultLayout>
            <Breadcrumb pageName="Employees" />
            <div className="flex flex-col gap-10">
                <EmployeesTable />
            </div>
        </DefaultLayout>
    );
};

export default EmployeesPage;