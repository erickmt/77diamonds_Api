namespace DiamondApi.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemPhotoPropertySet")]
    public partial class ItemPhotoPropertySet
    {
        public int Id { get; set; }

        public int ItemPhotoId { get; set; }

        public int PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        public virtual Properties Properties { get; set; }

        public virtual ItemPhotos ItemPhotos { get; set; }
    }
}
