namespace IdentityServer.Data
{
    public class DbInitializer
    {
        public void Initialize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
