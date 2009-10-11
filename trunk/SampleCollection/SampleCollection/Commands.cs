using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using SampleCollection.PaletteJig;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;

namespace SampleCollection
{
    public class Commands : IExtensionApplication
    {

        #region PaletteJig Sample

        /// <summary>
        /// Courtesy of Through The Interface: http://through-the-interface.typepad.com/through_the_interface/2006/11/controlling_int_1.html
        /// Included to demonstrate a problem that I had with this Jig
        /// </summary>
        [CommandMethod("MYPOLY")]
        public void MyPolyJig()
        {
            new Prompts().PromptNewPolylineByPoints();
        }

        // static paletteset
        static PaletteSet polylinePaletteSet;

        private void InitializePolyPalette()
        {
            polylinePaletteSet = new PaletteSet("PolyJigPalette");
            polylinePaletteSet.Add("Main", new PaletteUserControl());
            polylinePaletteSet.Visible = true;

        }

        #endregion



        #region IExtensionApplication Members

        public void Initialize()
        {
            // Palette Jig Sample
            InitializePolyPalette();
        }

        public void Terminate()
        {

        }

        #endregion
    }
}