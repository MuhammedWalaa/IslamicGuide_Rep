//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IslamicGuide.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookContent
    {
        public int Id { get; set; }
        public string BookContent1 { get; set; }
        public int AyahId { get; set; }
        public int SouraId { get; set; }
        public int BookId { get; set; }
        public string BookContentHTML { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual QuranAyat QuranAyat { get; set; }
        public virtual QuranSuar QuranSuar { get; set; }
    }
}
