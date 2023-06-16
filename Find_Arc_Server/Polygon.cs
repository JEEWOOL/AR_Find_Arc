using MyFirstApp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcServer
{
    class Polygon
    {
        private List<Pos> mPointList = new List<Pos>();

        public void addPoint(double x, double y)
        {
            mPointList.Add(new Pos(x, y));
        }

        public bool isPointInPolygon(double x, double y)
        {
            int size = mPointList.Count();

            // 점이 3개 이하로 이루어진 polygon은 없다.
            if (size < 3)
            {
                return false;
            }

            int prev = size - 1;
            bool isInnerPoint = false;

            // Point in polygon algorithm
            for (int cur = 0; cur < size; cur++)
            {
                Pos curPoint = mPointList[cur];
                Pos prevPoint = mPointList[prev];

                /*
                 * y - y1 = M * (x - x1)
                 * M = (y2 - y1) / (x2 - x1)
                 */
                if (curPoint.lon < y && prevPoint.lon >= y || prevPoint.lon < y && curPoint.lon >= y)
                {
                    if (curPoint.lat + (y - curPoint.lon) / (prevPoint.lon - curPoint.lon) * (prevPoint.lat - curPoint.lat) < x)
                    {
                        isInnerPoint = !isInnerPoint;
                        //return isInnerPoint;
                    }
                }

                prev = cur;
            }

            return isInnerPoint;
        }
    }
}
