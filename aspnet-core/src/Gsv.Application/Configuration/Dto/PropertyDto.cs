using Abp.Application.Features;
using Abp.UI.Inputs;
using Abp.Configuration;

namespace Gsv.Configuration.Dto
{
    public class PropertyDto
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }
        public string Editor { get; set; } = "text";

        public PropertyDto()
        {
        }

        public PropertyDto(Feature feature, string value)
        {
            Name = feature.Name + " " + feature.DisplayName;
            Value = value == null ? feature.DefaultValue : value;

            Editor = ConvertInputType(feature.InputType); 
        }

        public PropertyDto(SettingDefinition sd, string value)
        {
            Name = sd.Name + " " + sd.DisplayName;
            Value = value == null ? sd.DefaultValue : value;
            Group = sd.Name.Split('.')[0];            
        }
        private string ConvertInputType(IInputType inputType)
        {
            switch (inputType.Name)
            {
                case "CHECKBOX":
                    return "text";
                case "SINGLE_LINE_STRING":
                    return "text";
                default:
                    return "text";
            }
        }
     }
}