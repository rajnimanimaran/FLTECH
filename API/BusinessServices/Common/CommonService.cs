using DataModel;
using DataModel.UnitOfWork;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices
{
    class CommonService
    {
        private static IUnitOfWork _unitOfWork;
        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Returns the list of companies from 'allcompany' list which are subcompanies for given 'companyId'
        /// </summary>
        /// <param name="allCompanies"> List of all companies </param>
        /// <param name="companyId"> company id to start with </param>
        /// <returns> Sub companies List with there sub companies </returns>
        public static List<Company> getSubCompanyList(IEnumerable<Company> allCompanies, int companyId)
        {
            var subCompanies = allCompanies.Where(c => c.ParentCompany == companyId).ToList();

            var tempList = new List<Company>();

            // Get sub companies for each sub companies
            subCompanies.ForEach(c => { tempList.AddRange(getSubCompanyList(allCompanies, c.CompanyId)); });
            subCompanies.AddRange(tempList);

            return subCompanies;
        }

        /// <summary>
        /// Returns the list of companies from 'allcompanies' which the given company is reporting
        /// and its reporting company starts from 'startCompanyId' end as 'endComapnyId'
        /// </summary>
        /// <param name="allCompanies"> List of all companies </param>
        /// <param name="startCompanyId"> companyId to start with </param>
        /// <param name="endCompanyId"> companyId that itration ends at </param>
        /// <returns></returns>
        public static List<Company> getParentsList(IEnumerable<Company> allCompanies, int startCompanyId, int endCompanyId)
        {
            var companyList = new List<Company>();

            while (startCompanyId >= endCompanyId && startCompanyId != 0)
            {
                var parentCompany = allCompanies.First(c => c.CompanyId == startCompanyId);
                companyList.Add(parentCompany);
                startCompanyId = parentCompany.ParentCompany;
            }

            return companyList;
        }

        public int getIdFromSeqTable(string tableName)
        {
            var currentId = _unitOfWork.MongoRepository.GetCollection<Sequence>().AsQueryable()
                                    .Where(c => c.TableName == tableName).Select(c => c.CurrentId).First();

            return currentId;
        }

        public void updateIdInSeqTable(string tableName)
        {
            var filter = Builders<Sequence>.Filter.Where(c => c.TableName == tableName);
            var update = Builders<Sequence>.Update.Inc(c => c.CurrentId, 1);

            var res = _unitOfWork.MongoRepository.UpdateOne(filter, update);
        }

       
    }
}
