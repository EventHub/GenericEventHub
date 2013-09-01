using SportsHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsHub.Infrastructure
{
    public abstract class DatabaseServices
    {
        protected static SportsHubDb _Db = new SportsHubDb();

        public void AddEvent(Event ev) 
        {
            _Db.Event.Add(ev);
            _Db.SaveChanges();
        }
        public void AddEntity<T>(T newEntity) where T : class
        {
            _Db.Entry(newEntity).State = System.Data.EntityState.Added;
            _Db.SaveChanges();
        }

        public void UpdateEntity<T>(T newEntity) where T : class
        {
            _Db.Entry(newEntity).State = System.Data.EntityState.Modified;
            _Db.SaveChanges();
        }

        public void DeleteEntity<T>(T newEntity) where T : class
        {
            _Db.Entry(newEntity).State = System.Data.EntityState.Deleted;
            _Db.SaveChanges();
        }

        public void DisposeContext() 
        {
            _Db.Dispose();
        }

        public void ResetContext() 
        {
            _Db = new SportsHubDb();
        }
    }
}