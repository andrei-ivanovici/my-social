using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social.Api.Data.Model
{
    [Table("Posts")]
    public class PostEntity
    {
        public PostEntity()
        {
            Assets = new List<AssetEntity>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<AssetEntity> Assets { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }

        public UserEntity User { get; set; }
    }
}