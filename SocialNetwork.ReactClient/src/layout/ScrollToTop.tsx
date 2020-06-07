import { useEffect } from "react";
import { useLocation } from "react-router-dom";

export default function ScrollToTop() {
    const { pathname } = useLocation();
    //Display top of the form as soon as location(URL) changes. 
    useEffect(() => {
        window.scrollTo(0, 0);
    }, [pathname]);

    return null;
}