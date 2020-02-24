import React, {useState} from "react";
import {TagButton, TagEditField} from "@tag/tag-components-react-v2";
import {appConfig} from "../../services/config.service";
import axios from "axios";
import {navService} from "../../services/router.service";
import style from "./Register.module.scss";

const {root, actions} = style;

interface RegisterModel {
    username: string;
    password: string;
    confirmPassword: string;
}

function useRegisterUser() {

    const [state, setState] = useState<RegisterModel>({
        username: "",
        password: "",
        confirmPassword: ""
    });

    return {
        state,
        setCredentials: (newState: Partial<RegisterModel>) => {
            setState({
                ...state,
                ...newState
            })
        },
        register: () => {
            const url = `${appConfig().apiRoot}/auth`;
            axios.post(url, state)
                .then(() => {
                    navService.login();
                })
        }
    }
}

export function Register() {
    const {state, setCredentials, register} = useRegisterUser();
    const {username, confirmPassword, password} = state;
    return <div className={root}>
        <h1> Register a new User</h1>
        <TagEditField value={username} label={"Username"}
                      onValueChange={e => setCredentials({
                          username: e.detail.value
                      })}/>

        <TagEditField label={"Password"} editor={"password"}
                      value={password}
                      onValueChange={v => setCredentials({
                          password: v.detail.value
                      })}/>

        <TagEditField label={"Confirm Password"} editor={"password"}
                      value={confirmPassword}
                      onValueChange={v => setCredentials({
                          confirmPassword: v.detail.value
                      })}/>
        <div className={actions}>
            <TagButton accent={"access"} text={"Register"}
                       onClick={() => register()}
            />
        </div>


    </div>
}
