import React from "react";
import {Redirect, Route} from "react-router";

export interface GuardedRouteProps {
    path: string;
    canActivate: () => boolean;
    success: () => JSX.Element;
    redirect: string;
}

export function GuardedRoute({path, canActivate, success, redirect}: GuardedRouteProps) {
    return <Route path={path}>
        {
            canActivate()
                ? success()
                : <Redirect to={redirect}/>
        }
        }
    </Route>
}


