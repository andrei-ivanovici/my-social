import React from "react";
import {Header} from "./header/Header";
import {UserModel} from "../models/user.models";
import {PostList} from "./post-list/PostList";

export interface AppProps {
    user: UserModel;
}

export function App({user}: AppProps) {
    return <div>
        <Header user={user}/>
        <PostList user={user}/>
    </div>
}
