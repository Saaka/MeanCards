import decode from 'jwt-decode';
import {ConfigService} from '../Services';
import Axios from 'axios';

export class AuthService {
    configService = new ConfigService();
    tokenName = 'user_token';

    login = (email, password) => {
        return Axios.post(`${this.configService.ApiUrl}/auth/login`, {
            password: password,
            email: email
        })
        .then(resp => {
            this.setToken(resp.data.token);
            return resp.data;
        });
    };

    isLoggedIn() {
        const token = this.getToken();
        return !!token && !this.isTokenExpired(token);
    }

    isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000) { // Checking if token is expired. N
                return true;
            }
            else
                return false;
        }
        catch (err) {
            return false;
        }
    };

    logout() {
        localStorage.removeItem(this.tokenName);
    };

    setToken(token) {
        localStorage.setItem(this.tokenName, token);
    };

    getToken() {
        return localStorage.getItem(this.tokenName);
    };

    getTokenData() {
        return decode(this.getToken());
    };
}