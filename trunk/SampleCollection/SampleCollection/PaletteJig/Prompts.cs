using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;

namespace SampleCollection.PaletteJig
{
    public class Prompts
    {

        public static Action<Polyline> ActionOnPromptPolyline;

        public Polyline PromptNewPolylineByPoints()
        {

            Polyline resultingPolyline = null;

            Document doc =
             Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            // Get the current UCS, to pass to the Jig
            Matrix3d ucs =
              ed.CurrentUserCoordinateSystem;

            // Create our Jig object
            PlineJig jig = new PlineJig(ucs);

            // Loop to set the vertices directly on the polyline
            bool bSuccess = true, bComplete = false;
            do
            {
                PromptResult res = ed.Drag(jig);
                bSuccess =
                  (res.Status == PromptStatus.OK);
                // A new point was added
                if (bSuccess)
                    jig.AddLatestVertex();
                // Null input terminates the command
                bComplete =
                  (res.Status == PromptStatus.None);
                if (bComplete)
                    // Let's clean-up the polyline before adding it
                    jig.RemoveLastVertex();
            } while (bSuccess && !bComplete);

            // If the jig completed successfully, add the polyline
            if (bComplete)
            {
                // Append entity
                Database db = doc.Database;
                Transaction tr =
                  db.TransactionManager.StartTransaction();
                using (tr)
                {
                    BlockTable bt =
                      (BlockTable)tr.GetObject(
                        db.BlockTableId,
                        OpenMode.ForRead,
                        false
                      );
                    BlockTableRecord btr =
                      (BlockTableRecord)tr.GetObject(
                        bt[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite,
                        false
                      );
                    btr.AppendEntity(jig.GetEntity());
                    tr.AddNewlyCreatedDBObject(jig.GetEntity(), true);
                    tr.Commit();
                }

                resultingPolyline = (Polyline)jig.GetEntity();
            }


            // Added this code, so that a certain action can be called after prompting for polyline
            if (ActionOnPromptPolyline != null)
            {
                try
                {
                    ActionOnPromptPolyline.Invoke(resultingPolyline);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ActionOnPromptPolyline = null;
                }
            }
                

            return resultingPolyline;
        }
    }
}
