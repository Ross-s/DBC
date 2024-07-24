﻿import { createRoot } from 'react-dom/client';
import { createBrowserRouter, Link, RouterProvider } from "react-router-dom";



const router = createBrowserRouter([
    {
        path: "/",
        element: (
            <div>
                <h1>Hello World</h1>
                <Link to="about">About Us</Link>
            </div>
        ),
    },
    {
        path: "about",
        element: <div>About</div>,
    },
    {
        path: "*",
        element: <div>No Match</div>,
    }
]);


const root = createRoot(document.getElementById("app") as any);
root.render(<RouterProvider router={router} />);