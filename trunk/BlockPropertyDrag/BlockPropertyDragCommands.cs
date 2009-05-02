using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Cupocadnet
{

    /// <summary>
    /// Code sample for EntityJig and Dynamic Block Properties
    /// </summary>
    public class BlockPropertyDragCommands
    {

        [CommandMethod("ccnBlockDrag")]
        public void InsterBlockAndDragProperties()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            using (Transaction t = doc.TransactionManager.StartTransaction())
            {
                Database db = doc.Database;
                BlockTable blockTable = t.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                // first, create the block reference in memory
                ObjectId blockDefinitionId = blockTable["dummy"];
                BlockReference blockReference = new BlockReference(
                    new Point3d(0, 0, 0), blockDefinitionId);

                // first append the block
                BlockTableRecord modelSpace = t.GetObject(
                    blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false) as BlockTableRecord;

                modelSpace.AppendEntity(blockReference);

                t.AddNewlyCreatedDBObject(blockReference, true);
                
                // allow user to move the block around with the InsertBlockJig
                InsertBlockJig insertBlockJig = new InsertBlockJig(blockReference);

                if (ed.Drag(insertBlockJig).Status != PromptStatus.OK)
                    return;

                // do the property drag jig on the block
                BlockPropertyDragJig dragJig = new BlockPropertyDragJig(blockReference);

                if (ed.Drag(dragJig).Status != PromptStatus.OK)
                    return;

                t.Commit();

            }       
        }

    }
}
