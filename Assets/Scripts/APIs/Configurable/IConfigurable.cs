namespace APIs.Configurable
{
    public interface IConfigurable<in TConfiguration> 
    {
        void Configure(TConfiguration configuration);
    }
}