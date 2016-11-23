using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Diagram_WinProg2016.Model
{
    public class Connector : NotifyBase
    {
        public Connector(Class s, Class e)
        {
            startClass = s;
            endClass = e;

           // points = setPoints(startClass, endClass);
        }
        private Class startClass;
        public Class StarClass
        {
            get { return startClass; }
            set { if (startClass == value) return; startClass = value; NotifyPropertyChanged("StartClass"); }
        }
        private Class endClass;
        public Class EndClass
        {
            get { return endClass; }
            set { if (endClass == value) return; endClass = value; NotifyPropertyChanged("EndClass"); }
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
               // points = setPoints(startClass, endClass);
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
        //Kunne evt. være placeret i en controller f.eks. edgeViewModel istedet. 
        //Vi valgte dog at vi syntes det var mest overskueligt at placere den her. 
        /*private PointCollection setPoints(Class startClass, Class endClass)
        {
            int x1 = startClass.centeroffX, y1 = startClass.centeroffY, width1 = endClass.Width, height1 = startClass.Height,
                x2 = endClass.centeroffX, y2 = endClass.centeroffY, width2 = endClass.Width, height2 = endClass.Height;
            int STEP = 25, stepX, stepY, xWidth = Math.Abs(x1 - x2), yWidth = Math.Abs(y1 - y2);
            int arrowLength = 1;

            Point start, second, third, fourth, fifth, end, arrow1, arrow2;
            PointCollection points = new PointCollection();

          

            if (xWidth <= yWidth)
            {

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



            else
            {



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
        }*/
    }   
}
