using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Mappers
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);
        void Map<TDestination>(object source, TDestination destination);
    }
}
