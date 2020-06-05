import React from 'react';
import ReactDOM from 'react-dom';
import { Router } from 'react-router-dom';
import 'mobx-react-lite/batchingForReactDom';
import ScrollToTop from './layout/ScrollToTop';
import './layout/styles.css';
import 'react-toastify/dist/ReactToastify.min.css';
import createBrowserHistory from './utils/createBrowserHistory';
import App from './layout/App';


ReactDOM.render(
    <Router history={createBrowserHistory}>
        <React.Fragment>
            <ScrollToTop />
            <App />
        </React.Fragment>
    </Router>,
    document.querySelector("#root"));
