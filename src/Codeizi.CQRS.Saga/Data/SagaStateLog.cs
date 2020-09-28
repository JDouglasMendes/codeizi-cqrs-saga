using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaStateLog")]
    public class SagaStateLog
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(SagaInfo))]
        public Guid SagaId { get; set; }
        public SagaInfo SagaInfo { get; set; }

        [ForeignKey(nameof(SagaAction))]
        public Guid ActionId { get; set; }
        public SagaAction SagaAction { get; set; }

        [Required]
        public string InitialState { get; set; }
        [Required]
        public string FinshedState { get; set; }
    }
}