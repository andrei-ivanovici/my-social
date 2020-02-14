using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social.Api.Data.Model
{
    [Table("Comments")]
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text;
        public ICollection<AssetEntity> Assets { get; set; }
        public ICollection<CommentEntity> Replies { get; set; }
        public int ParentId { get; set; }
    }
}