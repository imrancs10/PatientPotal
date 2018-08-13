using PatientPortal.HISWebReference;
using PatientPortal.Infrastructure.Utility;
using PatientPortal.Models;

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
    }
}