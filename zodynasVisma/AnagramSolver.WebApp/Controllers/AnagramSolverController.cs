using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Repositories;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AnagramSolver.WebApp.Services;
using System.Linq;
using AnagramSolver.EF.CodeFirst.Contracts;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramSolverController : Controller
    {
        
        private IUserInfoRepository userInfoRepository;
        private ICacheService _cacheService;
        private IUserLogService _userLogService;

        public AnagramSolverController(ICacheService cacheService, IUserInfoRepository userInfoRepository, IUserLogService userLogService)
        {
            _cacheService = cacheService;
            _userLogService = userLogService;
            this.userInfoRepository = userInfoRepository;
        }

        public IActionResult Index(int searchesLeft)
        {
            return View(new AnagramViewModel { WordList = new List<string>(), searchesLeft = searchesLeft });
        }

        [Route("AnagramSolver/Index/")]
        public IActionResult Index(AnagramViewModel request)
        {
            var searchestLeft = userInfoRepository.GetUserInfo(); // get amount of searches left or create new user
            if (request.Input == null || request.Input.Length == 0 || searchestLeft <= 0)
            {
                return Index(searchestLeft);
            }
            Response.Cookies.Append("searchedWord", request.Input); // add cookie

            var resultList = new AnagramViewModel { WordList = _cacheService.GetMultiple(request.Input) }; //get anagrams
            userInfoRepository.UpdateSearchAmount(); // add +1 search
            resultList.searchesLeft = userInfoRepository.GetUserInfo(); // get updated amount for display in view

            _userLogService.AddToUserLog(HttpContext.Connection.RemoteIpAddress.ToString(), request.Input);
            return View(resultList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
