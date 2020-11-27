using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Views
{
    public class abcViewComponent : ViewComponent
    {
        //private DbContextOptions<MyContext> db = new DbContextOptions<MyContext>();
        public async Task<IViewComponentResult> InvokeAsync(User currentUser)
        {

            return View("/Views/Shared/TopMenu.cshtml", currentUser);
        }
    }
}
