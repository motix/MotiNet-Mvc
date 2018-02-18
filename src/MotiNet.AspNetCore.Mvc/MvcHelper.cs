using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MotiNet.AspNetCore.Mvc
{
    public class MvcHelper
    {
        public string BuildTitle<TModel>(ViewDataDictionary<TModel> viewData, string siteName)
            => viewData["Title"] == null || string.IsNullOrWhiteSpace((string)viewData["Title"]) || (string)viewData["Title"] == siteName ? siteName : $"{viewData["Title"]} - {siteName}";

        public void SetActiveMenuItem<TModel>(ViewDataDictionary<TModel> viewData, string activeMenuItem)
        {
            viewData["ActiveMenuItem"] = activeMenuItem;
        }

        public bool IsMenuItemActive<TModel>(ViewDataDictionary<TModel> viewData, string menuItem)
            => (string)viewData["ActiveMenuItem"] == menuItem;

        public string ActiveMenuItem<TModel>(ViewDataDictionary<TModel> viewData, string menuItem)
            => IsMenuItemActive(viewData, menuItem) ? "active" : null;

        public void SetActiveSubMenuItem<TModel>(ViewDataDictionary<TModel> viewData, string activeSubMenuItem)
        {
            viewData["ActiveSubMenuItem"] = activeSubMenuItem;
        }

        public bool IsSubMenuItemActive<TModel>(ViewDataDictionary<TModel> viewData, string subMenuItem)
            => (string)viewData["ActiveSubMenuItem"] == subMenuItem;

        public string ActiveSubMenuItem<TModel>(ViewDataDictionary<TModel> viewData, string subMenuItem)
            => IsSubMenuItemActive(viewData, subMenuItem) ? "active" : null;
    }
}
