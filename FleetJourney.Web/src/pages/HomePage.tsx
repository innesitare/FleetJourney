import Breadcrumb from "../components/Breadcrumb.tsx";
import DefaultLayout from '../layout/DefaultLayout.tsx';
import Metrics from "../components/Metrics.tsx";


const HomePage = () => {
    return (
        <DefaultLayout>
            <Breadcrumb pageName="Home" />
                <Metrics />
        </DefaultLayout>
    );
};

export default HomePage;