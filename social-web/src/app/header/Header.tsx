import React from "react";
import style from "./Header.module.scss"
import {UserModel} from "../../models/user.models";

const {
    root,
    content,
    user: userClass
} = style;

export interface HeaderProps {
    user: UserModel
}

export function Header({user}: HeaderProps) {
    return <div className={root}>
        <div className={content}>
            Social
        </div>
        <div className={userClass}>
            {user.name}
        </div>


    </div>
}
