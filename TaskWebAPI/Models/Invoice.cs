using System.ComponentModel.DataAnnotations;

namespace TaskWebAPI.Models
{
    public class Invoice
    {
        //beginDate, endDate, sum, serviceName, clientId, payDueDate
        [Key]
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Sum { get; set; }
        public string ServiceName { get; set; }
        public int ClientId { get; set; }
        public DateTime PayDueDate { get; set; }


    }
}
