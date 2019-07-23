using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class UserLogController : Controller
    {
        private IUserLogService _userLogService;

        public UserLogController(IUserLogService userLogService)
        {
            _userLogService = userLogService;
        }
        [Route("UserLog/Index/")]
        public IActionResult Index()
        {
            var userLogList = _userLogService.GetUserLog(HttpContext.Connection.RemoteIpAddress.ToString());

            List<UserLogViewModel> userLogViewList = new List<UserLogViewModel>();
            foreach (var userLog in userLogList)
            {
                var userLogView = new UserLogViewModel { IPAdress = userLog.IPAdress, AnagramList = userLog.AnagramWord, SearchedWord = userLog.SearchedWord, SearchTime = userLog.SearchTime };
                userLogViewList.Add(userLogView);
            }
            return View(userLogViewList);
        }
    }
}