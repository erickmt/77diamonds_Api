using System.Collections.Generic;

namespace DiamondApi.Models
{
    public class ItemPhotosCollection
    {
        public ItemPhotosCollection()
        {
            ItemPhotoPropertys = new List<ItemPhotoProperty>();
        }
        public int Id { get; set; }

        public int? ItemId { get; set; }

        public int TypeId { get; set; }

        public string FileName { get; set; }

        public int? Position { get; set; }

        public string Alt { get; set; }

        public virtual ICollection<ItemPhotoProperty> ItemPhotoPropertys { get; set; }

    }
}