using Microsoft.Kinect;
using OneDance.Core.Database.Models;
using OneDance.Core.Enums;

namespace OneDance.Core.Database.Templates
{
    public class PositionTemplate
    {
        public PositionTemplate()
        {
        }

        public PositionTemplate(MovimentoModel positionModel)
        {
            Model = positionModel;
        }

        public int Id
        {
            get { return Model.Id; }
        }

        public MovimentoModel Model
        {
            get;
            set;
        }

        public string JointsText
        {
            get;
            set;
        }

        public MovimentosEnum MovementId
        {
            get { return (MovimentosEnum)Model.Id; }
        }

        public bool IsGesture
        {
            get { return Model.Tipo == (int)MovimentosTypeEnum.GESTO; }
        }
        
        public string ImageDirectory
        {
            get
            {
                string type;

                if (!IsGesture)
                    type = "Poses/";
                else
                    type = "Gestos/";

                string namePosition = Utilities.Util.RemoverAcentos(Model.Nome);

                return "/Images/Positions/" + type + namePosition + "/IMAGEM.png";
            }
        }

        public string GifDirectory
        {
            get
            {
                string type;

                if (!IsGesture)
                    type = "Poses/";
                else
                    type = "Gestos/";

                string namePosition = Utilities.Util.RemoverAcentos(Model.Nome);

                return "/Images/Positions/" + type + namePosition +  "/GIF.gif";
            }
        }

        public string PictogramDirectory
        {
            get { return "/Images/Pictogram/" + Utilities.Util.RemoverAcentos(Model.Nome) + ".png"; }
        }

        public void CreateJoints()
        {
            foreach (var joint in Model.Articulacoes)
                JointsText += joint.Nome + ", ";

            if (JointsText.Length > 0)
                JointsText = JointsText.Remove(JointsText.Length - 2, 2);
        }
    }
}
