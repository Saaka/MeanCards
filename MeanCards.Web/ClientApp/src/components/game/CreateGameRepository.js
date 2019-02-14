import { AuthHttpService, Constants } from 'Services';

export class CreateGameRepository {
    httpService = new AuthHttpService();

    createGame = (model) => {
        return this.httpService
            .post(Constants.ApiRoutes.CREATE_GAME, model);
    };
}