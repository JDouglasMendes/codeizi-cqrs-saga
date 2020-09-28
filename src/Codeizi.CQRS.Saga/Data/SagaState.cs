using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeizi.CQRS.Saga.Data
{
    [Table("CODEIZISagaState")]
    public class SagaState
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(SagaInfo))]
        public Guid SagaInfoId { get; set; }
        public SagaInfo SagaInfo { get; set; }

        public string ExtendedData { get; set; }

        [NotMapped]
        public JObject JSONExtendedData
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(ExtendedData) ? "{}" : ExtendedData);
            }
            set
            {
                ExtendedData = value.ToString();
            }
        }

    }
}