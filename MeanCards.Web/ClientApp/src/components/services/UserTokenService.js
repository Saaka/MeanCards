import decode from 'jwt-decode';

export class UserTokenService {
    tokenName = 'user_token_mc';

    setToken = (token) => localStorage.setItem(this.tokenName, token);

    getToken = () => localStorage.getItem(this.tokenName);

    removeToken = () => localStorage.removeItem(this.tokenName);

    getTokenData = () => decode(this.getToken());

    isTokenExpired = (token) => {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000) {
                return true;
            } else
                return false;
        } catch (err) {
            return false;
        }
    };
};