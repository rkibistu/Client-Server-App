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
    
    public partial class ProgramariFilme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgramariFilme()
        {
            this.LocuriSalis = new HashSet<LocuriSali>();
        }
    
        public int IdProgramare { get; set; }
        public int IdSala { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public int IdFilm { get; set; }
        public Nullable<int> IdTip { get; set; }
        public Nullable<int> Pret { get; set; }
    
        public virtual Filme Filme { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocuriSali> LocuriSalis { get; set; }
        public virtual Sali Sali { get; set; }
    }
}