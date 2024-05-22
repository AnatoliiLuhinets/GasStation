using Reflection;

namespace Managers
{
    public abstract class ISingleton
    {
    }

    public class Singleton<T> : ISingleton where T : ISingleton
    {
        public readonly static T Instance;

        static Singleton()
        {
            Instance = typeof(T).InvokeDefaultConstructor() as T;
        }

    }
}