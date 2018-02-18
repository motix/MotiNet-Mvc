using System.Reflection;

namespace MotiNet.AspNetCore.Mvc
{
    public static class RazorPagesViews
    {
        public static Assembly Assembly => typeof(RazorPagesViews).GetTypeInfo().Assembly;

        public static string Namespace => typeof(RazorPagesViews).GetTypeInfo().Namespace;
    }
}
