using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Models
{
    public class Order
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string OrderId { get; set; }

        [BsonElement("customer_id"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int CustomerId { get; set; }
        [BsonElement("ordered_on"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime OrderedOn { get; set; }

        [BsonElement("ordered_details")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
