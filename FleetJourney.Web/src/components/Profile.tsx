import { useAuth0 } from '@auth0/auth0-react';

const Profile = () => {
    const { user } = useAuth0();

    return (
        <div className="overflow-hidden rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
            <div className="relative z-20 h-35 md:h-40" />
            <div className="px-4 pb-6 text-center lg:pb-8 xl:pb-11.5">
                <div className="relative z-30 mx-auto -mt-22 h-30 w-30 max-w-44 rounded-full bg-white/20 p-1 backdrop-blur sm:h-44 sm:max-w-44 sm:p-3">
                    <div className="relative drop-shadow-2 flex justify-center items-center h-full">
                        <img src={user?.picture} alt="profile" className="max-h-full" />
                    </div>
                </div>
                <div className="mt-4">
                    <h3 className="mb-1.5 text-2xl font-semibold text-black dark:text-white">
                        Administrator
                    </h3>
                    <p className="font-medium">Programmer. Employee at FleetJourney.</p>
                    <div className="mx-auto mt-4.5 mb-5.5 max-w-94 rounded-md border border-stroke py-2.5 shadow-1 dark:border-strokedark dark:bg-[#37404F]">
                        <div className="items-center content-center border-r border-stroke px-4">
                            <span className="font-semibold text-black dark:text-white">{user?.email}</span>
                        </div>
                    </div>
                    <div className="mx-auto max-w-180">
                        <h4 className="font-semibold text-black dark:text-white">About Me</h4>
                        <p className="mt-4.5">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque posuere
                            fermentum urna, eu condimentum mauris tempus ut. Donec fermentum blandit aliquet.
                            Etiam dictum dapibus ultricies. Sed vel aliquet libero. Nunc a augue fermentum,
                            pharetra ligula sed, aliquam lacus.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Profile;