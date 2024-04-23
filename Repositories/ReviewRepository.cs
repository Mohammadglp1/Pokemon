using ThePokemonProject.Data;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

namespace ThePokemonProject.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateReview(Review review)
        {
            _dataContext.Add(review);
           return Save();
        }

        public bool DeleteReview(Review review)
        {
            _dataContext.Remove(review);

            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _dataContext.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int id)
        {
           return _dataContext.Reviews.FirstOrDefault(r=>r.Id==id);
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _dataContext.Reviews.Where(p=>p.Id==pokeId).ToList();
        }

        public bool ReviewExists(int id)
        {
           return _dataContext.Reviews.Any(r => r.Id==id);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _dataContext.Update(review);
            return Save();
        }
    }
}
