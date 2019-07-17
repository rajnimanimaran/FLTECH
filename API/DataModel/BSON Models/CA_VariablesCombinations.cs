using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataModel
{
    public class CA_VariablesCombinations
    {
        public CA_VariablesCombinations()
        {
            this.Combinations = new List<Dictionary<string, string>>();
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int TypeOfCAId { get; set; }
        public int CA_SubCategoryId { get; set; }
        public int CA_CategoryId { get; set; }

        public string Code { get; set; }

        public IEnumerable<IDictionary<string, string>> Combinations { get; set; }
    }
}
