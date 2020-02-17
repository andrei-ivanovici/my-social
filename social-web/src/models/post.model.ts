export interface EntryOwner {
    name: string;
    username: string;
}

export interface Post {
    createdOn: string;
    text: string;
    owner: EntryOwner;

}
