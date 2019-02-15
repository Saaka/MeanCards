import { HttpService, UserTokenService } from 'Services';
import Axios from 'axios';

class AuthHttpService extends HttpService {

    createAxios = () => {
        var headers = this.getHeaders();
        return Axios.create({
            baseURL: `${this.baseAddress}/`,
            headers: headers
        });
    };

    getHeaders = () => {
        var tokenService = new UserTokenService();
        var token = tokenService.getToken();

        return  {
            'Authorization': `Bearer ${token}` 
        };
    };

    get = (address) => {
        return this.axios
            .get(address)
            .catch(err => {
                throw err.response.data;
            });
    };

    post = (address, data) => {
        return this.axios
            .post(address, data)
            .catch(err => {
                throw err.response.data;
            });
    };
};

export { AuthHttpService }
