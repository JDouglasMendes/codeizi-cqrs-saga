using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaScheduling")]
    public class SagaScheduling
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(SagaInfo))]
        public Guid SagaId { get; set; }
        public SagaInfo SagaInfo { get; set; }

        [ForeignKey(nameof(SagaAction))]
        public Guid SagaActionId { get; set; }
        public SagaAction SagaAction { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string TypeState { get; set; }
        [Required]
        public bool Cancel { get; set; }
    }
}