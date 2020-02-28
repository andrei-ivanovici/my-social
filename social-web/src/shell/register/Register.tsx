import React, {useState} from "react";
import {TagButton, TagEditField} from "@tag/tag-components-react-v2";
import {appConfig} from "../../services/config.service";
import axios from "axios";
import {navService} from "../../services/router.service";
import style from "./Register.module.scss";

const {root, actions} = style;

interface RegisterModel {
    name: string;
    username: string;
    password: string;
    confirmPassword: string;
}

function useRegisterUser() {

    const [state, setState] = useState<RegisterModel>({
        name: "",
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
            const url = `${appConfig().apiRoot}/auth/register`;
            axios.post(url, state)
                .then(() => {
                    navService.login();
                })
        }
    }
}

export function Register() {
    const {state, setCredentials, register} = useRegisterUser();
    const {username, confirmPassword, password, name} = state;
    return <div className={root}>
        <h1> Register a new User</h1>
        <TagEditField value={name} label={"Name"}
                      onValueChange={e => setCredentials({
                          name: e.detail.value
                      })}/>
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
