using System;
using AutoMapper;
using CrossCutting.ViewModels.Employers;
using CrossCutting.Models;
using CrossCutting.ViewModels.PhoneNumber;

namespace CrossCutting.Mapping
{
	public class Mapping : Profile
	{
		public Mapping()
		{
            CreateMap<AddEmployerRequest, Employer>()
				.ForMember(x=> x.CreatedDate,src=> src.MapFrom(x=> DateTime.UtcNow))
                .ForMember(x => x.Enabled, src => src.MapFrom(x => true));
            CreateMap<Employer, UpdateEmployerRequest>();

            CreateMap<UpdateEmployerRequest, Employer>()
                .ForMember(x => x.UpdatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ForMember(d => d.Id, opt => opt.MapFrom((s, d, _, context) =>
                {
                    return Convert.ToInt32(context.Items["employerId"]);
                }));

            CreateMap<Employer, AddEmployerResponse>();

            CreateMap<AddPhoneNumberRequest,PhoneNumber>()
                .ForMember(d => d.EmployerId, opt => opt.MapFrom((s, d, _, context) =>
                {
                    return Convert.ToInt32(context.Items["employerId"]);
                }));

            CreateMap<UpdatePhoneNumberRequest,PhoneNumber>()
                .ForMember(d => d.EmployerId, opt => opt.MapFrom((s, d, _, context) =>
                {
                    return Convert.ToInt32(context.Items["employerId"]);
                }))
                .ForMember(d => d.Id, opt => opt.MapFrom((s, d, _, context) =>
                {
                    return Convert.ToInt32(context.Items["phoneNumberId"]);
                })); ;

        }
    }
}

