using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

namespace Cupocadnet
{
    public class InsertBlockJig:EntityJig
    {

        private Vector3d currentVector;

        public InsertBlockJig(BlockReference blockReference):base(blockReference)
        {
            currentVector = new Vector3d(0, 0, 0);
        }

        protected override bool Update()
        {
            JiggedBlockReference.Position = JiggedBlockReference.Position.Add(currentVector);
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
            currentVector = CurrentPosition.GetVectorTo(res.Value);
            
            return SamplerStatus.OK;
        }
    }
}
