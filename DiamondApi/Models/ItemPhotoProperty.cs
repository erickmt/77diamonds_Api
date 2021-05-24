namespace DiamondApi.Models
{
    public class ItemPhotoProperty
    {
        public int Id { get; set; }

        public int? ItemId { get; set; }

        public int ItemPhotoId { get; set; }

        public int PropertyId { get; set; }

        public string Value { get; set; }
    }
}