
export class ConfigService {
    constructor(){
        this.ApiUrl = process.env.REACT_APP_API_URL;
    }
}