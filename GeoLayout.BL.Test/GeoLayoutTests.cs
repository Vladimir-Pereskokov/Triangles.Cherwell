using System;
using System.Linq;
using Xunit;
using GeoLayout.BL;
using System.Numerics;



namespace GeoLayout.BL.Test
{
    public class GeoLayoutTests
    {


        [Fact]
        public void Can_ReadTriangles2by2() {
            var cont = ContainerFactory.CreateSquare(2, 100, TrianglePattern.TwoPerColumn);
            var triangles = cont.GetAllTriangles().ToList();
            Assert.True(triangles.Count() == 8);


            Assert.Equal("A1", triangles[0].Location.Address);


            Assert.True(triangles[0].vertexA.X == 100);
            Assert.True(triangles[0].vertexA.Y == 0);

            Assert.True(triangles[0].vertexB.X == 0);
            Assert.True(triangles[0].vertexB.Y == 0);
            Assert.True(triangles[0].vertexC.X == 100);
            Assert.True(triangles[0].vertexC.Y == 100);

            Assert.True(triangles[7].vertexA.X == 100);
            Assert.True(triangles[7].vertexA.Y == 200);
            Assert.True(triangles[7].vertexB.X == 200);
            Assert.True(triangles[7].vertexB.Y == 200);
            Assert.True(triangles[7].vertexC.X == 100);
            Assert.True(triangles[7].vertexC.Y == 100);






        }



        [Fact]
        public void Can_ReadGridLines()
        {
            var cont = ContainerFactory.CreateSquare(4, 10, TrianglePattern.TwoPerColumn);
            var lines = cont.GetGridSegments();
            Assert.NotNull(lines);
            Assert.Equal<int>(17, lines.Count<GridLineSegment>());
            Assert.True(lines.FirstOrDefault((s) => s.PointA.X < 0).IsEmpty);
            var sz = cont.Size;
            Assert.Equal<float>(40F, sz.Height);
            Assert.Equal<float>(40F, sz.Width);
            var grps = lines.GroupBy(l => (l.PointA, l.PointB)).Where(g => g.Count() > 1).Select(g => g);                        
            Assert.False(grps.Any());
        }



        [Fact]
        public void Return_InvalidLocation()
        {
            // 6x6, cell size = 10
            var cont = ContainerFactory.CreateSquare(6, 10);
            var trgl = new Triangle(new Vector2(87), new Vector2(78), new Vector2(13));
            var loc = cont.GetTriangleLocation(trgl);
            Assert.False(loc.IsValid);
            trgl = new Triangle(new Vector2(50,50), new Vector2(60,50), new Vector2(50,40));
            loc = cont.GetTriangleLocation(trgl);
            Assert.True(loc.IsValid);
        }


        [Fact]
        public void Can_Create_Square_Container()
        {
            var cont = ContainerFactory.CreateSquare(6, 10, TrianglePattern.TwoPerColumn);
            Assert.True(cont.Rows == cont.Columns && cont.Rows == 6);
            var loc = cont.CreateLocation("");
            Assert.Throws<ArgumentOutOfRangeException>(() => {cont.GetTriangleAt(loc);});
            loc = cont.CreateLocation("C4");
            var t = cont.GetTriangleAt(loc);
            Assert.False(t.IsEmpty);
            Assert.Equal("C4", t.Location.Address);
            loc = cont.GetTriangleLocation(t);
            Assert.True(loc.IsValid);
            Assert.Equal("C4", loc.Address);
            loc = cont.CreateLocation("A2");
            t = cont.GetTriangleAt(loc);
            Assert.Equal("A2", t.Location.Address);
                
        }

        [Fact]
        public void Can_Create_Parallelogram_Container()
        {
            var cont = ContainerFactory.CreateParallelogram(60, 6, 10, TrianglePattern.TwoPerColumn);
            Assert.True(cont.Rows == cont.Columns && cont.Rows == 6);
            var loc = new TriangleLocation(4, 4);   
            var t = cont.GetTriangleAt(loc);
            Assert.False(t.IsEmpty);
            var tAddr = t.Location.Address;
            Assert.Equal("E5", tAddr);
            loc = cont.GetTriangleLocation(t);
            Assert.True(loc.IsValid);
            Assert.Equal("E5", loc.Address);

            var sz = cont.Size;
            var testHeight = Math.Round(Math.Sin(0.33333333) * 60, 4);
            var testWidth = Math.Round(60 * (1 + Math.Cos(0.33333333)), 4);
            Assert.Equal<double>(testHeight, Math.Round(sz.Height, 4));
            Assert.Equal<double>(testWidth , Math.Round(sz.Width, 4));


        }


        

    }
}
