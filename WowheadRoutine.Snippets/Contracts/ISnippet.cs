namespace WowheadRoutine.Snippets
{
    public interface ISnippet
    {
        void Run(params object[] args);
        void RunAsync(params object[] args);
        T GetResult<T>();
        void OnLoaded();
        void OnUnloaded();
    }
}
