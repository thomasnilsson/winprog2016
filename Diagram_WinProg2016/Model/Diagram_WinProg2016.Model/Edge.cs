using System;
using System.Windows;
using System.Windows.Media;

namespace Diagram_WinProg2016.Model
{
    public class Edge : NotifyBase
    {
        public Edge(Class _from, Class _to)
        {
            From = _from;
            To = _to;
            edgeCoordinates = generateEdge(From, To);
        }

        #region Properties
        
        private Class from;
        public Class From
        {
            get { return from; }
            set { if (from == value) return; from = value; NotifyPropertyChanged(); }
        }

        private Class to;
        public Class To
        {
            get { return to; }
            set { if (to == value) return; to = value; NotifyPropertyChanged(); }
        }

        private PointCollection edgeCoordinates;
        public PointCollection Points
        {
            get
            {
                return edgeCoordinates;
            }
            set
            {
                edgeCoordinates = generateEdge(from, to);
                NotifyPropertyChanged();
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }

        }
        #endregion

        private PointCollection generateEdge(Class _From, Class _To)
        {
            #region variable declaration
            var ClassWidth = 250; //static for now

            var X1 = _From.CenterX;
            var Y1 = _From.CenterY;

            var X2 = _To.CenterX;
            var Y2 = _To.CenterY; 

            var DeltaX = Math.Abs(X1 - X2);
            var DeltaY = Math.Abs(Y1 - Y2);

            var startPoint = new Point(0,0);
            var endPoint = new Point(0,0);

            var points = new PointCollection();
            #endregion

            #region logic
            

            if (DeltaX <= DeltaY)
            {
                if (Y1 <= Y2)
                {
                    startPoint = new Point(X1, Y1 + _From.Height / 2);
                    endPoint = new Point(X2, Y2 - _To.Height / 2);
                }
                else
                {
                    startPoint = new Point(X1, Y1 - _From.Height / 2);
                    endPoint = new Point(X2, Y2 + _To.Height / 2);
                }
            }

            else
            {
                if (X1 <= X2)
                {
                    startPoint = new Point(X1 + ClassWidth / 2, Y1);
                    endPoint = new Point(X2 - ClassWidth / 2, Y2);
                }
                else
                {
                    startPoint = new Point(X1 - ClassWidth / 2, Y1);
                    endPoint = new Point(X2 + ClassWidth / 2, Y2);

                }
            }

            points.Add(startPoint);
            points.Add(endPoint);
            return points;
            #endregion
        }
    }
}
