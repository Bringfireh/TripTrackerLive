﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TripTracker.BackService.Controllers
{
    public class TripsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}