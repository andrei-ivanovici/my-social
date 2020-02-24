import {createBrowserHistory} from "history";

export const browserHistory = createBrowserHistory();

export const navService = {
    login: () => browserHistory.push("/login"),
    register: () => browserHistory.push("/register")
};
