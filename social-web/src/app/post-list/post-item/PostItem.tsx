import React, {useMemo} from "react";
import style from "./PostItem.module.scss"
import {Post} from "../../../models/post.model";
import moment from "moment";

const {
    root,
    title: titleClass,
    content,
    actions
} = style;

export interface PostItemProps {
    post: Post;
}

function toLocalDate(date: Date) {
    return moment(date).fromNow();
}

function usePost({post}: PostItemProps) {

    const {owner: {username, name}, createdOn, text,} = post;
    const title = useMemo(() => `${name} @${username} - ${toLocalDate(new Date(createdOn))}`, [post]);

    return {
        title,
        text
    }
}

export function PostItem(props: PostItemProps) {

    const {title, text} = usePost(props);
    return <div className={root}>
        <div className={titleClass}>{title}</div>
        <div className={content}>
            {text}
        </div>
        <div className={actions}>

        </div>
    </div>
}
