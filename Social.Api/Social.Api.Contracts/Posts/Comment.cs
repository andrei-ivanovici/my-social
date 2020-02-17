namespace Social.Api.Contracts.Posts
{
    public class Comment
    {
        public string Text { get; set; }
        public EntryOwner Owner { get; set; }
    }
}