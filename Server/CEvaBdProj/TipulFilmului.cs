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
    
    public partial class TipulFilmului
    {
        public int IdFilm { get; set; }
        public int IdTip { get; set; }
        public Nullable<int> Cheie { get; set; }
    
        public virtual Filme Filme { get; set; }
        public virtual Tipuri Tipuri { get; set; }
    }
}