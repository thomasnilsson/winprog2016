using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Diagram_WinProg2016.Model
{
    public class Edge : NotifyBase
    {
        public Edge(Class a, Class b)
        {
            endA = a;
            endB = b;

            points = setPoints(endA, endB);
        }

        private Class endA;
        public Class EndA
        {
            get { return endA; }
            set { if (endA == value) return; endA = value; NotifyPropertyChanged("EndA"); }
        }

        private Class endB;
        public Class EndB
        {
            get { return endB; }
            set { if (endB == value) return; endB = value; NotifyPropertyChanged("EndB"); }
        }

        private PointCollection points = new PointCollection();
        public PointCollection Points
        {
            get
            {
                return points;
            }
            set
            {
                points = setPoints(endA, endB);
                NotifyPropertyChanged("Points");
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
                NotifyPropertyChanged("IsSelected");
                NotifyPropertyChanged("SelectedColor");
            }

        }

        private PointCollection setPoints(Class endA, Class endB)
        {
            #region variable declaration
            var ClassWidth = 250; //static for now

            var X1 = endA.CenterX;
            var Y1 = endA.CenterY;

            var X2 = endB.CenterX;
            var Y2 = endB.CenterY; 

            var DeltaX = Math.Abs(X1 - X2);
            var DeltaY = Math.Abs(Y1 - Y2);

            var startPoint = new Point(0,0);
            var endPoint = new Point(0,0);

            PointCollection points = new PointCollection();
            #endregion

            if (DeltaX <= DeltaY)
            {
                if (Y1 <= Y2)
                {
                    startPoint = new Point(X1, Y1 + endA.Height / 2);
                    endPoint = new Point(X2, Y2 - endB.Height / 2);
                }
                else
                {
                    startPoint = new Point(X1, Y1 - endA.Height / 2);
                    endPoint = new Point(X2, Y2 + endB.Height / 2);
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
        }
    }
}
