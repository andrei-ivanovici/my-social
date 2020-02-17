import React from "react";
import style from "./Actions.module.scss"
import clsx from "clsx";

const {root} = style;

export interface ActionProps {
    icon: JSX.Element;
    onClick: () => void;
    className?: string
}

export function Action({icon, onClick, className}: ActionProps) {
    const clazz = clsx(className, root);
    return <div onClick={onClick} className={clazz}>
        {icon}
    </div>
}
