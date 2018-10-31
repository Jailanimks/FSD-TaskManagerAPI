using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TaskManager.DataLayer
{
    [Table("Task")]
    public class TaskData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Column("Task")]
        [StringLength(100)]
        public string TaskName { get; set; }

        [Column("ParentTask_ID")]
        public int? ParentTaskId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        [Column("Priority")]
        public int Priority { get; set; }

        [Column("IsTaskEnd")]
        public Boolean IsTaskEnded { get; set; }

    }
}
