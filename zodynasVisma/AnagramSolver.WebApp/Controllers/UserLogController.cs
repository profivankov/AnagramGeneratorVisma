using AnagramSolver.Contracts;
using AnagramSolver.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class UserLogController : Controller
    {
        private IUserLogRepository userLogRepository;

        public UserLogController(IUserLogRepository userLogRepository)
        {
            this.userLogRepository = userLogRepository;
        }
        [Route("UserLog/Index/")]
        public IActionResult Index()
        {
            var userLogList = userLogRepository.GetUserLog(HttpContext.Connection.RemoteIpAddress.ToString());
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