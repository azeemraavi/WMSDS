using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities
{
    public class WmsdsResponse<T>
    {
        public EnumStatus ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T DataObject { get; set; }
        public List<T> Collections { get; set; }
    }
    public enum EnumStatus
    {
        Success = 1000,
        AlreadyExist = 1001,        
        Failed = 1002,
        Error = 1003,
        InternalServer = 1004,
        ValidationFailed = 1005,
        NoSeatVacant = 1006,
        NotFound = 1007,
        RequiredField = 1008,
        EmptyObject=1009
    }
}
