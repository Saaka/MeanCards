import { AuthHttpService, Constants } from 'Services';

export class GameRepository {
    httpService = new AuthHttpService();

    createGame = (model) => {
        return this.httpService
            .post(Constants.ApiRoutes.CREATE_GAME, model);
    };

    getGame = (gameCode) => {
        return this.httpService
            .get(`${Constants.ApiRoutes.GET_GAME}/${gameCode}`);
    };

    getGameList = () => {
        return this.httpService
            .get(Constants.ApiRoutes.GAME_LIST);
    };

    joinGame = (gameCode) => {
        return this.httpService
            .get(`${Constants.ApiRoutes.JOIN_GAME}/${gameCode}`);
    };
}