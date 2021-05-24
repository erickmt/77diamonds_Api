namespace DiamondApi.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ItemPhotos
    {
        public ItemPhotos()
        {
            ItemPhotoPropertySet = new HashSet<ItemPhotoPropertySet>();
        }

        public int Id { get; set; }

        public int? ItemId { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        public string FileName { get; set; }
        
        [NotMapped]
        public string File { get; set; }

        public int? Position { get; set; }

        [StringLength(500)]
        public string Alt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemPhotoPropertySet> ItemPhotoPropertySet { get; set; }

        public virtual Items Items { get; set; }

        public virtual Types Types { get; set; }
    }
}
