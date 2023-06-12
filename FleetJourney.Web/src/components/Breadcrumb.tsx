import { Link } from 'react-router-dom';
import { Component } from "react";

interface BreadcrumbProps {
  pageName: string;
}

class Breadcrumb extends Component<BreadcrumbProps> {
    render() {
        let {pageName} = this.props;
        return (
            <div className="mb-6 flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
                <h2 className="text-title-md2 font-semibold text-black dark:text-white">
                    {pageName}
                </h2>
                <nav>
                    <ol className="flex items-center gap-2">
                        <li>
                            <Link to="/">FleetJourney /</Link>
                        </li>
                        <li className="text-primary">{pageName}</li>
                    </ol>
                </nav>
            </div>
        );
    }
}

export default Breadcrumb;