import { createContext } from 'react';
import { configure } from 'mobx';

import userStore from './userStore';
import activityStore from './activityStore';

// don't allow state modifications outside actions
configure({ enforceActions: "always" });

export class rootStore {
    activityStore: activityStore;
    userStore: userStore;

    constructor() {
        this.activityStore = new activityStore(this);
        this.userStore = new userStore(this);
    }
};


export const rootStoreContext = createContext(new rootStore());

