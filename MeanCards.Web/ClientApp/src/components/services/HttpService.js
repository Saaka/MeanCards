import { ConfigService } from './Services';
import Axios from 'axios';

class HttpService {
    configService = new ConfigService();

    getFullAddress = (address) => {
        return `${this.configService.ApiUrl}/${address}`;
    };

    get = (address) => {
        return Axios.get(this.getFullAddress(address));
    };

    post = (address, data) => {
        return Axios.post(this.getFullAddress(address), data);
    };
};

export { HttpService }
