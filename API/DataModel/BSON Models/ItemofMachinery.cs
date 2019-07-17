using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;


namespace DataModel
{
    public class ItemofMachinery : BaseModel
    {

        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public int MachTypeId { get; set; }
        public int MachSubCategoryId { get; set; }
        public int COM_Id { get; set; }
        public int UOM_Id { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SpecType { get; set; }

        public IDictionary<string, string> Properties { get; set; }
        public int Status { get; set; }
    }
}
