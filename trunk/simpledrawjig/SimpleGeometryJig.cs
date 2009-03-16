using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Cupocadnet
{
    public class SimpleGeometryJig : DrawJig
    {

        private IList<Polyline> polylines;
        private Point3d currentPosition;
        private Vector2d currentVector;

        public SimpleGeometryJig(IList<Polyline> polylines, Point3d referencePoint)
        {
            this.polylines = polylines;

            // use first point in polyline collection as reference point
            currentPosition = referencePoint;

            // init current vector as 0,0,0
            currentVector = new Vector2d(0, 0);
            
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("select insertion point");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;


            PromptPointResult res = prompts.AcquirePoint(jigOpt);

            if (res.Status != PromptStatus.OK)
                return SamplerStatus.Cancel;

            // compare points
            if (res.Value.IsEqualTo(currentPosition, new Tolerance(0.1, 0.1)))
                return SamplerStatus.NoChange;

            // get vector to current position
            Vector3d v3d = currentPosition.GetVectorTo(res.Value);
            currentVector = new Vector2d(v3d.X, v3d.Y);
                

            // reset current position
            currentPosition = res.Value;
            

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
        {
            try
            {

                // add vector to all points of all polylines
                foreach (var pl in polylines)
                {
                    for (int i = 0; i < pl.NumberOfVertices; i++)
                    {
                        // add vector to point
                        pl.SetPointAt(i, pl.GetPoint2dAt(i).Add(currentVector));
                    }

                    draw.Geometry.Draw(pl);

                }

                

                

            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

    }
}