namespace DiamondApi.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Types
    {
        public Types()
        {
            ItemPhotos = new HashSet<ItemPhotos>();
            TypePropertySet = new HashSet<TypePropertySet>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<ItemPhotos> ItemPhotos { get; set; }

        public virtual ICollection<TypePropertySet> TypePropertySet { get; set; }
    }
}
