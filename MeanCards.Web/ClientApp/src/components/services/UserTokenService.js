import decode from 'jwt-decode';

export class UserTokenService {
    tokenName = 'user_token_mc';

    setToken = (token) => localStorage.setItem(this.tokenName, token);

    getToken = () => localStorage.getItem(this.tokenName);

    removeToken = () => localStorage.removeItem(this.tokenName);

    getTokenData = () => decode(this.getToken());

    isTokenValid = () => {
        const token = this.getToken();
        return !!token && !this.isTokenExpired(token);
    };

    isTokenExpired = (token) => {
        const decoded = decode(token);
        if (decoded.exp < Date.now() / 1000) {
            return true;
        } else
            return false;
    };
};