namespace Social.Api.Infrastructure
{
    public class TenantAccessor
    {
        public string ActiveUser { get; set; }

        public TenantAccessor()
        {
            ActiveUser = "Undefined";
        }
    }
}