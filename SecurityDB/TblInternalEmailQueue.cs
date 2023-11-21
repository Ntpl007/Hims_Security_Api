using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblInternalEmailQueue
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string EmailId { get; set; }
        public string EmailContent { get; set; }
        public string RabbitMqStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string QueueName { get; set; }
        public DateTime? RabbitMqDateTime { get; set; }
        public DateTime? DeliveredDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
