using AutoMapper;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;
using ShopNT.Services.Dtos.BrandDtos;
using ShopNT.Services.Dtos.Common;
using ShopNT.Services.Exceptions;
using ShopNT.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public CreatedEntityDto Create(BrandPostDto postDto)
        {
            if (_brandRepository.IsExist(x => x.Name == postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest,"Name","Name is already taken");
            
            
            var entity=_mapper.Map<Brand>(postDto);

            _brandRepository.Add(entity);
            _brandRepository.Commit();

            return new CreatedEntityDto { Id = entity.Id };
        }

        public void Delete(int id)
        {
            var entity = _brandRepository.Get(x=>x.Id==id);

            if (entity == null) throw new RestException(System.Net.HttpStatusCode.NotFound,"Entity not found");
            

            _brandRepository.Delete(entity);
            _brandRepository.Commit();
        }

        public void Edit(int id, BrandPutDto putDto)
        {
            var entity = _brandRepository.Get(x=>x.Id==id);

            if (entity == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Entity not found");

            if (entity.Name!=putDto.Name && _brandRepository.IsExist(x => x.Name == putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name is already taken");

            entity.Name= putDto.Name;
            _brandRepository.Commit();
        }

        public BrandGetItemDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<BrandGetAllItemsDto> GetAll()
        {
            var data = _brandRepository.GetAll(x=>true);

            return _mapper.Map<List<BrandGetAllItemsDto>>(data);
        }
    }
}
