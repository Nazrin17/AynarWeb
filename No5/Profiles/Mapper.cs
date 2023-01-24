using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using No5.Dtos.Employee;
using No5.Models;

namespace No5.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Employee,EmployeeGetDto>();
            CreateMap<EmployeePostDto, Employee>();
        }
    }
}
