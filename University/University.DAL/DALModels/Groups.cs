using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.DAL
{
    public class Groups
    {
        [Key]
        public int Group_ID { get; set; }
        public string Name { get; set; }
        public int Course_ID { get; set; }

        [ForeignKey("Course_ID")]
        public Cours courses { get; set; }

    }
}
