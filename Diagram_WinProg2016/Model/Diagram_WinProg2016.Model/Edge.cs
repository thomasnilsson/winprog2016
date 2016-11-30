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
        public Brush SelectedColor { get { return IsSelected ? Brushes.Blue : Brushes.Black; } }

        private PointCollection setPoints(Class endA, Class endB)
        {
            int x1 = endA.CenterX, y1 = endA.CenterY, width1 = endA.Width, height1 = endA.Height,
                x2 = endB.CenterX, y2 = endB.CenterY, width2 = endB.Width, height2 = endB.Height;
            int xWidth = Math.Abs(x1 - x2), yWidth = Math.Abs(y1 - y2);
            int arrowLength = 1;
            Point startPoint, endPoint, head1, head2;
            PointCollection points = new PointCollection();

            if (xWidth <= yWidth)
            {
                if (y1 <= y2)
                {
                    startPoint = new Point(x1, y1 + height1 / 2);
                    endPoint = new Point(x2, y2 - height2 / 2);
                    head1 = new Point(x2 - arrowLength, y2 - height2 / 2 - arrowLength);
                    head2 = new Point(x2 + arrowLength, y2 - height2 / 2 - arrowLength);
                }
                else
                {
                    startPoint = new Point(x1, y1 - height1 / 2);
                    endPoint = new Point(x2, y2 + height2 / 2);
                    head1 = new Point(x2 - arrowLength, y2 + height2 / 2 + arrowLength);
                    head2 = new Point(x2 + arrowLength, y2 + height2 / 2 + arrowLength);
                }
            }

            else
            {
                if (x1 <= x2)
                {
                    startPoint = new Point(x1 + width1 / 2, y1);
                    endPoint = new Point(x2 - width2 / 2, y2);
                    head1 = new Point(x2 - width2 / 2 - arrowLength, y2 + arrowLength);
                    head2 = new Point(x2 - width2 / 2 - arrowLength, y2 - arrowLength);
                }
                else
                {
                    startPoint = new Point(x1 - width1 / 2, y1);
                    endPoint = new Point(x2 + width2 / 2, y2);
                    head1 = new Point(x2 + width2 / 2 + arrowLength, y2 - arrowLength);
                    head2 = new Point(x2 + width2 / 2 + arrowLength, y2 + arrowLength);

                }
            }

            points.Add(startPoint);
            points.Add(endPoint);
            points.Add(head1);
            points.Add(endPoint);
            points.Add(head2);
            points.Add(endPoint);
            return points;
        }
    }
}
