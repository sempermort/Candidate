using AutoMapper;
using Candidate.Application.Dtos;
using Candidate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Test
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Prospect, ApplicantDto>();
            CreateMap<ApplicantDto, Prospect>();
        }
    }
}
