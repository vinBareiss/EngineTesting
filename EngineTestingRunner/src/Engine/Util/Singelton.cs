/// <summary>
/// Small abstract Class you can Inherit from to implement the Singelton Pattern
/// </summary>
/// 


namespace EngineTesting
{
    abstract class Singelton<T>
        where T : class, new()
    {
        static T mInstance;

        public static T Instance
        {
            get {
                if (mInstance == null)
                    mInstance = new T();
                return mInstance;
            }
        }

    }

}
