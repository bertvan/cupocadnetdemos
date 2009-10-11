using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace SampleCollection.PaletteJig
{
    public partial class PaletteUserControl : UserControl
    {
        public PaletteUserControl()
        {
            InitializeComponent();
        }

        private void promptPolyDirectlyButton_Click(object sender, EventArgs e)
        {
            Document activeDocument = Autodesk.AutoCAD.ApplicationServices.Application.
                DocumentManager.MdiActiveDocument;
            
            using (DocumentLock docLock = activeDocument.LockDocument())
            {

                Polyline pl = new Prompts().PromptNewPolylineByPoints();
                OnPromptPolyline(pl);
                
            }
                        
        }

        private void promptPolylineViaCommand_Click(object sender, EventArgs e)
        {

            // SendStringToExecute has asynchronous behaviour, so we cannot inspect the result of the
            // command here.

            // Set the actionOnPromptPolyline delegate to the desired action and 
            // in the command the action will be executed.
            Prompts.ActionOnPromptPolyline = OnPromptPolyline;

            // call command
            Autodesk.AutoCAD.ApplicationServices.Application.
                DocumentManager.MdiActiveDocument.SendStringToExecute("MYPOLY ", true, false, false);

        }

        private void OnPromptPolyline(Polyline polyline)
        {
            if (polyline == null || !polyline.ObjectId.IsValid)
            {
                numberOfPointsTextbox.Text = "No polyline drawn";
                return;
            }

            numberOfPointsTextbox.Text = polyline.NumberOfVertices.ToString();

        }
    }
}
