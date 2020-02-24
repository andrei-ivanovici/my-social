import React, {useEffect, useState} from 'react';
import {Route, Router, Switch} from "react-router";
import {browserHistory} from "../services/router.service";
import {GuardedRoute} from "../infrastructure/GuardedRoute";
import {App} from "../app/App";
import {Login} from "./login/Login";
import {UserModel} from "../models/user.models";
import {Register} from "./register/Register";


function useUser() {
    const USER = "USER";
    const [user, setUser] = useState<UserModel>();

    useEffect(() => {
        const activeUser = sessionStorage.getItem(USER);
        if (activeUser) {
            setUser(JSON.parse(activeUser));
        }

    }, []);

    useEffect(() => {
        if (user) {
            sessionStorage.setItem(USER, JSON.stringify(user))
        }
    }, [user]);


    return {
        user,
        setUser
    };
}

export function Root() {
    const {user, setUser} = useUser();
    return <Router history={browserHistory}>
        <Switch>

            <GuardedRoute path={"/login"}
                          redirect={"/"}
                          canActivate={() => !user}
                          success={() => <Login
                              onLoginSuccessful={setUser}
                          />}/>

            <Route path={"/register"}>
                <Register/>
            </Route>
            <GuardedRoute path={"/"}
                          redirect={"/login"}
                          canActivate={() => !!user}
                          success={() => <App user={user!}/>}/>
        </Switch>


    </Router>
}
