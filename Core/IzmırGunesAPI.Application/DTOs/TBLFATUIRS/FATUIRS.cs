namespace IzmirGunesAPI.Application.DTOs.TBLFATUIRS
{
    public class TBLFATUIRS
    {       
        public string InvoiceNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
        
    }
}
