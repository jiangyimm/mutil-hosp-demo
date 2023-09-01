using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_hosp_demo.Entities;

[Table("job_done", Schema = "mrqcs")]
public class JobDone
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid ID { get; set; }
    [Column("visit_id")]
    [Required]
    public string VisitId { get; set; }
    [Column("visit_type")]
    [Required]
    public byte VisitType { get; set; }
    [Column("record_id")]
    public string? RecordId { get; set; }
    [Column("create_time")]
    [Required]
    public DateTime CreateTime { get; set; }
    [Column("exec_time")]
    [Required]
    public DateTime ExecTime { get; set; }
    [Column("org_code")]
    [Required]
    public string OrgCode { get; set; }
}