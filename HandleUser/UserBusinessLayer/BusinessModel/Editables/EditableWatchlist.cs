using Csla;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableWatchlist: BusinessBase<EditableWatchlist>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(IdProperty, "You need to give a id"));
            BusinessRules.AddRule(new ValidUser());
        }

        private class ValidUser : Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var wl = (EditableWatchlist)context.Target;
                using (var contxt = new UserDataLayer.DbContext.UserDbContext())
                {
                    var user = contxt.User.Include("Watchlists").Where(x => x.Id == wl.UserId).FirstOrDefault();
                    if (user == null)
                        context.AddErrorResult("There was no user found");
                    else
                    {
                        foreach (var item in user.Watchlists)
                        {
                            if (item.WatchlistId == wl.Id)
                            {
                                context.AddErrorResult("This user already contains the watchlist");
                            }
                        }
                    }
                }
            }
        }

        public static void DeleteWatchlistById(int id)
        {
            DataPortal.Delete<EditableWatchlist>(id);
        }

        public static EditableWatchlist NewEditableWatchlist(WatchlistCriteria criteria)
        {
            return DataPortal.Create<EditableWatchlist>(criteria);
        }

        public static EditableWatchlist ConvertChild(Watchlist wl)
        {
            return DataPortal.FetchChild<EditableWatchlist>(wl);
        }

        protected void DataPortal_Create(WatchlistCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Id = criteria.Id;
                this.UserId = criteria.UserId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            Watchlist wl = new Watchlist();
            wl.WatchlistId = this.Id;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Include("Watchlists").Where(x => x.Id == this.UserId).FirstOrDefault();
                user.Watchlists.Add(wl);
                context.Watchlist.Add(wl);
                context.SaveChanges();
            }
            this.Id = wl.WatchlistId;
        }

        private void Child_Fetch(Watchlist wl)
        {
            this.Id = wl.WatchlistId;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var wl = context.Watchlist.Where(x => x.WatchlistId == id).FirstOrDefault();
                if (wl == null)
                    throw new Exception("There was no watchlist found");
                context.Watchlist.Remove(wl);
                context.SaveChanges();
            }
        }
    }
}
