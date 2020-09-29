using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaAction")]
    public class SagaAction
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdSaga { get; set; }

        [Required]
        public byte Position { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string TypeState { get; set; }

        [Required]
        public StatusOperation Status { get; set; }

        public DateTime Created { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime Initiate { get; set; }
        public DateTime Ended { get; set; }
    }
}