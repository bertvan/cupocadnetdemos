using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Cupocadnet
{
    public class BlockPropertyDragJig:EntityJig
    {


        Point3d lastMousePosition = new Point3d();
        
        public BlockPropertyDragJig(BlockReference blockReference)
            :base(blockReference)
        {
        }

        protected override bool Update()
        {

            // only 2d
            Point2d oldPosition = new Point2d(CurrentPosition.X, CurrentPosition.Y);
            Point2d newPosition = new Point2d(lastMousePosition.X, lastMousePosition.Y);

            JiggedBlockReference.Rotation = oldPosition.GetVectorTo(newPosition).Angle - Math.PI/2;

            // loop properties to find the ones we need
            foreach (DynamicBlockReferenceProperty prop in JiggedBlockReference.DynamicBlockReferencePropertyCollection)
            {
                if (prop.PropertyName.ToUpper() == "LENGTH")
                {
                    prop.Value = oldPosition.GetDistanceTo(newPosition);
                }
            }
                
            return true;
        }

        private BlockReference JiggedBlockReference
        {
            get { return (BlockReference)Entity; }
        }

        private Point3d CurrentPosition
        {
            get { return JiggedBlockReference.Position; }
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("select insertion point");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;


            PromptPointResult res = prompts.AcquirePoint(jigOpt);

            if (res.Status != PromptStatus.OK)
                return SamplerStatus.Cancel;



            // compare points
            if (res.Value.IsEqualTo(CurrentPosition, new Tolerance(0.1, 0.1)))
                return SamplerStatus.NoChange;

            // get vector to current position
            lastMousePosition = res.Value;

            return SamplerStatus.OK;

        }
    }
}
