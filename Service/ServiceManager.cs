using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IMentorService> _LazyMentorService = new Lazy<IMentorService>(() => new MentorService(unitOfWork, mapper));
        public IMentorService MentorService => _LazyMentorService.Value;
    }
}
