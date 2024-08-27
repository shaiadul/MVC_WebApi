using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace testing.Models
{

    public enum StockStatus
    {
        InStock,
        OutOfStock,
        LowStock
    }

    public class Stock
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required int Price { get; set; }
        public required int QuantityInStock { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required StockStatus StockStatus { get; set; } = StockStatus.InStock;
        public required int Quantity { get; set; }
        public List<Comments>? Comments { get; set; } = new List<Comments>();
        public required DateTime DateAdded { get; set; } = DateTime.Now;
    }
}