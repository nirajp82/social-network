import { observable, action } from 'mobx';
import { toast } from 'react-toastify';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

import { rootStore } from './rootStore';
import * as constants from '../utils/constants';

export default class activityHubStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
    }

    @observable.ref hubConnection: HubConnection | null = null;

    @action createHubConnection = (activityId: string) => {
        //Build Hub Connection
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(`${constants.BASE_SERVICE_URL}/activitychat`, {
                //Send token as part as QueryString.
                accessTokenFactory: () => this.rootStore.commonStore.getToken()!
            })
            .configureLogging(LogLevel.Information)
            .build();

        //Start Hub Connection.
        this.hubConnection.start()
            .then(() => this.hubConnection?.state!)
            .then(() => {
                console.log(`Attempting to join group ${activityId}`);
                if (this.hubConnection!.state === 'Connected')
                    this.hubConnection?.invoke('AddToGroup', activityId);
            })
            .catch(error => console.log(`Error establishing a connection: ${error}`));

        //Event Handlers on Receiving message from server.
        this.hubConnection.on('ReceiveComment', comment => {
            this.rootStore.activityStore.onReceivingCommentFromServer(comment);
        });

        this.hubConnection.on("GroupNotification", message => {
            toast.info(message);
        });
    };

    @action stopHubConnection = (activityId: string) => {
        this.hubConnection!.invoke('RemoveFromGroup', activityId)
            .then(() => {
                this.hubConnection!.stop();
            })
            .then(() => {
                console.log('Connection stopped');
            })
            .catch(error => console.log(`Error establishing a connection: ${error}`));
    };

    @action sendComment = async (comment: any) => {
        await this.hubConnection!.invoke('SendComment', comment);
    };
}