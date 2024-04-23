namespace WMG.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public Movie Movie { get; set; } = new Movie();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Director> Directors { get; set; } = new List<Director>();
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<Format> Formats { get; set; } = new List<Format>();
        public IEnumerable<int> GenresIds { get; set; }
        public IFormFile File { get; set; }

        public MovieVM(){
            Genres = GenreManager.Load();
            Directors = DirectorManager.Load();
            Ratings = RatingManager.Load();
            Formats = FormatManager.Load();
        }

        public MovieVM(int id)
        {
            Movie = MovieManager.LoadById(id);
            Genres = GenreManager.Load();
            Directors = DirectorManager.Load();
            Ratings = RatingManager.Load();
            Formats = FormatManager.Load();
            GenresIds = Movie.Genres.Select(g => g.Id);


        }


    }
}
