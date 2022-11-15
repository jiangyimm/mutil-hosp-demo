using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_hosp_demo.Entities
{
    [Table("ext_dept", Schema = "mrqcs")]
    public class ExtDept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 科室代码（必须）
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
        /// <summary>
        /// 科室名称（必须）
        /// </summary>
        [Required]
        public string DeptName { get; set; }
        /// <summary>
        /// 院区代码
        /// </summary>
        public string HospCode { get; set; }
    }
}