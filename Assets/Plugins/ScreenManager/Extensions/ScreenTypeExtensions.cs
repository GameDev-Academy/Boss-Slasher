namespace ScreenManager.Extensions
{
    public static class ScreenContext
    {
        public static object Wrap(params object[] args)
        {
            if (args.Length == 1)
            {
                return args[0];
            }

            return args;
        }

        public static T Get<T>(object context) where T : class
        {
            if (context is T result)
            {
                return result;
            }

            if (context is object[] contextObjects)
            {
                foreach (var contextObject in contextObjects)
                {
                    result = contextObject as T;
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}