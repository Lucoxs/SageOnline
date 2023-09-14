using API.Identity.DTO.User;

namespace API.Identity.DTO.Company
{
    public class CompanyUserDTO : CompanyDTO
    {
        public List<UserDTO> Users { get; set; } = new();

        public CompanyUserDTO(Models.Company company) : base(company)
        {
            company.Users.ToList().ForEach(x => Users.Add(new UserDTO(x, null)));
        }
    }
}
