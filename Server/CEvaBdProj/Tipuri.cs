//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CEvaBdProj
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tipuri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipuri()
        {
            this.TipulFilmuluis = new HashSet<TipulFilmului>();
        }
    
        public int IdTip { get; set; }
        public string Denumire { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipulFilmului> TipulFilmuluis { get; set; }
    }
}