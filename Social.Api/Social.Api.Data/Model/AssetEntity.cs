using System.ComponentModel.DataAnnotations.Schema;

namespace Social.Api.Data.Model
{
    [Table("Assets")]
    public class AssetEntity
    {
        public int Id { get; set; }
        public string Hash { get; set; }
    }
}