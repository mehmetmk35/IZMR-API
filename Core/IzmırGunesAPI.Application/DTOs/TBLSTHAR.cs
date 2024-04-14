using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.DTOs
{
    public class TBLSTHAR
    {
        public decimal StockMovementQuantity { get; set; }
        public string StockIn_Out { get; set; }
        public DateTime Date { get; set; }
        public decimal ProductNetPrice { get; set; }
        public decimal ProductGrossPrice{ get; set; }
        public int WarehouseCode { get; set; }
    }
}
