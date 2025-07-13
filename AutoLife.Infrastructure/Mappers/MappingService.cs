using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Mappers
{
    public class MappingService(IMapper mapper) : IMappingService
    {
        public TDestination Map<TDestination>(object source) =>
        mapper.Map<TDestination>(source);

        public void Map<TDestination>(object source, TDestination destination) =>
            mapper.Map(source, destination);
    }
}
