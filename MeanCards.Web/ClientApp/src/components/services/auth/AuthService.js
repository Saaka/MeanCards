import decode from 'jwt-decode';
import { HttpService, Constants } from '../Services';

export class AuthService {
    tokenName = 'user_token';
    httpService = new HttpService();

    login = (email, password) => {
        return this.httpService.post(Constants.ApiRoutes.LOGIN, {
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