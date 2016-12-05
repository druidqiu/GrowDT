using GrowDT.Application.Services.Dto;
using GrowDT.Models.Entities;

namespace GrowDT.Services.ServiceModels
{
    [AutoMappers.AutoMapFrom(typeof (User))]
    public class UserModel : IEntityDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
