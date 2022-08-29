using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom18WebBanHoa.Models
{
    [Serializable]
    public class GioHang
    {
        public San_Pham San_Pham { get; set; }
        public int Quantity { get; set; }

    }
}