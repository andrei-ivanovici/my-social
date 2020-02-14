import React, {useState} from "react";
import style from "./Login.module.scss";
import {TagButton, TagEditField} from "@tag/tag-components-react-v2";
import {CredentialsModel, UserModel} from "../../models/user.models";
import {appConfig} from "../../services/config.service";
import axios from "axios";

const {root, main, actions, field, title, action} = style;

async function signInAsync(credentials: CredentialsModel) {
    const url = appConfig().apiRoot;

    const result = await axios.post<UserModel>(`${url}/auth/login`, credentials);
    return result.data;
}

export interface LoginProps {
    onLoginSuccessful: (credentials: UserModel) => {}
}

function useLogin() {
    const [credentials, setCredentials] = useState<CredentialsModel>({
        password: "",
        username: ""
    });
    return {
        credentials,
        setUsername: (username: string) => {
            setCredentials({
                ...credentials,
                username
            })
        },
        setPassword: (password: string) => {
            setCredentials({
                ...credentials,
                password
            })
        }
    };
}

export function Login({onLoginSuccessful}: LoginProps) {
    const {
        credentials,
        setPassword, setUsername,
        signIn
    } = useLogin();
    const {username, password} = credentials;

    return <div className={root}>
        <div className={main}>
            <div className={title}> Log in</div>
            <TagEditField className={field} value={username} label={"Username"}
                          onValueChange={e => setUsername(e.detail.value)}/>

            <TagEditField className={field} label={"Password"} editor={"password"}
                          value={password} onValueChange={v => setPassword(v.detail.value)}/>

            <div className={actions}>
                <TagButton className={action}
                           accent={"access"} text={"Log in"}
                           onClick={() => onLogin(credentials)}
                />
                <TagButton className={action} text={"Register"}/>
            </div>

        </div>
    </div>;
}


