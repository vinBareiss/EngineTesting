namespace EngineTestingNrDuo.src.util
{
    /// <summary>
    /// Class to Inherit for implementation of Singeltonpattern
    /// </summary>
    /// <typeparam name="T">Type of the singelton</typeparam>
    [System.Obsolete("Kann nicht verwendet werden!",true)]
    class Singelton<T> where T : new()
    {
        //private field of instance
        static T mInstance = default(T);

        /// <summary>
        /// Get instance of this class, create one if none exist
        /// </summary>
        /// <returns>Singelton-instance of this class</returns>
        public static T GetInstance()
        {
            if (mInstance == null) {
                return mInstance = new T();
            }
            else {
                return mInstance;
            }
        }
    }
}
