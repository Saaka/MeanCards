
let Constants = class Constants { }

Constants.ApiRoutes = class ApiRoutes {
    static get LOGIN() {
        return "auth/login";
    }
    static get CREATE_GAME() {
        return "game/create";
    }
}

export { Constants }