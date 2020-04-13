using System.Net;

namespace EquipmentRental.BLL.Models.Response
{
    public class ServiceResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}