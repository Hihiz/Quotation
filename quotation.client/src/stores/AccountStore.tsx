import { makeObservable, observable } from 'mobx';
import { referenceEnhancer } from 'mobx/dist/internal';

export default class AccountStore {
    data = {
        accessToken: "",
        refreshToken: ""
    }

    constructor() {
        makeObservable(this, {
            data: observable
        })
    }

    updateAccessToken = (accessToken: string, refreshToken: string) => {
        this.data.accessToken = accessToken;
        this.data.refreshToken = refreshToken;
    }
}