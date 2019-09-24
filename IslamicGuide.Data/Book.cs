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
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.BookContents = new HashSet<BookContent>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Title_English { get; set; }
        public string Author_English { get; set; }
    
        public virtual BookCategory BookCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookContent> BookContents { get; set; }
    }
}
