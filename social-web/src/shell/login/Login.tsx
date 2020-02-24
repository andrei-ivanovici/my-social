import React, {useState} from "react";
import style from "./Login.module.scss";
import {TagButton, TagEditField} from "@tag/tag-components-react-v2";
import {CredentialsModel, UserModel} from "../../models/user.models";
import {appConfig} from "../../services/config.service";
import axios from "axios";
import {navService} from "../../services/router.service";

const {
    root,
    main,
    actions,
    field,
    title,
    action,
    error: errorClass
} = style;

async function signInAsync(credentials: CredentialsModel) {
    const url = appConfig().apiRoot;

    const result = await axios.post<UserModel>(`${url}/auth/login`, credentials);
    return result.data;
}

export interface LoginProps {
    onLoginSuccessful: (credentials: UserModel) => void
}


function useLogin(onLoginSuccessful: (credentials: UserModel) => void) {
    const [credentials, setCredentials] = useState<CredentialsModel>({
        password: "",
        username: ""
    });
    const [error, setError] = useState();
    return {
        credentials,
        error,
        setCredentials: (newCredentials: Partial<CredentialsModel>) => {
            setCredentials({
                ...credentials,
                ...newCredentials
            })
        },
        signIn: async () => {
            try {
                const user = await signInAsync(credentials);
                setError(null)
                onLoginSuccessful(user);

            } catch (err) {
                setError("Login failed")
            }
        }
    };
}

function register() {
    navService.register()
}

export function Login({onLoginSuccessful}: LoginProps) {
    const {
        error,
        credentials,
        setCredentials,
        signIn
    } = useLogin(onLoginSuccessful);
    const {username, password} = credentials;

    return <div className={root}>
        <div className={main}>
            <div className={title}> Log in</div>
            <TagEditField className={field} value={username} label={"Username"}
                          onValueChange={e => setCredentials({
                              username: e.detail.value
                          })}/>

            <TagEditField className={field} label={"Password"} editor={"password"}
                          value={password}
                          onValueChange={v => setCredentials({
                              password: v.detail.value
                          })}/>

            <div className={actions}>
                <TagButton className={action}
                           accent={"access"} text={"Log in"}
                           onClick={() => signIn()}
                />
                <TagButton className={action} text={"Register"} onClick={register}/>
            </div>
            {error && <div className={errorClass}>{error}</div>}

        </div>
    </div>;
}


