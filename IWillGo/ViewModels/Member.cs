using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IWillGo.Model;

namespace IWillGo.ViewModels
{
    public class Member : ViewModelBase
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        /*[JsonProperty("confirm")] 
        public bool Confirm {  get; set; }*/

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        public Model.Member ToModel()
        {
            var model = new Model.Member();
            model.Id = this.Id;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.Email = Email;
            model.Password = Password;
            //model.Confirm = Confirm;
            model.City = City;
            model.State = State;
            model.Zip = Zip;
            return model;
        }

        public Member FromModel(Model.Member model)
        {
            var member = new Member();
            member.Id = model.Id;
            member.FirstName = model.FirstName;
            member.LastName = model.LastName;
            member.Email = model.Email;
            member.Password = model.Password;
            //member.Confirm = model.Confirm;
            member.City = model.City;
            member.State = model.State;
            member.Zip = model.Zip;
            return member;
        }
    }
}
