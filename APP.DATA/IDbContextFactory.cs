namespace Portal.Data
{
    public interface IDbContextFactory<T>
    {
        T GetContext();
    }
}
