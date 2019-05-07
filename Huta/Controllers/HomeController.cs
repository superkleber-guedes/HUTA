using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Huta.Models;
using Newtonsoft.Json;
using System.IO;
using Huta.Prop;

namespace Huta.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TheList()
        {
            DailyMessages allMessages = LoadMessagesJson();
            AroundData beforeAndAfter = LoadAroundJson();

            CardList cardList = new CardList();
            cardList.Cards = new List<Card>();

            cardList.TotalCount = allMessages.Messages.Count;
            foreach (var intro in beforeAndAfter.Intro)
            {
                cardList.Cards.Add(new Card()
                {
                    HasDate = false,
                    HasIndex = false,
                    Message = intro.Message,
                    SubMessage = intro.SubMessage
                });
            }
            var index = 1;
            foreach (var message in allMessages.Messages)
            {
                cardList.Cards.Add(new Card()
                {
                    HasDate = true,
                    HasIndex = true,
                    Date = message.Date.UtcDateTime,
                    Index = index,
                    Message = message.Message,
                    SubMessage = message.SubMessage
                });
                index++;
            }
            foreach (var final in beforeAndAfter.End)
            {
                cardList.Cards.Add(new Card()
                {
                    HasDate = false,
                    HasIndex = false,
                    Message = final.Message,
                    SubMessage = final.SubMessage
                });
            }

            return View(cardList);
        }

        public DailyMessages LoadMessagesJson()
        {
            using (StreamReader r = new StreamReader("wwwroot/Data/Prop/prop.json"))
            {
                string json = r.ReadToEnd();
                DailyMessages items = JsonConvert.DeserializeObject<DailyMessages>(json);
                items.Messages = items.Messages.OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public Huta.Prop.AroundData LoadAroundJson()
        {
            using (StreamReader r = new StreamReader("wwwroot/Data/Prop/arounddata.json"))
            {
                string json = r.ReadToEnd();
                AroundData items = JsonConvert.DeserializeObject<AroundData>(json);
                return items;
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
