using System.ComponentModel.DataAnnotations.Schema;

namespace TaskRequestDem.Model
{
    public class Request
    {
        public int RequestId { get; set; }
        public int RequstNumber { get; set; }
        public string Equipment { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client client { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status status { get; set; }

        public int? ExecutorId { get; set; }
        [ForeignKey("ExecutorId")]
        public Executor executor { get; set; }

    }
}
