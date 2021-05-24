namespace DiamondApi.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Properties
    {
        public Properties()
        {
            ItemPhotoPropertySet = new HashSet<ItemPhotoPropertySet>();
            TypePropertySet = new HashSet<TypePropertySet>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<ItemPhotoPropertySet> ItemPhotoPropertySet { get; set; }

        public virtual ICollection<TypePropertySet> TypePropertySet { get; set; }
    }
}
