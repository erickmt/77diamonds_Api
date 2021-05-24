using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DiamondApi.Data;
using DiamondApi.Entities;
using DiamondApi.Models;

namespace DiamondApi.Controllers
{
    public class ItemPhotoPropertySetsController : ApiController
    {
        private DiamondContext db = new DiamondContext();

        public IQueryable<ItemPhotoPropertySet> GetItemPhotoPropertySet()
        {
            return db.ItemPhotoPropertySet;
        }

        [ResponseType(typeof(ItemPhotoPropertySet))]
        public IHttpActionResult GetItemPhotoPropertySet(string nameProperty)
        {
            var itemPhotoPropertySet =
                db.ItemPhotoPropertySet.Where(x => x.Properties.Name == nameProperty && x.ItemPhotos.IsActive)
                .Select(x=> new ItemPhotoProperty {
                            ItemId = x.ItemPhotos.ItemId,
                            Value = x.Value
                        }).Distinct()
                .ToList();

            if (itemPhotoPropertySet == null)
            {
                return NotFound();
            }

            return Ok(itemPhotoPropertySet);
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