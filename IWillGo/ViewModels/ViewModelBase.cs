
using IWillGo.Model;
using Newtonsoft.Json;
using System;

namespace IWillGo.ViewModels
{
    public abstract class ViewModelBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("createdDate")]
        public DateTime? CreatedDate { get; set; }
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }
        [JsonProperty("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }
        protected void AddBaseToModel(BaseModel model)
        {
            model.Id = this.Id;
            model.CreatedBy = this.CreatedBy;
            model.CreatedDate = this.CreatedDate;
            model.ModifiedBy = this.ModifiedBy;
            model.ModifiedDate = this.ModifiedDate;
        }
        protected void PopulateBaseFromModel(BaseModel model)
        {
            this.Id = model.Id;
            this.CreatedBy = model.CreatedBy;
            this.CreatedDate = model.CreatedDate;
            this.ModifiedBy = model.ModifiedBy;
            this.ModifiedDate = model.ModifiedDate;
        }
    }
    public abstract class ViewModelBase<T1, T2> where T1 : IWillGo.Model.BaseModel where T2 : ViewModelBase<T1, T2>
    {
        public abstract T1 ToModel();
        public abstract T2 FromModel(T1 model);
    }
}
}
