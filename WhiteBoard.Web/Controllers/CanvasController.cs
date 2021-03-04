using System.Linq;
using System.Reflection;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using WhiteBoard.Web.Models;

namespace WhiteBoard.Web.Controllers
{
    public class CanvasController : Controller
    {
        private readonly string _projectName = "WhiteBoard.db";

        [HttpGet]
        [Route("/canvas")]
        public IActionResult GetCanvas()
        {
            using(var db = new LiteDatabase(_projectName))
            {
                var collection = db.GetCollection<Canvas>(nameof(Canvas));

                var canvas = collection.FindAll().FirstOrDefault();

                return Ok(canvas?.Data);
            }
        }

        [HttpPost]
        [Route("/canvas")]
        public IActionResult PostCanvas([FromBody]Canvas canvas)
        {
            using(var db = new LiteDatabase(_projectName))
            {
                var collection = db.GetCollection<Canvas>(nameof(Canvas));

                collection.DeleteAll();
                collection.Insert(canvas);

                return Ok();
            }
        }
    }
}