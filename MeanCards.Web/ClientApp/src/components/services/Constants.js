let Constants = class Constants {}

Constants.ApiRoutes = class ApiRoutes {
    static get LOGIN() {
        return "auth/login";
    }
    static get GETUSER() {
        return "auth/user";
    }
    static get CREATE_GAME() {
        return "game/create";
    }
}

Constants.Routes = class Routes {
    static get GAME() {
        return "/game";
    }
}

export {
    Constants
}