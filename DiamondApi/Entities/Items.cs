namespace DiamondApi.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Items
    {
        public Items()
        {
            ItemPhotos = new HashSet<ItemPhotos>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ItemPhotos> ItemPhotos { get; set; }
    }
}
