using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;

namespace CSProjectTemplate1
{
    public class CSProjectTemplate1Command : Command
    {
        public CSProjectTemplate1Command()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static CSProjectTemplate1Command Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "CSProjectTemplate1Command"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)                                    // всё происходит здесь внутри
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("The {0} command will add a line right now.", EnglishName);                  // сообщение в командной строке о том что команда запущена и что она будет делать
                                                                                                            //
            Point3d pt0;                                                                                    // декларируем новую переменную точка (начало линии)
            using (GetPoint getPointAction = new GetPoint())                                                // ??
            {                                                                                               //
                getPointAction.SetCommandPrompt("Please select the start point");                           // призыв к действию в командной строке (жирным)
                if (getPointAction.Get() != GetResult.Point)                                                // что делать если точкка не выбрана но был нажет Enter
                {                                                                                           // 
                    RhinoApp.WriteLine("No start point was selected.");                                     // если точкка не выбрана но был нажет Enter, то написать это в командной строке
                    return getPointAction.CommandResult();                                                  // ??
                }                                                                                           //
                pt0 = getPointAction.Point();                                                               // присвоить для переменной pt1 только что выбранную точку
            }

            Point3d pt1;                                                                                    // декларируе новую переменную точка ( конец линии)                                                           
            using (GetPoint getPointAction = new GetPoint())
            {                                                                                               //
                getPointAction.SetCommandPrompt("Please select the end point");
                getPointAction.SetBasePoint(pt0, true);
                getPointAction.DynamicDraw +=
                  (sender, e) => e.Display.DrawLine(pt0, e.CurrentPoint, System.Drawing.Color.DarkRed);     
                if (getPointAction.Get() != GetResult.Point)                                                // если точкка не выбрана но был нажет Enter
                {
                    RhinoApp.WriteLine("No end point was selected.");
                    return getPointAction.CommandResult();
                }
                pt1 = getPointAction.Point();                                                               // присвоить для переменной pt1 только что выбранную точку  
            }

            doc.Objects.AddLine(pt0, pt1);
            doc.Views.Redraw();
            RhinoApp.WriteLine("The {0} command added one line to the document.", EnglishName);             // сообщение в командную строку что всё готово

            // ---

            return Result.Success;
        }
    }
}
