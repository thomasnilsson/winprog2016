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
        // Constructor der bruges n�r en kant tilf�jes, points s�ttes til den rigtige kant
        public Edge(Class a, Class b)
        {
            endA = a;
            endB = b;

            points = setPoints(endA, endB);
        }

        // Get og set for endepunkterne
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



        // Bruges af EdgeUserControl til at l�se punkterne i kanten
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
        // Metoden der udregner hvordan en kant skal se ud afh. af forholdet mellem de to endepunkter
        //Kunne evt. v�re placeret i en controller f.eks. edgeViewModel istedet. 
        //Vi valgte dog at vi syntes det var mest overskueligt at placere den her. 
        private PointCollection setPoints(Class endA, Class endB)
        {
            int x1 = endA.CenterX, y1 = endA.CenterY, width1 = endA.Width, height1 = endA.Height,
                x2 = endB.CenterX, y2 = endB.CenterY, width2 = endB.Width, height2 = endB.Height;
            int STEP = 25, stepX, stepY, xWidth = Math.Abs(x1 - x2), yWidth = Math.Abs(y1 - y2);
            int arrowLength = 1;
            Point start, second, third, fourth, fifth, end, arrow1, arrow2;
            PointCollection points = new PointCollection();

            // top eller bund
            if (xWidth <= yWidth)
            {
                // klasse 1 �verst
                if (y1 <= y2)
                {
                    start = new Point(x1, y1 + height1 / 2);
                    end = new Point(x2, y2 - height2 / 2);
                    stepY = Math.Max(STEP, (yWidth - (height1 + height2) / 2) / 10);
                    second = new Point(x1, y1 + height1 / 2 + stepY);
                    fifth = new Point(x2, y2 - height2 / 2 - stepY);
                    stepX = xWidth / 2;
                    if (x1 < x2)
                    {
                        stepX = -1 * stepX;
                    }
                    third = new Point(x1 - stepX, y1 + height1 / 2 + stepY);
                    fourth = new Point(x2 + stepX, y2 - height2 / 2 - stepY);
                    arrow1 = new Point(x2 - arrowLength, y2 - height2 / 2 - arrowLength);
                    arrow2 = new Point(x2 + arrowLength, y2 - height2 / 2 - arrowLength);
                }
                // klasse 2 �verst
                else
                {
                    start = new Point(x1, y1 - height1 / 2);
                    end = new Point(x2, y2 + height2 / 2);
                    stepY = Math.Max(STEP, (yWidth - (height1 + height2) / 2) / 10);
                    second = new Point(x1, y1 - height1 / 2 - stepY);
                    fifth = new Point(x2, y2 + height2 / 2 + stepY);
                    stepX = xWidth / 2;
                    if (x1 < x2)
                    {
                        stepX = -1 * stepX;
                    }
                    third = new Point(x1 - stepX, y1 - height1 / 2 - stepY);
                    fourth = new Point(x2 + stepX, y2 + height2 / 2 + stepY);
                    arrow1 = new Point(x2 - arrowLength, y2 + height2 / 2 + arrowLength);
                    arrow2 = new Point(x2 + arrowLength, y2 + height2 / 2 + arrowLength);
                }
            }
            // h�jre eller venstre
            else
            {
                // klasse 1 til venstre
                if (x1 <= x2)
                {
                    start = new Point(x1 + width1 / 2, y1);
                    end = new Point(x2 - width2 / 2, y2);
                    stepX = Math.Max(STEP, (xWidth - (width1 + width2) / 2) / 10);
                    second = new Point(x1 + width1 / 2 + stepX, y1);
                    fifth = new Point(x2 - width2 / 2 - stepX, y2);
                    stepY = yWidth / 2;
                    if (y1 < y2)
                    {
                        stepY = -1 * stepY;
                    }
                    third = new Point(x1 + width1 / 2 + stepX, y1 - stepY);
                    fourth = new Point(x2 - width2 / 2 - stepX, y2 + stepY);
                    arrow1 = new Point(x2 - width2 / 2 - arrowLength, y2 + arrowLength);
                    arrow2 = new Point(x2 - width2 / 2 - arrowLength, y2 - arrowLength);
                }
                // klasse 2 til venstre
                else
                {
                    start = new Point(x1 - width1 / 2, y1);
                    end = new Point(x2 + width2 / 2, y2);
                    stepX = Math.Max(STEP, (xWidth - (width1 + width2) / 2) / 10);
                    second = new Point(x1 - width1 / 2 - stepX, y1);
                    fifth = new Point(x2 + width2 / 2 + stepX, y2);
                    stepY = yWidth / 2;
                    if (y1 < y2)
                    {
                        stepY = -1 * stepY;
                    }
                    third = new Point(x1 - width1 / 2 - stepX, y1 - stepY);
                    fourth = new Point(x2 + width2 / 2 + stepX, y2 + stepY);
                    arrow1 = new Point(x2 + width2 / 2 + arrowLength, y2 - arrowLength);
                    arrow2 = new Point(x2 + width2 / 2 + arrowLength, y2 + arrowLength);

                }
            }

            points.Add(start);
            points.Add(second);
            points.Add(third);
            points.Add(fourth);
            points.Add(fifth);
            points.Add(end);
            points.Add(arrow1);
            points.Add(end);
            points.Add(arrow2);
            points.Add(end);

            return points;
        }
    }
}
