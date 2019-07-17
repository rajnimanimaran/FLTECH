using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataModel
{
    public class MachineryVariablesCombinations
    {
        public MachineryVariablesCombinations()
        {
            this.MachCombinations = new List<Dictionary<string, string>>();
        }
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public int MachTypeId { get; set; }
        public int MachSubCategoryId { get; set; }
        public int COM_Id { get; set; }
        public string Code { get; set; }
        public IEnumerable<IDictionary<string, string>> MachCombinations { get; set; }
    }
}
