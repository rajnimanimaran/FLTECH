using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataModel
{
    public class CA_Attributes
    {
        public CA_Attributes()
        {
            Attribute = new List<DataModel.Attribute>();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int TypeOfCAId { get; set; }
        public int CA_SubCategoryId { get; set; }
        public int CA_CategoryId { get; set; }


        public List<Attribute> Attribute { get; set; }
    }

    public class Attribute
    {
        public Attribute()
        {
            Variables = new List<string>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<string> Variables { get; set; }
    }
}
