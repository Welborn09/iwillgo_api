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

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        public bool Confirm {  get; set; }

        public Model.Member ToModel()
        {
            var model = new Model.Member();
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.Email = Email;
            model.Confirm = Confirm;
            return model;
        }

        public Member FromModel(Model.Member model)
        {
            var member = new Member();
            member.FirstName = model.FirstName;
            member.LastName = model.LastName;
            member.Email = model.Email;
            return member;
        }
    }
}
