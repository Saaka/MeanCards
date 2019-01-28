import {decode} from 'jwt-decode';
import {ConfigService} from '../Services';
import Axios from 'axios';

export class AuthService {
    configService = new ConfigService();

    login = async (email, password) => {
        const resp = await Axios.post(`${this.configService.ApiUrl}/auth/login`, {
            password: password,
            email: email
        });
        this.setToken(resp.data.token);
        return Promise.resolve(resp.data);
    };

    setToken(token) {
        localStorage.setItem('user_token', token);
    }
}