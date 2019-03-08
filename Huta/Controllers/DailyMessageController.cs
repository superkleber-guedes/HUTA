using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Huta.Prop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Huta.Controllers
{
    public class DailyMessageController : Controller
    {
        public IActionResult Index()
        {

            var items = LoadJson();
            return View("~/Views/Prop/DailyMessageList.cshtml", items);
        }

        public IActionResult Edit(DateTime date)
        {
            var items = LoadJson();

            var specificDate = items.Messages.FirstOrDefault(x => x.Date == date);

            if (specificDate == null)
            {
                return View("~/Views/Prop/NotFound.cshtml");
            }
            else
            {
                return View("~/Views/Prop/DailyMessageEdit.cshtml", specificDate);
            }
        }

        public IActionResult Save(DailyMessage daily)
        {
            try
            {
                var items = LoadJson();
                var specificDate = items.Messages.First(x => x.Date == daily.Date.Date);

                daily.Date = specificDate.Date;

                items.Messages.Remove(specificDate);
                items.Messages.Add(daily);

                var result = SaveJson(items);

                return View("~/Views/Prop/DailyMessageList.cshtml", result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Prop/NotFound.cshtml");
            }
        }

        public Huta.Prop.DailyMessages LoadJson()
        {
            using (StreamReader r = new StreamReader("wwwroot/Data/Prop/prop.json"))
            {
                string json = r.ReadToEnd();
                Huta.Prop.DailyMessages items = JsonConvert.DeserializeObject<Huta.Prop.DailyMessages>(json);
                items.Messages = items.Messages.OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public Huta.Prop.DailyMessages SaveJson(Huta.Prop.DailyMessages data)
        {
            string json = JsonConvert.SerializeObject(data);
            System.IO.File.WriteAllText("wwwroot/Data/Prop/prop.json", json);

            return LoadJson();
        }
    }
}