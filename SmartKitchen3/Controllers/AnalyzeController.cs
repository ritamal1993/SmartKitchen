using LINQtoCSV;
using SmartKitchen3.Models;
using SmartKitchen3.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SmartKitchen3.Models.RecipeEvent;
using static SmartKitchen3.Models.UserEvent;

namespace SmartKitchen3.Controllers
{
    [SmartKitchenAuthorizeAttribute(AdminOnly = "true")]
    public class AnalyzeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Analyze
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult BubbleGraphData()
        {
            string pathToFiles = Server.MapPath("/") + "PopularRecipes.csv";

            CsvContext cc = new CsvContext();

            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', // delimited
                FirstLineHasColumnNames = true, // column names in first record
                FileCultureName = "en-US" // use formats used in The Netherlands
            };

            var PopularRecipes = GetPopularRecipes();

            cc.Write(
                PopularRecipes,
                pathToFiles,
                outputFileDescription);

            return File(pathToFiles, "text/csv");
        }

        public ActionResult LineGraphData()
        {
            string pathToFiles = Server.MapPath("/") + "LoginTrend.csv";

            CsvContext cc = new CsvContext();

            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', // delimited
                FirstLineHasColumnNames = true, // column names in first record
                FileCultureName = "en-US" // use formats used in The Netherlands
            };

            var LoginTrend = GetLoginTrend();

            cc.Write(
                LoginTrend,
                pathToFiles,
                outputFileDescription);

            return File(pathToFiles, "text/csv");
        }

        List<PopularRecipes> GetPopularRecipes()
        {
            var startDate = DateTime.Now.AddDays(-360);
            var eventType = RecipeEventType.Click;

            var query = db.RecipeEvent.Join(db.Recipes,
                              re => re.RecipeId,
                              r => r.RecipeId,
                              (re, r) => new { RecipeEvent = re, Recipes = r })

                .Where(re_r => re_r.RecipeEvent.Date >= startDate &&
                    re_r.RecipeEvent.Type == eventType)

                .GroupBy(c => new { c.Recipes.RecipeId, c.Recipes.Title, c.Recipes.CategoryId, c.RecipeEvent.Type })

                .Select(re_r => new PopularRecipes
                {
                    RecipeId = re_r.Key.RecipeId,
                    RecipeTitle = re_r.Key.Title.ToString(),
                    CategoryId = re_r.Key.CategoryId,
                    Type = re_r.Key.Type.ToString(),
                    Value = re_r.Count()
                });

            return query.ToList();
        }

        List<LoginTrend> GetLoginTrend()
        {
            var startDate = DateTime.Now.AddDays(-360);
            var eventType = UserEventType.Login;

            var query = db.UserEvent
                .Where(ue => ue.Date >= startDate &&
                    ue.Type == eventType)

                .GroupBy(c => new { V = DbFunctions.CreateDateTime(c.Date.Year, c.Date.Month, c.Date.Day, 0, 0, 0) })

                .Select(ue => new LoginTrend
                {
                    Date = ue.Key.V,
                    Value = ue.Count()
                });

            return query.ToList();
        }
    }

    class PopularRecipes
    {
        [CsvColumn(FieldIndex = 1)]
        public int RecipeId { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string RecipeTitle { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public int CategoryId { get; set; }

        [CsvColumn(Name = "EventType", FieldIndex = 4)]
        public string Type { get; set; }

        [CsvColumn(FieldIndex = 5, CanBeNull = false)]
        public decimal Value { get; set; }
    }

    class LoginTrend
    {
        [CsvColumn(Name = "date", FieldIndex = 1)]
        public DateTime? Date { get; set; }

        [CsvColumn(Name = "close", FieldIndex = 3, CanBeNull = false)]
        public decimal Value { get; set; }
    }
}