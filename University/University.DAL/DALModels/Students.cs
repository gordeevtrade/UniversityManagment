using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.DAL
{
    public class Students
    {
        [Key]
        public int Student_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Group_ID { get; set; }

        [ForeignKey("Group_ID ")]
        public Groups groups { get; set; }

    }
}

