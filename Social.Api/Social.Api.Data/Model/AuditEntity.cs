using System;

namespace Social.Api.Data.Model
{
    public class AuditEntity
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date;
        public string Event { get; set; }
    }
}