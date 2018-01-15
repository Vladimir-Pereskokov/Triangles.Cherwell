using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GeoLayout.BL;
using Microsoft.Extensions.DependencyInjection;
using GeoLayout.WebAPI.Services;

namespace GeoLayout.WebAPI.Services {

    /// <summary>
    /// Returns the list of active parallelogram containers
    /// </summary>
    public class LayoutContainerService
    {
        private List<IParallelogramContainer> _list = null;
        public IReadOnlyCollection<IParallelogramContainer> GetActiveContainers() {
            if (_list == null) {
                _list = new List<IParallelogramContainer>();
                _list.Add(ContainerFactory.CreateSquare(6,60)); //standard 6x6 square with each cell size = 80 units
                _list.Add(ContainerFactory.CreateSquare(6,60,TrianglePattern.TwoPerRow)); //standard 6x6 square with each cell size = 80 units
               // _list.Add(ContainerFactory.CreateParallelogram(60, 4, 120)); //4x4 parallelogram with 60 degree angle and column width = 180 units
            }
            return _list.AsReadOnly();
        }        
        public IParallelogramContainer GetContainer(int index) => GetActiveContainers().ElementAt(index);

    }
}

namespace GeoLayout.WebAPI {
    public static class SvcCollectionItems {
        public static IServiceCollection AddLayoutConteinerSvc( this IServiceCollection services) {
            if (services != null) {
                var descr = new ServiceDescriptor(typeof(LayoutContainerService),
                                                  new LayoutContainerService());
                services.Add(descr);
            }
            return services;
        }
    }
}