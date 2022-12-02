using AutoMapper;
using DAL.Entities;
using BLL.DTO;
using System.Linq;
using System.Collections.Generic;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Role, RoleDTO>()
                .ForMember(p => p.UsersIds, c => c.MapFrom(role => role.Users.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<Category, CategoryDTO>()
                .ForMember(p => p.TasksIds, c => c.MapFrom(category => category.Tasks.Select(b => b.Id)))
                .ReverseMap();

            CreateMap<JobStatus, JobStatusDTO>()
               .ForMember(p => p.TasksIds, c => c.MapFrom(job => job.Tasks.Select(b => b.Id)))
               .ReverseMap();

            CreateMap<Task, TaskDTO>()
               .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(p=>p.TasksIds,c=>c.MapFrom(s=>s.Tasks.Select(t=>t.Id)))
                .ReverseMap();

           // CreateMap<IEnumerable<User>, List<UserDTO>>().ReverseMap();
        }


    }
}

