import { createContext } from 'react';
import { configure } from 'mobx';

import activityStore from './activityStore';
import commonStore from './commonStore';
import userStore from './userStore';
import profileStore from './profileStore';

// don't allow state modifications outside actions
configure({ enforceActions: "always" });

export class rootStore {

    activityStore: activityStore;
    userStore: userStore;
    commonStore: commonStore;
    profileStore: profileStore;

    constructor() {
        this.activityStore = new activityStore(this);
        this.userStore = new userStore(this);
        this.commonStore = new commonStore(this);
        this.profileStore = new profileStore(this);
    }
};


export const rootStoreContext = createContext(new rootStore());

