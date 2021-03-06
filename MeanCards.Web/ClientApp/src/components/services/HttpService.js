import { ConfigService } from 'Services';
import Axios from 'axios';

class HttpService {

    constructor(baseAddress) {
        this.configService = new ConfigService();
        this.baseAddress =  baseAddress || this.configService.ApiUrl;
    };

    _axios = null;
    get axios() {
        if(!this._axios)
            this._axios = this.createAxios(this.baseAddress);

        return this._axios;
    }

    createAxios = (baseAddress) => {
        return Axios.create({
            baseURL: `${baseAddress}/`
        });
    };

    get = (address) => {
        return this.axios
            .get(address)
            .catch(err => {
                if (err.response)
                    throw err.response.data;
                else
                    throw err.message;
            });
    };

    post = (address, data) => {
        return this.axios
            .post(address, data)
            .catch(err => {
                if (err.response)
                    throw err.response.data;
                else
                    throw err.message;
            });
    };
};

export { HttpService }
