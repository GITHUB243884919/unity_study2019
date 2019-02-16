namespace UFrame.Common
{
    public class Singleton<Type> where Type : new()
    {
        static public Type GetInstance()
        {
            if (null == s_Instance)
            {
                s_Instance = new Type();
            }
            return s_Instance;

        }

        static public void DestroyInstance()
        {
            s_Instance = default(Type);
        }

        protected Singleton() { }

        private static Type s_Instance = default(Type);
    }
}

