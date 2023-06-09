namespace FiorelloOneToMany.Areas.Admin.ViewModels.Slider
{
    public class SliderVM
    {
        public int Id { get; set; }
        public string? Logo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public string CreatedDate { get; set; }
        public string Sliders { get; set; }
    }
}
