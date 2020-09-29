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

        public Guid SagaId { get; set; }

        public Guid SagaActionId { get; set; }

        [ForeignKey(nameof(SagaActionId))]
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