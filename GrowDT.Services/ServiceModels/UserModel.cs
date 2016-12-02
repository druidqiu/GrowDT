using GrowDT.Application.Services.Dto;

namespace GrowDT.Services.ServiceModels
{
    public class UserModel: IEntityDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
