import axios from 'axios';
import { warning, failure } from './toastService';
import { language, currentUser } from './store';
import jwt_decode from "jwt-decode";

export default class HttpService {

    constructor() {
        this.base_url = "https://localhost:44382/api";

        this.instance = axios.create();
        this._token = null;
        this.language = "en";

        language.subscribe(value => this.language = value);

        this.instance.interceptors.request.use((config) => {
            config.headers["X-Language"] = this.language;
        
            if(this._token != null) config.headers["Authorization"] = this._token;

            return config;
        });

        this.instance.interceptors.response.use(response => {
            if(!response.data.infos) return response;

            if(response.data.hasInfo) 
                toast.push(response.data.infos.infos.join(" "));
            
            if(response.data.hasMessage)
                toast.push(response.data.infos.messages.join(" "));

            return response;
        }, error => {
            if(error === undefined) return error;

            if(!error.response.status)
                failure("Error: Failed to fetch!");

            if(error.response.data) {
                if(error.response.data.hasError)
                    failure(`Error: ${error.response.data.infos.errors.join(" ")}`);

                if(error.response.data.hasInfo) 
                    warning(error.response.data.infos.infos.join(" "));
            }

            return Reject(error);
        })
    }

    setToken(token) {
        this._token = token;

        if(token != null) {
            let decoded = jwt_decode(token);

            currentUser.update(() => {
                return {
                    username: decoded['username'],
                    userType: decoded['userType']
                };
            });
        }
        else {
            currentUser.update(() => null);
        }
    }

    getJobOffers() {
        return this.instance.get(`${this.base_url}/job/offer`);
    }

    login(loginRequestDto) {
        return this.instance.post(`${this.base_url}/authentication/authenticate`, loginRequestDto);
    }

}