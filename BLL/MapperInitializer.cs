using System;
using AutoMapper;
namespace BLL
{
    public class MapperInitializer
    {
        public static Mapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}
