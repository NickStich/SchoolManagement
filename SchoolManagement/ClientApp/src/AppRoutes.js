import { AddStudentPage } from "./components/student/AddStudentPage.js";
import { StudentList } from "./components/student/StudentList";
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
    // {
    //     path: '/student',
    //     element: <AddStudentPage />
    // },
    {
        path: '/student',
        element: <StudentList />
    },

    //{
    //    path: '/teacher',
    //    element: <AddTeacherPage />
    //},
    //{
    //    path: '/teachers',
    //    element: <TeacherList />
    //}
];

export default AppRoutes;
