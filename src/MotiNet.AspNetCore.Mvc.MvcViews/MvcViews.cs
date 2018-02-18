using System.Reflection;

namespace MotiNet.AspNetCore.Mvc
{
    public static class MvcViews
    {
        public static Assembly Assembly => typeof(MvcViews).GetTypeInfo().Assembly;

        public static string Namespace => typeof(MvcViews).GetTypeInfo().Namespace;
    }
}
