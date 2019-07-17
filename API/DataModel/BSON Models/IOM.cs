using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataModel
{
    public class IOM : BaseModel
    {
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialSubCategoryId { get; set; }
        public int MaterialCategoryId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int UnitId { get; set; }
        public string SpecType { get; set; }
        public int UnitWeight { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public int Status { get; set; }
    }
}
