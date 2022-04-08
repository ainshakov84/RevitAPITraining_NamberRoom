using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_NamberRoom
{
    internal class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<RoomTagType> Tags { get; } = new List<RoomTagType>();
        public List<Level> RoomLevels { get; } = new List<Level>();
        public RoomTagType SelectedTag { get; set; }
        public Level SelectedLevel { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            Tags = TagsUtils.GetRoomTagTypes(commandData);
            RoomLevels = LevelsUtils.GetLevels(commandData);
        }

        private void OnSaveCommand()
        {
            //RaiseHideRequest();
            Document Doc = _commandData.Application.ActiveUIDocument.Document;

            PhaseArray phases = Doc.Phases;
            Phase createRoomsInPhase = phases.get_Item(phases.Size - 1);

            try
            {
                using (Transaction ts = new Transaction(Doc))
                {
                    int x = 1;
                    string levelName = SelectedLevel.Name;
                    ts.Start("Расстановка помещений");
                    {
                        PlanTopology topology = Doc.get_PlanTopology(SelectedLevel, createRoomsInPhase);
                        PlanCircuitSet circuitSet = topology.Circuits;

                        foreach (PlanCircuit circuit in circuitSet)
                        {
                            if (!circuit.IsRoomLocated)
                            {
                                Room room = Doc.Create.NewRoom(null, circuit);
                                room.Name = $"{levelName}_{x}";
                                x++;
                                LocationPoint locationPoint = room.Location as LocationPoint;
                                Autodesk.Revit.DB.UV point = new Autodesk.Revit.DB.UV(locationPoint.Point.X, locationPoint.Point.Y);
                                RoomTag newTag = Doc.Create.NewRoomTag(new LinkElementId(room.Id), point, null);
                                newTag.RoomTagType = SelectedTag;
                            }
                        }
                    }
                    ts.Commit();
                }
            }
            catch
            {

            }

            RaiseCloseRequest();
                
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
           HideRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }
    }

   
}
