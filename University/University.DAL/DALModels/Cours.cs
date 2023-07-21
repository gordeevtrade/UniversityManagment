using System.ComponentModel.DataAnnotations;

namespace University.DAL
{
    public class Cours
    {
        [Key]
        public int Course_ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
