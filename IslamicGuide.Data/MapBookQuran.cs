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
    
    public partial class MapBookQuran
    {
        public int BookID { get; set; }
        public int ParagraphID { get; set; }
        public int PositionID { get; set; }
    
        public virtual BookContent BookContent { get; set; }
        public virtual Position Position { get; set; }
    }
}
