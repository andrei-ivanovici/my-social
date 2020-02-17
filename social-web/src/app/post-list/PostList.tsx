import React, {useEffect, useState} from "react";
import {appConfig} from "../../services/config.service";
import axios from 'axios';
import {Post} from "../../models/post.model";
import {PostItem} from "./post-item/PostItem";
import style from "./PostList.module.scss"
import {NewPost, NewPostContract} from "./new-post/NewPost";
import {UserModel} from "../../models/user.models";
import {publicDecrypt} from "crypto";

const {root} = style;

export interface PostListProps {
    user: UserModel;
}

async function loadPostsAsync() {
    const api = `${appConfig().apiRoot}/posts`;
    const newPosts = await axios.get(api);
    return newPosts.data
}

async function addPostAsync(newPost: Post) {
    const api = `${appConfig().apiRoot}/posts`;
    return axios.post(api, newPost);
}


function usePosts(props: PostListProps) {
    const [posts, setPosts] = useState<Post[]>([]);


    useEffect(() => {
        (async () => {
            setPosts(await loadPostsAsync())
        })()
    }, []);

    return {
        posts,
        publish: async (newPost: NewPostContract) => {
            const post: Post = {
                ...newPost,
                owner: props.user,
                ...{} as any
            };
            await addPostAsync(post as Post);
            setPosts(await loadPostsAsync())
        }
    }
}

export function PostList(props: PostListProps) {
    const {posts, publish} = usePosts(props);
    return <div className={root}>
        <NewPost onPublish={publish}/>
        {posts.map((item, idx) => <PostItem key={idx} post={item}/>)}
    </div>
}
