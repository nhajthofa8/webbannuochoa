//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhom18WebBanHoa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trang_Thai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trang_Thai()
        {
            this.Don_Hang = new HashSet<Don_Hang>();
        }
    
        public int id { get; set; }
        public string trangthai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Don_Hang> Don_Hang { get; set; }
    }
}
