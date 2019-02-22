let Constants = class Constants {}

Constants.ApiRoutes = class ApiRoutes {
    static get LOGIN() {
        return "auth/login";
    }
    static get GOOGLE() {
        return "auth/google";
    }
    static get GETUSER() {
        return "auth/user";
    }
    static get CREATE_GAME() {
        return "game/create";
    }
    static get GAME_LIST() {
        return "game/list";
    }
    static get JOIN_GAME() {
        return "game/join";
    }
    static get GET_GAME() {
        return "game";
    }
}

Constants.Routes = class Routes {
    static get GAME() {
        return "/game";
    }
    static get GAMELIST() {
        return "/gameList";
    }
}

export {
    Constants
}