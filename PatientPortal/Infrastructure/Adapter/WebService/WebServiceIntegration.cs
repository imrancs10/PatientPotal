using PatientPortal.HISWebReference;
using PatientPortal.Infrastructure.Utility;
using PatientPortal.Models;
using PatientPortal.PateintInfoService;

namespace PatientPortal.Infrastructure.Adapter.WebService
{
    public class WebServiceIntegration
    {
        public HISPatientInfoModel GetPatientInfoBYCRNumber(string crNumber)
        {
            GetPatientDetails service = new GetPatientDetails();
            var result = service.getPatientDetails(crNumber);
            if (result.ToLower().Contains("no record"))
                return null;
            Serializer serilizer = new Serializer();
            result = result.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "");
            return serilizer.Deserialize<HISPatientInfoModel>(result, "Table1");
        }

        public string GetPatientInfoinsert(HISPatientInfoInsertModel insertModel)
        {
            Serializer serilizer = new Serializer();
            string xml = serilizer.SerializeToXML(insertModel);
            GetPatient_Info_insert pateintInfoService = new GetPatient_Info_insert();
            return pateintInfoService.GetPatientInfoinsert(xml);
        }
    }
}