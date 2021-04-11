using FullCalendar.AjaxExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalendar.AjaxExample.Controllers
{
    public class HomeController : Controller
    {
        private static List<Event> Events;
        public HomeController()
        {
            Events = new List<Event>();

            Events.Add(new Event
            {
                Title = $"Toplantı",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                AllDay = false
            });
            Events.Add(new Event
            {
                Title = $"Yemek",
                Start = DateTime.Now.AddDays(5),
                AllDay = true
            });
            Events.Add(new Event
            {
                Title = $"Toplantı",
                Start = DateTime.Now.AddDays(10),
                End = DateTime.Now.AddDays(10).AddHours(1),
                AllDay = false
            });
            Events.Add(new Event
            {
                Title = $"İş Görüşmesi",
                Start = DateTime.Now.AddMonths(1).AddDays(1),
                End = DateTime.Now.AddMonths(1).AddDays(1).AddHours(1),
                AllDay = false
            });
            Events.Add(new Event
            {
                Title = $"Şirket Etkinliği",
                Start = DateTime.Now.AddMonths(1).AddDays(10),
                AllDay = true
            });
        }

        public IActionResult Index()
        {
            var titleList = Events.GroupBy(x => x.Title).Select(x => x.Key).ToList();
            ViewBag.TitleList = titleList;
            return View();
        }

        [HttpPost]
        public IActionResult GetData()
        {
            var form = HttpContext.Request.Form;
            var startDate = DateTime.Parse(form["start"]);
            var endDate = DateTime.Parse(form["end"]);
            var title = form["title"].ToString();

            List<Event> filteredList;
            if (string.IsNullOrEmpty(title)) //Title değeri seçilmediyse bütün eventleri listeleyelim.
            {
                filteredList = Events.Where(x => startDate <= x.Start && x.End <= endDate).ToList();
            }
            else //Title değeri seçilmiş ise filtreye ekleyelim.
            {
                filteredList = Events.Where(x => x.Title == title && startDate <= x.Start && x.End <= endDate).ToList();
            }

            return Json(filteredList);
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
