using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

namespace SampleCollection.PaletteJig
{
    /// <summary>
    /// Courtesy of Through the Interface: http://through-the-interface.typepad.com/through_the_interface/2006/11/controlling_int_1.html <br/>
    /// Included to demonstrate a problem that I had with this Jig
    /// </summary>
    public class PlineJig : EntityJig
    {
        // Maintain a list of vertices...
        // Not strictly necessary, as these will be stored in the
        // polyline, but will not adversely impact performance
        Point3dCollection m_pts;
        // Use a separate variable for the most recent point...
        // Again, not strictly necessary, but easier to reference
        Point3d m_tempPoint;
        Plane m_plane;

        public PlineJig(Matrix3d ucs)
            : base(new Polyline())
        {
            // Create a point collection to store our vertices
            m_pts = new Point3dCollection();

            // Create a temporary plane, to help with calcs
            Point3d origin = new Point3d(0, 0, 0);
            Vector3d normal = new Vector3d(0, 0, 1);
            normal = normal.TransformBy(ucs);
            m_plane = new Plane(origin, normal);

            // Create polyline, set defaults, add dummy vertex
            Polyline pline = Entity as Polyline;
            pline.SetDatabaseDefaults();
            pline.Normal = normal;
            pline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpts =
              new JigPromptPointOptions();
            jigOpts.UserInputControls =
              (UserInputControls.Accept3dCoordinates |
              UserInputControls.NullResponseAccepted |
              UserInputControls.NoNegativeResponseAccepted
              );

            if (m_pts.Count == 0)
            {
                // For the first vertex, just ask for the point
                jigOpts.Message =
                  "\nStart point of polyline: ";
            }
            else if (m_pts.Count > 0)
            {
                // For subsequent vertices, use a base point
                jigOpts.BasePoint = m_pts[m_pts.Count - 1];
                jigOpts.UseBasePoint = true;
                jigOpts.Message = "\nPolyline vertex: ";
            }
            else // should never happen
                return SamplerStatus.Cancel;

            // Get the point itself
            PromptPointResult res =
              prompts.AcquirePoint(jigOpts);

            // Check if it has changed or not
            // (reduces flicker)
            if (m_tempPoint == res.Value)
            {
                return SamplerStatus.NoChange;
            }
            else if (res.Status == PromptStatus.OK)
            {
                m_tempPoint = res.Value;
                return SamplerStatus.OK;
            }
            return SamplerStatus.Cancel;
        }

        protected override bool Update()
        {
            // Update the dummy vertex to be our
            // 3D point projected onto our plane
            Polyline pline = Entity as Polyline;
            pline.SetPointAt(
              pline.NumberOfVertices - 1,
              m_tempPoint.Convert2d(m_plane)
            );
            return true;
        }

        public Entity GetEntity()
        {
            return Entity;
        }

        public void AddLatestVertex()
        {
            // Add the latest selected point to
            // our internal list...
            // This point will already be in the
            // most recently added pline vertex
            m_pts.Add(m_tempPoint);
            Polyline pline = Entity as Polyline;
            // Create a new dummy vertex...
            // can have any initial value
            pline.AddVertexAt(
              pline.NumberOfVertices,
              new Point2d(0, 0),
              0, 0, 0
            );
        }

        public void RemoveLastVertex()
        {
            // Let's remove our dummy vertex
            Polyline pline = Entity as Polyline;
            pline.RemoveVertexAt(m_pts.Count);
        }
    }
}
