import { AddStudentPage } from "./components/AddStudentPage.js";
import { StudentList } from "./components/StudentList";
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/fetch-data',
        element: <FetchData />
    },
    {
        path: '/student',
        element: <AddStudentPage />
    },
    {
        path: '/students',
        element: <StudentList />
    }
];

export default AppRoutes;
