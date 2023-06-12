import Breadcrumb from '../components/Breadcrumb';
import DefaultLayout from '../layout/DefaultLayout';
import Profile from "../components/Profile.tsx";

const ProfilePage = () => {
    return (
        <DefaultLayout>
            <Breadcrumb pageName="Profile"/>
                <Profile />
        </DefaultLayout>
    );
};

export default ProfilePage;