using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DiamondApi.Data;
using DiamondApi.Entities;

namespace DiamondApi.Controllers
{
    public class ItemsController : ApiController
    {
        private DiamondContext db = new DiamondContext();

        public IEnumerable<Items> GetItems()
        {
           return db.Items.ToList();
        }

        [ResponseType(typeof(Items))]
        public IHttpActionResult GetItems(int id)
        {
            Items items = db.Items.Find(id);
            if (items == null)
                return NotFound();

            return Ok(items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}