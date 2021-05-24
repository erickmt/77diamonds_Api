using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DiamondApi.Data;
using DiamondApi.Entities;
using DiamondApi.Models;

namespace DiamondApi.Controllers
{
    public class ItemPhotosController : ApiController
    {
        private DiamondContext db = new DiamondContext();

        public IList<ItemPhotosCollection> GetItemPhotos()
        {
            return db.ItemPhotos.Where(itemPhoto => itemPhoto.IsActive).Select(itemPhoto => 
                new ItemPhotosCollection
                {
                    Alt = itemPhoto.Alt,
                    FileName = itemPhoto.FileName,
                    Id = itemPhoto.Id,
                    ItemId = itemPhoto.ItemId,
                    Position = itemPhoto.Position,
                    TypeId = itemPhoto.TypeId,
                    ItemPhotoPropertys = itemPhoto.ItemPhotoPropertySet.Select(property => new ItemPhotoProperty
                    {
                        Id = property.Id,
                        ItemId = property.ItemPhotos.ItemId,
                        ItemPhotoId = property.ItemPhotoId,
                        PropertyId = property.PropertyId,
                        Value = property.Value
                    }).ToList()
                }
            ).ToList();
        }

        [ResponseType(typeof(ItemPhotos))]
        public IHttpActionResult GetItemPhotos(int itemId, string metal, string shape)
        {
            List<ItemPhotos> itemPhotos = db.ItemPhotos
                                        .Where(photo =>
                                            photo.IsActive &&
                                            photo.ItemId == itemId &&
                                            photo.TypeId == 1 &&
                                            photo.ItemPhotoPropertySet.Any(property => property.Value == metal) &&
                                            photo.ItemPhotoPropertySet.Any(property => property.Value == shape)).ToList();

            return Ok(itemPhotos);
        }


        [ResponseType(typeof(ItemPhotos))]
        public IHttpActionResult PostItemPhotos(ItemPhotos itemPhotos)
        {
            itemPhotos.FileName = Guid.NewGuid().ToString();
            string filePath = $@"{AppContext.BaseDirectory}Assets\Images\{itemPhotos.FileName}.jpeg";
            try
            {
                if(itemPhotos.ItemPhotoPropertySet.Count < 2)
                    return BadRequest("Metal and shape are requireds");
                
                if(string.IsNullOrEmpty(itemPhotos.File))
                    return BadRequest("A file is required");

                string metal = itemPhotos.ItemPhotoPropertySet.Where(x=>x.PropertyId == 1).FirstOrDefault().Value;
                string shape = itemPhotos.ItemPhotoPropertySet.Where(x => x.PropertyId == 2).FirstOrDefault().Value;

                if (itemPhotos.TypeId == 2 && db.ItemPhotos.Any(p =>
                            p.IsActive &&
                            p.TypeId == 2 &&
                            p.ItemId == itemPhotos.ItemId &&
                            p.ItemPhotoPropertySet.Any(x => x.Value == metal) &&
                            p.ItemPhotoPropertySet.Any(x => x.Value == shape))
                    )
                    throw new Exception("Thumb alredy registered for this item, shape and metal. Delete it befero registering a new one.");

                File.WriteAllBytes(filePath, Convert.FromBase64String(itemPhotos.File));

                foreach (var item in itemPhotos.ItemPhotoPropertySet)
                    itemPhotos.Alt += string.IsNullOrWhiteSpace(itemPhotos.Alt) ? item.Value : " - " + item.Value;

                db.ItemPhotos.Add(itemPhotos);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ResponseType(typeof(ItemPhotos))]
        public IHttpActionResult DeleteItemPhotos(int id)
        {
            ItemPhotos itemPhotos = db.ItemPhotos.Find(id);
            if (itemPhotos == null)
            {
                return NotFound();
            }

            db.ItemPhotos.Remove(itemPhotos);
            db.SaveChanges();

            return Ok(itemPhotos);
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