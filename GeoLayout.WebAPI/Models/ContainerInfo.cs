using System;
using System.Drawing;
using GeoLayout.BL;


namespace GeoLayout.WebAPI.Models
{
    public class ContainerInfo {

        public ContainerInfo(): base() {}
        public ContainerInfo(IParallelogramContainer container, int index): base() {
            Index = index;
            Name = container.Name; 
            var sz = container.Size;
            Height = sz.Height;
            Width = sz.Width;
        }
        public int Index { get; set; }
        public string Name { get; set; }

        public float Height { get; set;}

        public float Width { get; set;}
        
    }
}