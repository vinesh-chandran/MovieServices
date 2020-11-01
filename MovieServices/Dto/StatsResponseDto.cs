namespace MovieServices.Dto
{
    public class StatsResponseDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
