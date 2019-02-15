import {
    AuthHttpService,
    HttpService,
    Constants,
    UserTokenService
} from 'Services';

export class AuthService {
    httpService = new HttpService();
    authHttpService = new AuthHttpService();
    tokenService = new UserTokenService();

    loginWithCredentials = (email, password) => {
        return this.httpService
            .post(Constants.ApiRoutes.LOGIN, {
                password: password,
                email: email
            })
            .then(this.onLogin);
    };

    onLogin = (resp) => {
        
        this.tokenService
            .setToken(resp.data.token);
        return {
            code: resp.data.code,
            email: resp.data.email,
            name: resp.data.name,
            imageUrl: resp.data.imageUrl,
            token: resp.data.token
        };
    };

    getUser = () => {
        var token = this.tokenService
            .getToken();
        return this.authHttpService
            .get(Constants.ApiRoutes.GETUSER)
            .then(resp => {
                return {
                    ...resp.data,
                    token: token
                };
            });
    };

    isLoggedIn() {
        const token = this.tokenService
            .getToken();
        return !!token && !this.tokenService
            .isTokenExpired(token);
    }

    logout = () => this.tokenService.removeToken();
}