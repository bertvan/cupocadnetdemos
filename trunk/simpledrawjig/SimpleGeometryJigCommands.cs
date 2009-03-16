using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Cupocadnet
{
    public class SimpleGeometryJigCommands
    {
        [CommandMethod("ccnSimpleJig")]
        public void SimpleGeometryJig()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            using (Transaction t = doc.TransactionManager.StartTransaction())
            {

                // select some polylines
                PromptSelectionOptions promptSelection = new PromptSelectionOptions();



                PromptSelectionResult result = ed.GetSelection(promptSelection);

                if (result.Status != PromptStatus.OK)
                    return;

                List<Polyline> polylines = new List<Polyline>();

                foreach (ObjectId oid in result.Value.GetObjectIds())
                {
                    DBObject ent = t.GetObject(oid, OpenMode.ForWrite);

                    Polyline p = ent as Polyline;

                    if (p == null)
                        continue;

                    polylines.Add(p);

                }

                // prompt refernce point
                PromptPointOptions promptPoint = new PromptPointOptions("select reference point");
                PromptPointResult promptPointResult = ed.GetPoint(promptPoint);

                if (promptPointResult.Status != PromptStatus.OK)
                    return;



                SimpleGeometryJig jig = new SimpleGeometryJig(polylines, promptPointResult.Value);

                PromptResult res = ed.Drag(jig);

                t.Commit();

            }



        }
    }
}