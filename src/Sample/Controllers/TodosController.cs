using System;
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Linq;
using Sample.Hubs;
using Sample.Models;

namespace Sample.Controllers
{
    [BreezeController]
    public class TodosController : ApiController {

        static readonly TimeSpan RefreshRate = TimeSpan.FromMinutes(60);
        private static readonly object Locker = new object();
        static DateTime _lastRefresh = DateTime.Now; // will first clear db at Now + "RefreshRate" 
        // static DateTime lastRefresh = DateTime.MinValue; // will clear when server starts

        readonly EFContextProvider<ToDosContext> _contextProvider =
            new EFContextProvider<ToDosContext>();

        public TodosController()
        {
            PeriodicReset();
        }

        // ~/api/todos/Metadata 
        [HttpGet]
        public string Metadata() {
            SendMessage("metadaten abgefragt");
            return _contextProvider.Metadata();
        }

        // ~/api/todos/Todos
        // ~/api/todos/Todos?$filter=IsArchived eq false&$orderby=CreatedAt 
        [HttpGet]
        public IQueryable<TodoItem> Todos() {
            SendMessage("todos abgefragt");
            return _contextProvider.Context.Todos;
        }

        // ~/api/todos/SaveChanges
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle) {
            var saveChanges = _contextProvider.SaveChanges(saveBundle);
            foreach (TodoItem item in saveChanges.Entities)
            {
                SendMessage(string.Format("{0} {1} {2}", item.Description, item.IsArchived?"archived":"", item.IsDone?"done":""));
            }
            return saveChanges;
        }

        // ~/api/todos/purge
        [HttpPost]
        public string Purge()
        {
            ToDoDatabaseInitializer.PurgeDatabase(_contextProvider.Context);
            SendMessage("database purged");
            return "purged";
        }

        // ~/api/todos/reset
        [HttpPost]
        public string Reset()
        {
            Purge();
            ToDoDatabaseInitializer.SeedDatabase(_contextProvider.Context);
            SendMessage("reset database");
            return "reset";
        }

        /// <summary>
        /// Reset the database to it's initial data state after the server has run 
        /// for "RefreshRate" minutes.
        /// </summary>
        private void PeriodicReset()
        {
            if ((DateTime.Now - _lastRefresh) <= RefreshRate) return;

            lock (Locker)
            {
                if ((DateTime.Now - _lastRefresh) > RefreshRate)
                {
                    _lastRefresh = DateTime.Now;
                    Reset();
                }
            }
        }

        public void SendMessage(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            context.Clients.All.AddMessage(message);
        }
    }
}