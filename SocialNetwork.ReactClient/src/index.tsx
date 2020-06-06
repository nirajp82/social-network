import React from 'react';
import ReactDOM from 'react-dom';
import { Router } from 'react-router-dom';

import dateFnsLocalizer from 'react-widgets-date-fns';

import 'mobx-react-lite/batchingForReactDom';
import 'react-toastify/dist/ReactToastify.min.css';
import 'react-widgets/dist/css/react-widgets.css';


import './layout/styles.css';

import ScrollToTop from './layout/ScrollToTop';
import createBrowserHistory from './utils/createBrowserHistory';
import App from './layout/App';

dateFnsLocalizer();

ReactDOM.render(
    <Router history={createBrowserHistory}>
        <React.Fragment>
            <ScrollToTop />
            <App />
        </React.Fragment>
    </Router>,
    document.querySelector("#root"));
