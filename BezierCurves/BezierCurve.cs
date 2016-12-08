using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BezierCurves
{
    class BezierCurve
    {
        //сдесь храним точки
        private List<Point> pivots;
        public BezierCurve()
        {
            this.pivots = new List<Point>();
        }
        public BezierCurve(List<Point> pivots)
        {
            this.pivots = pivots;
        }
        public BezierCurve(Point[] pivots)
        {
            this.pivots = pivots.ToList();
        }
        //добавляет новую точку в хранилище
        public void addPivot(Point pivot)
        {
            this.pivots.Add(pivot);
        }
        // удаляет точку из хранилища по самой точке
        public void removePivot(Point pivot)
        {
            this.pivots.Remove(pivot);
        }
        // удаляет точку из хранилища по ее индексу
        public void removePivotIndex(int index)
        {
            this.pivots.RemoveAt(index);
        }
        // очищает хранилище
        public void clear()
        {
            this.pivots = new List<Point>();
        }
        // возвращает все точки в виде масива
        public Point[] getCurve()
        {
            return this.pivots.ToArray();
        }
        // возвращает индекс точки из хранилища которая находится на заданом растоянии от 
        // заданой точки или ближе.
        // если такой точки в хранилище нет возвращает -1;
        // point - заданая точка, eps - заданое растояние
        public int getPivotIndex(Point point, int eps)
        {
            // дальнейшие выражение ищет нужный индекс точки
            // p => (...)  - это лямда выражение (анонимная функция);
            // p - параметр функции
            // все что после => ее тело
            // в даном случае эта функция выполняется для 
            // каждого элемента в хранилище, где p - его очередной элемент
            // и возвращaет true/false 
            // в итоге как только она вернет true значит 
            // текущий p - искомый FindIndex возвращает его индекс
            // если на всех элементах вернет false то FindIndex вернет -1
            return this.pivots.FindIndex(p => (
                Math.Sqrt(Math.Pow(p.X - point.X, 2) + Math.Pow(p.Y - point.Y, 2)) < eps
            ));
        }
        // возвращает точку по индексу
        public Point getPivot(int index)
        {
            return this.pivots[index];
        }
        // устанавливает новое значение точки по индексу
        public void setPivot(int index, Point pivot)
        {
            if(index >= 0 && index < this.pivots.Count)
            {
                this.pivots[index] = pivot;
            }
        }
    }
}
