namespace Game339.Shared.Infastructure.DependencyInjection
{
    public interface IMiniContainer
    {
        T Resolve<T>();
    }
}