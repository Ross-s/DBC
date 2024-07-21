import * as React from "react";
import { createRoot } from 'react-dom/client';

const root = createRoot(document.getElementById("app"));
root.render(<button className="btn">Hello, world!</button>);