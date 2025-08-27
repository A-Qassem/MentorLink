using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MentorService(IUnitOfWork _unitOfWork, IMapper mapper) : IMentorService
    {
        public async Task<IEnumerable<MentorDto>> GetAllMentorsAsync()
        {
            var Repo = _unitOfWork.GetRepository<Mentor>();
            var mentors = await Repo.GetAllAsync();
            var mentorDtos = mapper.Map<IEnumerable<Mentor>,IEnumerable<MentorDto>>(mentors);
            return mentorDtos;

        }

        public async Task<MentorDto> GetMentorById(int id)
        {
            var Repo = _unitOfWork.GetRepository<Mentor>();
            var mentor = await Repo.GetByIdAsync(id);
            var mentorDto = mapper.Map<Mentor, MentorDto>(mentor);
            return mentorDto;
        }
    }
}
