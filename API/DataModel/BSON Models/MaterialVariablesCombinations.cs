using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;


namespace DataModel
{
    public class MaterialVariablesCombinations
    {
        public MaterialVariablesCombinations()
        {
            this.VariablesCombinations = new List<Dictionary<string, string>>();


        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialSubCategoryId { get; set; }
        public int MaterialCategoryId { get; set; }
        public string Code { get; set; }
        public IEnumerable<IDictionary<string, string>> VariablesCombinations { get; set; }
    }

}



