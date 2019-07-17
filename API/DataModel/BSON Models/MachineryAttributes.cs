
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class MachineryAttributes
    {
        public MachineryAttributes()
        {
            Attribute = new List<DataModel.Attribute>();

        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int MachTypeId { get; set; }
        public int MachSubCategoryId { get; set; }
        public int COM_Id { get; set; }

        public List<Attribute> Attribute { get; set; }
    }


}