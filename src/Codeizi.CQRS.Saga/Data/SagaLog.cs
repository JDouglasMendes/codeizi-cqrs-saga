using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaLog")]
    public class SagaLog
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(SagaInfo))]
        public Guid SagaId { get; set; }

        public SagaInfo SagaInfo { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Ended { get; set; }

        [Required]
        public string DataLog { get; set; }
    }
}