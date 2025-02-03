import EmployerList from "../components/EmployerList";
import DashboardPage from "../components/DashboardPage";
import { EmployerProvider } from '../context/EmployerContext';
import Test from '../pages/PhoneEdit'


const MainPage = () => {

    return (
        <EmployerProvider>
            <div>
                <DashboardPage/>
            </div>
            <div>
                <EmployerList/>
            </div>
        </EmployerProvider>
    )
}

export default MainPage;