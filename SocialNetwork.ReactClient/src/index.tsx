import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import ScrollToTop from './layout/ScrollToTop';
import './layout/styles.css';

import App from './layout/App';


ReactDOM.render(
    <BrowserRouter>
        <React.Fragment>
            <ScrollToTop />
            <App />
        </React.Fragment>
    </BrowserRouter>,
    document.querySelector("#root"));
