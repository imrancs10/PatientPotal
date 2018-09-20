using PatientPortal.HISWebReference;
using PatientPortal.Infrastructure.Utility;
using PatientPortal.Models;
using PatientPortal.PateintInfoService;
using System.IO;
using System.Text;

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
            string text;
            var fileStream = new FileStream(@"C:\Users\sheikhimran\Desktop\HISInsert.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            GetPatient_Info_insert pateintInfoService = new GetPatient_Info_insert();
            return pateintInfoService.GetPatientInfoinsert(text);
        }
    }
}