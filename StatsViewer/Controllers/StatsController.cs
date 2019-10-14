using System;
using System.Data;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Npgsql;
using StatsViewer.Models;
using StatsXmlProcessor.Extensions;
using StatsXmlProcessor.Models;

namespace StatsViewer.Controllers
{
    public class StatsController : Controller
    {
        private IDbConnection connection;

        private string statsQuery = $"SELECT  {typeof(User).GetColumnNamesJoined("u")}, {typeof(PostStats).GetColumnNamesJoined("ps")}, {typeof(CommentStats).GetColumnNamesJoined("cs")},  {typeof(Badge).GetColumnNamesJoined("b")} from users u left join post_stats s on u.account_id = ps.owner_user_id and u.site_name = ps.site_name  left join comment_stats cs on u.account_id = cs.user_id and u.site_name = cs.site_name left join badge b on u.account_id = b.user_id and u.site_name = b.site_name where u.account_id = @userId and u.site_name = @siteName";
        public StatsController(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string candidateUri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var statsRequestProcess = new StatsRequestProcess(new StatsRequest(candidateUri));
            var stats = connection.Query<StatsViewModel>(statsQuery,
                new {userId = statsRequestProcess.UserId, siteName = statsRequestProcess.SiteName});
            return View(stats);

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
