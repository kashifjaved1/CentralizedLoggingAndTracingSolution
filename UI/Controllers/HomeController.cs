﻿using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILoggerService _loggerService;

        public HomeController(IHttpClientFactory clientFactory, ILoggerService loggerService)
        {
            _clientFactory = clientFactory;
            _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username)
        {
            _loggerService.LogInformation("MVC: HomeController.Index() called with username: " + username);

            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync($"http://localhost:5001/welcome/{username}");

            ViewData["Message"] = response;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
