//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace moneyApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Wydatkii
    {
        public int Id { get; set; }
        public decimal Kwota { get; set; }
        public string Nazwa_Transakcji { get; set; }
        public string Rodzaj_Transakcji { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    }
}