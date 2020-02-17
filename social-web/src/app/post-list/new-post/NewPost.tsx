import React, {useEffect, useState} from "react";
import style from "./NewPost.module.scss";
import {Action} from "../../../components/action/Action";
import {IoIosAddCircleOutline} from 'react-icons/io'
import {IoIosImage} from 'react-icons/io'

const {
    root,
    actions,
    text: textClass,
    publish: publishClass,
    header,
    preview
} = style;

export interface NewPostContract {
    text: string;
}


function useFileSelection() {

    const [file, setFile] = useState();
    const {previewUrl} = useImagePreview(file);
    return {
        file,
        previewUrl,
        selectFile: () => {
            const element = document.createElement("input");
            element.type = "file";
            element.onchange = e => {
                const selectedFile = element.files && element.files[0];
                if (selectedFile) {
                    setFile(selectedFile)
                }
            };
            element.click();
        }
    }
}

function useImagePreview(file: File) {
    const [previewUrl, setPreviewUrl] = useState();
    useEffect(() => {
        if (!file) {
            return;
        }

        const reader = new FileReader();
        reader.onload = f => {
            if (f.target) {
                setPreviewUrl(f.target.result)
            }
        };
        reader.readAsDataURL(file)
    }, [file]);
    return {
        previewUrl
    }
}

function useNewPost({onPublish}: NewPostProps) {
    const {file, previewUrl, selectFile} = useFileSelection();
    const [newPost, setNewPost] = useState<NewPostContract>({text: ""});


    return {
        previewUrl,
        newPost,
        updateNewPost: (post: Partial<NewPostContract>) => {
            setNewPost({
                ...newPost,
                ...post
            })
        },
        publish: () => {
            const {text} = newPost;
            if (text) {
                onPublish(newPost, file);
                setNewPost({text: ""})
            }
        },
        selectImage: () => {
            selectFile();
        }
    }
}

export interface NewPostProps {
    onPublish: (post: NewPostContract, image?: File) => void;
}


export function NewPost(props: NewPostProps) {
    const {
        updateNewPost,
        selectImage,
        newPost: {text}, publish,
        previewUrl
    } = useNewPost(props);

    return <div className={root}>
        <div className={header}> New post</div>
        <textarea className={textClass} value={text}
                  onChange={e => updateNewPost({text: e.target.value})}
                  placeholder={"What's on your mind ?"}/>
        {
            previewUrl && <div className={preview}>
                <img src={previewUrl} alt={"na"}/>
            </div>
        }

        <div className={actions}>

            <Action icon={<IoIosImage/>} onClick={selectImage}/>
            <Action className={publishClass} icon={<IoIosAddCircleOutline/>} onClick={publish}/>
        </div>
    </div>

}
