
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class MaterialAttributes
    {
        public MaterialAttributes()
        {
            Attribute = new List<DataModel.Attribute>();

        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialSubCategoryId { get; set; }
        public int MaterialCategoryId { get; set; }
        public List<Attribute> Attribute { get; set; }
    }

    //public class Attribute
    //{
    //    public Attribute()
    //    {
    //        Variables = new List<string>();
    //    }
    //    public string Name { get; set; }
    //    public List<string> Variables { get; set; }
    //}


}







