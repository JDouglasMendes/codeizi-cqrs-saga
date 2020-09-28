using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaInfo")]
    public class SagaInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}