import { ConfigService, HttpService, AuthService } from './Services';
import Axios from 'axios';

class AuthHttpService extends HttpService {
    configService = new ConfigService();
    authService = new AuthService();

    getAuthConfiguration = () => {
        var token = this.authService.getToken();

        return {
            headers: {
                'Authorization': `Bearer ${token}` 
            }
        };
    };

    get = (address) => {
        return Axios
            .get(this.getFullAddress(address), this.getAuthConfiguration());
    };

    post = (address, data) => {
        return Axios
            .post(this.getFullAddress(address), data, this.getAuthConfiguration());
    };
};

export { AuthHttpService }
