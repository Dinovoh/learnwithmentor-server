//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LearnWithMentorDAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanSuggestion
    {
        public int Plan_Id { get; set; }
        public int User_Id { get; set; }
        public int Mentor_Id { get; set; }
        public string Text { get; set; }
    
        public virtual Plan Plan { get; set; }
        public virtual User User { get; set; }
        public virtual User Mentor { get; set; }
    }
}
