using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Artshop.Website.Controllers
{
    public class BaseController : Controller
    {
        public readonly DatabaseConnection db;
        public BaseController()
        {
            db = new DatabaseConnection(ConnectionType.Database, WebConfigurationManager.ConnectionStrings["LocalDB"].ToString());
        }
        
            protected bool ModelIsValid(List<ValidationResult> listModel)
            {
                var message = string.Empty;
                var result = listModel != null && listModel.Count > 0;
                if (result)
                {
                    foreach (var item in listModel)
                        message += string.Format("{0}\r\n", item.ErrorMessage);
                    ViewBag.MessageDanger = message;
                }

                return result;
            }

            protected void CheckAuditPattern(BaseClass model, bool created = false)
            {
                string userId = TryGetUserId();
                if (created)
                {
                    model.CreatedOn = DateTime.Now;
                    model.CreatedBy = userId;
                }
                model.ChangedOn = DateTime.Now;
                model.ChangedBy = userId;
            }

            protected virtual string TryGetUserId()
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                string userId = null;
                try
                {
                    userId = User.Identity.Name;
                    if (userId != null)
                        return userId;
                }
                catch { /* no action */}

                return userId;
            }
        
    }
}