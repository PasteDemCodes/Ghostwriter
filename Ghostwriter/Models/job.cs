//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace Ghostwriter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public job()
        {
            this.employees = new HashSet<employee>();
        }
        [DisplayName("Job ID")]
        public short job_id { get; set; }
        [DisplayName("Job Description")]
        public string job_desc { get; set; }
        [DisplayName("Minimum Level")]
        public byte min_lvl { get; set; }
        [DisplayName("Maximum Level")]
        public byte max_lvl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DisplayName("Employees")]
        public virtual ICollection<employee> employees { get; set; }
    }
}