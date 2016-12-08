using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BezierCurves
{
    
    class RenderBezier
    {
        //радиус отрисованой точки
        private const int PIVOT_RADIUS = 4; //px
        // шаг интерполяции
        private const float RENDER_STEP = (float)0.001;
        //та конва на которой рисуем
        private Graphics context;
        //буферная канва в памяти для двойной буферизации (от мерцания)
        private BufferedGraphics bufferContext;
        // стиль линии Безье
        private Pen mainPen;
        //стиль опорной линии
        private Pen pivotLinePen;
        //стиль опорной точки
        private Brush pivotFillBrush;
        //границы отрисовки
        private Rectangle borderbox;
        

        public RenderBezier(Graphics context, Rectangle borderbox)
        {
            this.context = context;
            //создаем буфер отрисовки в памяти
            //сперва все рисуем в него, потом разом из него в канву (двойная буферизация, от мерцания)
            this.bufferContext = new BufferedGraphicsContext().Allocate(context, borderbox);
            this.borderbox = borderbox;
            this.mainPen = new Pen(Color.Black);
            this.pivotLinePen = new Pen(Color.Gray);
            //делаем стиль опорной линии чертачками
            pivotLinePen.DashStyle = DashStyle.Dash;
            this.pivotFillBrush = new SolidBrush(Color.Gray);

        }

        public void render(BezierCurve bezier)
        {
            //получаем опорные точки из хранилища
            Point[] curve = bezier.getCurve();
            //чистим канву
            this.bufferContext.Graphics.Clear(Color.White);
            //рисуем опорные точки и линии
            this.drawPivot(curve);
            //если кол-во точек больше одной то рисуем безье
            if(curve.Length > 1)
            {
                this.renderBezierLine(curve);
            }
            //все что получилось скидываем на экран
            this.bufferContext.Render();
        }
        //рисует опорные точки и линии
        private void drawPivot(Point[] curve)
        {
            if(curve.Length > 0)
            {
                //проходим по точкам и отрисовываем их и опорные линии
                for (int i = 1; i < curve.Length; i++)
                {
                    this.drawPivotLine(curve[i], curve[i - 1]);
                    this.drawPivotPoint(curve[i]);
                }

                this.drawPivotPoint(curve[0]);
            }
            
        }
        //рисует опорную точку
        private void drawPivotPoint(Point pivot)
        {
            int d = PIVOT_RADIUS + PIVOT_RADIUS;
            this.bufferContext.Graphics.FillEllipse(pivotFillBrush, pivot.X - PIVOT_RADIUS, pivot.Y - PIVOT_RADIUS, d, d);
        }
        //рисует опорную линию
        private void drawPivotLine(Point startPivot, Point endPivot)
        {
            this.bufferContext.Graphics.DrawLine(this.pivotLinePen, startPivot, endPivot);
        }
        //рисует саму безье
        private void renderBezierLine(Point[] pivots)
        {
            //так как кривая должна быть гладкой то переходим на дробное исчисление (PointF)
            PointF[] pivotsF = Array.ConvertAll(pivots, new Converter<Point, PointF>(PointTOPointF));
            //здесь сохраняем все точки кривой
            List<PointF> bezierPoints = new List<PointF>();
            //начинаем с первой
            bezierPoints.Add(pivots[0]);
            //вычисляем рекрсивно точку кривой для шага интерполяции
            for (float stage = 0; stage <= 1; stage += RENDER_STEP)
            {
                bezierPoints.Add(getStagePoint(stage, pivotsF));
            }
            //не забываем последнюю точку
            bezierPoints.Add(pivots.Last());
            //отрисовываем линию по точкам
            this.bufferContext.Graphics.DrawLines(this.mainPen, bezierPoints.ToArray());
        }
        //возвращает точку кривой от двух опорных и шага интерполяции
        private PointF getStagePoint(float stage, PointF pf1, PointF pf2)
        {
            return new PointF((float)((pf2.X - pf1.X) * stage + pf1.X), (float)((pf2.Y - pf1.Y) * stage + pf1.Y));
        }
        //возвращает точку кривой от множества опорный и шага интерполяции
        private PointF getStagePoint(float stage, PointF[] pivots)
        {
            // кол-во оорных точек - две то используем метод getStagePoint
            if (pivots.Length == 2)
            {
                return getStagePoint(stage, pivots[0], pivots[1]);
            }
            // иначе генерируем рекурсивно новые опорные точки на одну меньше чем старых
            else
            {
                PointF[] nextPivots = new PointF[pivots.Length - 1];
                for(int i = 1; i < pivots.Length; i++)
                {
                    nextPivots[i - 1] = this.getStagePoint(stage, pivots[i - 1], pivots[i]);
                }
                // рекурсивно получаем точку кривой (или опорную для следующего уровня рекурсии)
                return this.getStagePoint(stage, nextPivots);
            }
        }
        // методы конвертации Point to PointF и обратно
        private static  PointF PointTOPointF(Point point)
        {
            return new PointF(point.X, point.Y);
        }
        private static Point PointFTOPoint(PointF pointf)
        {
            return new Point(((int)pointf.X), ((int)pointf.Y));
        }
    }
}
