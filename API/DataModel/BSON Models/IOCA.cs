using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataModel
{
    public class IOCA : BaseModel
    {

        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public int ProjectIocaId { get; set; }
        public int TypeOfCAId { get; set; }
        public int CA_SubCategoryId { get; set; }
        public int CA_CategoryId { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int UnitId { get; set; }
        public string SpecType { get; set; }
        public int UOM_ParametersId { get; set; }
        public ContractorScope ContractorScope { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public int Status { get; set; }
    }

    public class ContractorScope
    {
        public string L_Des { get; set; }
        public string LM_Des { get; set; }
        public string LMh_Des { get; set; }
        public string LMM_Des { get; set; }
        public string LMMC_Des { get; set; }
    }
}
