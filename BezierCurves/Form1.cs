using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BezierCurves
{
    //Возможные состояния работы приложения
    enum OperationMode { ADD_POINT, MOVE_POINT, REMOVE_POINT }
    //Возможные состояния захвата точки
    enum DropDownMode { PREPAREDNESS, MOVE}
    //действия от канвы
    enum CanvasAction { DOWN, MOVE_TO, UP}

    public partial class Form1 : Form
    {
        // чуствительность захвата точки мышкой
        private int MOUSE_SENSITIVITY = 8; // px

        //Определяет текущий режим работы (добавление, перемещение, удаление точек).
        private OperationMode appMode = OperationMode.ADD_POINT;
        //Определяет режим перетаскивания (захвачена / не захвачена точка)
        private DropDownMode DDMode = DropDownMode.PREPAREDNESS;
        //отрисовщик хранилища опорных точек (смотри файл RenderBezier.cs) 
        private RenderBezier renderBezier;
        // хранилище опорных точек (смотри файл BezierCurve.cs)
        private BezierCurve bezierCurve;
        //текущая перемещаемая точка
        private int movingPivotIndex;

        public Form1()
        {
            InitializeComponent();
            //создаем хранилище под опорные точки Безье
            this.bezierCurve = new BezierCurve();

            //фиксируем размер окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //создаем контекст в котором будем все рисовать
            Graphics context = this.canvasPanel.CreateGraphics();
            //добавляем сглаживание
            context.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //определяем граници отрисовки в контексте
            Rectangle borderbox = new Rectangle(new Point(0, 0), this.canvasPanel.Size);
            //создаем обьект для отрисовки хранилища опорных точек
            this.renderBezier = new RenderBezier(context, borderbox);
        }

        /************************************************************************************ 
         *  методы обрабатывающие дейтвия от канвы в зависимости от 
         *  режима работы приложения  
         ************************************************************************************/
        //в зависимости от режима работы приложения вызывает нужный метод
        private void resolveCanvasAction(CanvasAction action, Point data)
        {
            switch (this.appMode)
            {
                case OperationMode.ADD_POINT:
                    this.resolveCanvasAction_addPoint(action, data);
                    break;
                case OperationMode.MOVE_POINT:
                    this.resolveCanvasAction_movePoint(action, data);
                    break;
                case OperationMode.REMOVE_POINT:
                    this.resolveCanvasAction_removePoint(action, data);
                    break;
            }
        }
        // метод обработки действия от канвы при режиме добавления точки.
        private void resolveCanvasAction_addPoint(CanvasAction action, Point point)
        {
            //если действи - опустиласль кнопка мыши то
            // добавляем новую опорную точку в хранилище с места клика и отрисовываем его 
            if(action == CanvasAction.DOWN)
            {
                this.bezierCurve.addPivot(point);
                this.renderBezier.render(this.bezierCurve);
            }
        }
        // метод обработки действия от канвы при режиме перемещения точки.
        private void resolveCanvasAction_movePoint(CanvasAction action, Point point)
        {
            switch (action)
            {
                //если действие - кнопка мыши опустилась
                case CanvasAction.DOWN:
                    // и если она была достаточно рядом к одной из точек (задается this.MOUSE_SENSITIVITY)
                    int movingPivotIndex = this.bezierCurve.getPivotIndex(point, this.MOUSE_SENSITIVITY);
                    if (movingPivotIndex >= 0)
                    {
                        //то сохраняем индекс этой точки и включаем режим ее перемещения
                        this.movingPivotIndex = movingPivotIndex;
                        this.DDMode = DropDownMode.MOVE;
                    }
                    break;
                //если действие перемещение мышки и включен режим перемещения
                case CanvasAction.MOVE_TO:
                    if(this.DDMode == DropDownMode.MOVE)
                    {
                        //то меняем состояние точки в хранилище и отрисовываем его.
                        this.bezierCurve.setPivot(this.movingPivotIndex, point);
                        this.renderBezier.render(this.bezierCurve);
                    }
                    break;
                //если действие - поднялась кнопка мыши то отключаем режим перемещения точки.
                case CanvasAction.UP:
                    this.DDMode = DropDownMode.PREPAREDNESS;
                    break;
            }
                
                


        }
        // метод обработки действия от канвы при режиме удаления точки.
        private void resolveCanvasAction_removePoint(CanvasAction action, Point point)
        {
            //если действи - опустиласль кнопка мыши то
            if (action == CanvasAction.DOWN)
            {
                // и если она была достаточно рядом к одной из точек(задается this.MOUSE_SENSITIVITY)
                int removingPivotIndex = this.bezierCurve.getPivotIndex(point, this.MOUSE_SENSITIVITY);
                if(removingPivotIndex >= 0)
                {
                    //то удаляем точку из хранилища в хранилище и отрисовываем его.
                    this.bezierCurve.removePivotIndex(removingPivotIndex);
                    this.renderBezier.render(this.bezierCurve);
                }
            }
        }

        /************************************************************************************ 
         *  методы реагирующие на переключение режима 
         *  работы (добавление, перемещение, удаление точек).
         *  В зависимости от переключателя будет установлен соответсвующий 
         *  режим работы в свойсве this.appMode  
         ************************************************************************************/

        private void addPointRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.appMode = OperationMode.ADD_POINT;
            }
        }

        private void movePointRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.appMode = OperationMode.MOVE_POINT;
                this.DDMode = DropDownMode.PREPAREDNESS;
            }
        }

        private void removePointRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.appMode = OperationMode.REMOVE_POINT;
            }
        }

        /************************************************************************************ 
         *  методы реагирующие на действия мышью на канвасе
         *  отправляют точку действия и его тип в resolveCanvasAction
         *  для дальнейшей обработки в зависимости от режима работы приложения
         ************************************************************************************/

        private void canvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Point downPos = new Point(e.X, e.Y);
            this.resolveCanvasAction(CanvasAction.DOWN, downPos);
        }

        private void canvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Point upPos = new Point(e.X, e.Y);
            this.resolveCanvasAction(CanvasAction.UP, upPos);
        }

        private void canvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point movePos = new Point(e.X, e.Y);
            this.resolveCanvasAction(CanvasAction.MOVE_TO, movePos);
        }

        //очищает хранилище и отрисовывает его
        private void clear_button_Click(object sender, EventArgs e)
        {
            this.bezierCurve.clear();
            this.renderBezier.render(this.bezierCurve);
        }
    }
}
