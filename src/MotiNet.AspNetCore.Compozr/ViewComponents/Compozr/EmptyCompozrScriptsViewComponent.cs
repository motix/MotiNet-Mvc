﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MotiNet.AspNetCore.Mvc.ViewComponents.Compozr
{
    public class EmptyCompozrScriptsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.FromResult(0);
            return View();
        }
    }
}
