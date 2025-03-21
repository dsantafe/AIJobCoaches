namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        [Required, MaxLength(100)]
        public string ItemName { get; set; }
    }
}
