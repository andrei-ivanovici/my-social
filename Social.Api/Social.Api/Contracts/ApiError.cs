using System.Data.Common;

namespace Social.Api.Contracts
{
    public class ApiError
    {
        private string Id { get; set; }
        public string Error { get; set; }
    }
}