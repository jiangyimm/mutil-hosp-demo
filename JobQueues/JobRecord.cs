using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace multi_hosp_demo.JobQueues;

[Table("job_record", Schema = "mrqcs")]
public class JobRecord : IJobStorageRecord
{
    [Key]
    [Column("id")]
    public Guid ID { get; set; }
    [Column("queue_id")]
    public string QueueID { get; set; }
    [NotMapped]
    public object JobData { get; set; }
    [Column("j_command")]
    public JsonElement JCommand { get; set; }
    // {
    //     get
    //     {
    //         return JsonSerializer.SerializeToElement(Command);
    //     }
    //     set
    //     {
    //         Command = JsonSerializer.Deserialize<object>(value);
    //     }
    // }
    [Column("execute_after")]
    public DateTime ExecuteAfter { get; set; }
    [Column("expire_on")]
    public DateTime ExpireOn { get; set; }
    [Column("is_complete")]
    public bool IsComplete { get; set; }
}