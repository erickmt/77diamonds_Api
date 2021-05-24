using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DiamondApi.Entities
{
    [Table("TypePropertySet")]
    public partial class TypePropertySet
    {
        public int Id { get; set; }

        public int MediaTypeId { get; set; }

        public int PropertyId { get; set; }

        public virtual Properties Properties { get; set; }

        public virtual Types Types { get; set; }
    }
}
