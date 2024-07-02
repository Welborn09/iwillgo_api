using System;

namespace IWillGo.Model
{
    public abstract class BaseModel 
    {
        public string Id { get; set; }
        public bool IsNew { get; set; } 
        public string CreatedBy { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }  
        public DateTime? ModifiedDate { get; set; }
    }
}