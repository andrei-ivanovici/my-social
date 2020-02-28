using System;

namespace Social.Api.Contracts
{
    public class Audit
    {
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Event { get; set; }
    }
}